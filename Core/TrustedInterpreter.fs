#light

module Aethers.Notebook.TrustedInterpreter

open Ionic.Zip

open IronPython.Hosting

open Microsoft.WindowsAzure.ServiceRuntime

open System
open System.IO
open System.Security
open System.Security.AccessControl
open System.Security.Policy
open System.Security.Permissions
open System.Reflection

let sandboxPath = RoleEnvironment.GetLocalResource("Sandbox").RootPath
sprintf "Sandbox Path: %s" sandboxPath |> Log.verbose
try
    let exAssembly = System.Reflection.Assembly.GetExecutingAssembly()
    let inpath = 
        (new Uri(
            Path.GetDirectoryName(
                exAssembly.GetName().CodeBase ))).LocalPath
    [ "PythonSandbox.dll"; "Model.dll" ] 
        |> List.iter (
            fun name ->
                File.Copy(
                    Path.Combine(inpath, name),
                    Path.Combine(sandboxPath, name),
                    true))
    use zipFile = ZipFile.Read(exAssembly.GetManifestResourceStream("pythonlib.zip"))
    zipFile.ExtractAll(sandboxPath, ExtractExistingFileAction.OverwriteSilently)
with _ as e ->
    sprintf "Error setting up sandbox: %s\n%s" e.Message e.StackTrace |> Log.error

type t () =
    inherit MarshalByRefObject()
    do()    
    member this.ExecuteUntrustedCode 
            (assemblyName : string) 
            (typeName : string)
            (entryPoint : string)
            (parameters : Object[]) =
        let target = Assembly.Load(assemblyName).GetType(typeName).GetMethod(entryPoint)
        target.Invoke(null, parameters)

let parseUntrusted (variables : Map<string, obj>) (code : string) =
    let adSetup = new AppDomainSetup()
    adSetup.ApplicationBase <- Path.GetFullPath(sandboxPath)

    let permSet = new PermissionSet(PermissionState.None)
    
    permSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution)) |> ignore
    
    let fileIOPerm = new FileIOPermission(PermissionState.None)
    fileIOPerm.AddPathList(
        (FileIOPermissionAccess.AllAccess 
            ^^^ FileIOPermissionAccess.Write)
                ^^^ FileIOPermissionAccess.Append, sandboxPath)
    permSet.AddPermission(fileIOPerm) |> ignore
    
    let refPermission = new ReflectionPermission(PermissionState.None)
    refPermission.Flags <- ReflectionPermissionFlag.MemberAccess
    permSet.AddPermission(refPermission) |> ignore

    let fullTrustAssembly = typeof<t>.Assembly.Evidence.GetHostEvidence<StrongName>()
    let newDomain = AppDomain.CreateDomain("Sandbox", null, adSetup, permSet, fullTrustAssembly)
    let handle = Activator.CreateInstanceFrom(
                    newDomain,
                    typeof<t>.Assembly.ManifestModule.FullyQualifiedName,
                    typeof<t>.FullName)
    let newDomainInstance = handle.Unwrap() :?> t
    let parameters : obj array = [| variables; code; [ sandboxPath ] |]
    let result = 
        newDomainInstance.ExecuteUntrustedCode 
            "PythonSandbox"
            "Aethers.Notebook.PythonSandbox"
            "parse"
            parameters
    match result with
        | :? Exception as e ->
            sprintf "Python sandbox error: %s\n%s" e.Message e.StackTrace |> Log.error
            raise e
        | _ -> 
            result