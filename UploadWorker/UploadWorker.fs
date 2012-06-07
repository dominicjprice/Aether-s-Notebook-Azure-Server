#light

namespace Aethers.Notebook.Workers

open System.Threading
open Microsoft.WindowsAzure
open Microsoft.WindowsAzure.ServiceRuntime
open Microsoft.WindowsAzure.StorageClient

module Conf = Aethers.Notebook.Configuration
module DA = Aethers.Notebook.DataAccess
module Log = Aethers.Notebook.Log

type UploadWorker() =
    inherit RoleEntryPoint() 
    
    let processMessage (msg : CloudQueueMessage) =
        let storageAccount = CloudStorageAccount.FromConfigurationSetting("Aethers.Notebook.Storage.ConnectionString")
        let queueClient = CloudStorageAccountStorageClientExtensions.CreateCloudQueueClient(storageAccount)
        let queueContainer = queueClient.GetQueueReference(RoleEnvironment.GetConfigurationSettingValue("Aethers.Notebook.Storage.Queue.UploadedData"))
        try
            Log.information ("Processing queue message: " + msg.Id)
            DA.store msg
        with
            | _ as e -> 
                    Log.error(e.Message)
                    Log.error(e.StackTrace)
        queueContainer.DeleteMessage(msg)

    override wr.Run() =   
        let storageAccount = CloudStorageAccount.FromConfigurationSetting("Aethers.Notebook.Storage.ConnectionString")
        let queueClient = CloudStorageAccountStorageClientExtensions.CreateCloudQueueClient(storageAccount)
        let queueContainer = queueClient.GetQueueReference(RoleEnvironment.GetConfigurationSettingValue("Aethers.Notebook.Storage.Queue.UploadedData"))
        while(true) do
            queueContainer.GetMessages(10) 
                    |> Seq.map (fun msg -> async { return processMessage msg }) 
                    |> Async.Parallel 
                    |> Async.RunSynchronously
                    |> ignore
            Thread.Sleep(10000)

    override wr.OnStart() =
        Conf.onStart ()
        Conf.configureStorage ()
        Conf.configureDiagnostics ()
        base.OnStart()