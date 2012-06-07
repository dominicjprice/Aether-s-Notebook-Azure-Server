#light

module Aethers.Notebook.Configuration

open Microsoft.FSharp.Reflection

open Microsoft.WindowsAzure
open Microsoft.WindowsAzure.Diagnostics
open Microsoft.WindowsAzure.ServiceRuntime
open Microsoft.WindowsAzure.StorageClient

open System
open System.IO
open System.Linq
open System.Reflection

let inline value<'T> name : 'T =
    let v = RoleEnvironment.GetConfigurationSettingValue(name)
    let t = typeof<'T>
    System.Convert.ChangeType(v, t) :?> 'T

let onStart () =
    CloudStorageAccount.SetConfigurationSettingPublisher(
            fun configName configSetter ->
                    configSetter.Invoke(value configName) |> ignore
                    RoleEnvironment.Changed.Add(fun arg ->
                            if arg.Changes.OfType<RoleEnvironmentConfigurationSettingChange>()
                                    .Any(fun change -> change.ConfigurationSettingName = configName)
                            then
                                if configSetter.Invoke(value configName) |> not
                                then RoleEnvironment.RequestRecycle()
                    )
            )

let configureDiagnostics () =
    let diaConf = DiagnosticMonitor.GetDefaultInitialConfiguration()
    
    let logLevel = Enum.Parse(typeof<LogLevel>, value("Aethers.Notebook.Diagnostics.LogLevel"), true) :?> LogLevel
    let transferPeriod = TimeSpan.FromMinutes(value("Aethers.Notebook.Diagnostics.TransferPeriod"))
    let bufferQuota = value("Aethers.Notebook.Diagnostics.BufferQuota")

    diaConf.Logs.ScheduledTransferPeriod <- transferPeriod
    diaConf.Logs.BufferQuotaInMB <- bufferQuota
    diaConf.Logs.ScheduledTransferLogLevelFilter <- logLevel

    diaConf.Directories.ScheduledTransferPeriod <- transferPeriod
    diaConf.Directories.BufferQuotaInMB <- bufferQuota

    diaConf.DiagnosticInfrastructureLogs.ScheduledTransferPeriod <- transferPeriod
    diaConf.DiagnosticInfrastructureLogs.BufferQuotaInMB <- bufferQuota
    diaConf.DiagnosticInfrastructureLogs.ScheduledTransferLogLevelFilter <- logLevel

    diaConf.WindowsEventLog.ScheduledTransferPeriod <- transferPeriod
    diaConf.WindowsEventLog.BufferQuotaInMB <- bufferQuota
    diaConf.WindowsEventLog.ScheduledTransferLogLevelFilter <- logLevel
    
    DiagnosticMonitor.Start("Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString", diaConf) |> ignore

let configureDevelopmentDiagnostics () =
    if RoleEnvironment.IsEmulated then
        try
            let dllPath = 
                Path.Combine(
                    (new Uri(
                        Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase ))).LocalPath,
                        "Microsoft.ServiceHosting.Tools.DevelopmentFabric.Runtime.dll")
            let assembly = Assembly.LoadFile(dllPath)
            let t = assembly.GetType("Microsoft.ServiceHosting.Tools.DevelopmentFabric.Runtime.DevelopmentFabricTraceListener") 
            Diagnostics.Trace.Listeners.Add(Activator.CreateInstance(t) :?> Diagnostics.TraceListener) |> ignore
        with _ -> ()

let configureStorage () =
    let account = CloudStorageAccount.FromConfigurationSetting("Aethers.Notebook.Storage.ConnectionString")
        
    let tables =
        match value<string>("Aethers.Notebook.Storage.Tables") with
            | s when String.IsNullOrWhiteSpace(s) -> Array.empty
            | s -> s.Split(';')
    let tc = CloudStorageAccountStorageClientExtensions.CreateCloudTableClient(account)
    tables |> Array.iter (fun t -> tc.CreateTableIfNotExist(t) |> ignore)

    let containers =
        match value<string>("Aethers.Notebook.Storage.Containers") with
            | s when String.IsNullOrWhiteSpace(s) -> Array.empty
            | s -> s.Split(';')
    let bc = CloudStorageAccountStorageClientExtensions.CreateCloudBlobClient(account)
    containers 
        |> Array.iter (
            fun c -> bc.GetContainerReference(c).CreateIfNotExist() |> ignore)
                
    let queues =
        match value<string>("Aethers.Notebook.Storage.Queues") with
            | s when String.IsNullOrWhiteSpace(s) -> Array.empty
            | s -> s.Split(';')
    let qc = CloudStorageAccountStorageClientExtensions.CreateCloudQueueClient(account)
    queues |> Array.iter (fun q -> qc.GetQueueReference(q).CreateIfNotExist() |> ignore)