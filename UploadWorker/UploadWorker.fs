#light

namespace Aethers.Notebook.Workers

open System
open System.Collections.Generic
open System.Diagnostics
open System.Linq
open System.Net
open System.Threading
open Microsoft.WindowsAzure
open Microsoft.WindowsAzure.Diagnostics
open Microsoft.WindowsAzure.ServiceRuntime
open Microsoft.WindowsAzure.StorageClient

module Log = Aethers.Notebook.Log
module S = Aethers.Notebook.Storage

type UploadWorker() =
    inherit RoleEntryPoint() 
        
    override wr.Run() =
        let storageAccount = CloudStorageAccount.FromConfigurationSetting("DataConnectionString")
        let queueClient = CloudStorageAccountStorageClientExtensions.CreateCloudQueueClient(storageAccount)
        let queueContainer = queueClient.GetQueueReference(ServiceRuntime.RoleEnvironment.GetConfigurationSettingValue("UploadedDataQueue"))
        queueContainer.CreateIfNotExist() |> ignore
        while(true) do
            Log.information("UploadWorker role wake")
            queueContainer.GetMessages(10) 
                    |> Seq.map (fun msg -> async 
                                            { 
                                                try
                                                    Log.information ("Processing queue message: " + msg.Id)
                                                    S.store msg
                                                with
                                                    | _ as e -> 
                                                            Log.error(e.Message)
                                                            Log.error(e.StackTrace)
                                                queueContainer.DeleteMessage(msg)
                                                return ()
                                            }) 
                    |> Async.Parallel 
                    |> Async.RunSynchronously
                    |> ignore
            Log.information("UploadWorker role sleep")
            Thread.Sleep(10000)

    override wr.OnStart() =
        Aethers.Notebook.Config.setupConfigurationPublisher()
        Aethers.Notebook.Config.setupDiagnostics()
        base.OnStart()