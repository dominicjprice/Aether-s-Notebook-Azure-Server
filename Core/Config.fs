#light

module Aethers.Notebook.Config

open Microsoft.WindowsAzure
open Microsoft.WindowsAzure.Diagnostics
open Microsoft.WindowsAzure.ServiceRuntime

open System.Linq

let setupDiagnostics () =
    ()

let setupConfigurationPublisher () =
    CloudStorageAccount.SetConfigurationSettingPublisher(
            fun configName configSetter ->
                    configSetter.Invoke(RoleEnvironment.GetConfigurationSettingValue(configName)) |> ignore
                    RoleEnvironment.Changed.Add(fun arg ->
                            if arg.Changes.OfType<RoleEnvironmentConfigurationSettingChange>().Any(fun change -> change.ConfigurationSettingName = configName)
                            then
                                if configSetter.Invoke(RoleEnvironment.GetConfigurationSettingValue(configName)) |> not
                                then RoleEnvironment.RequestRecycle()
                    )
            )    