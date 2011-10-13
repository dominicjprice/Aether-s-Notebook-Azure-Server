#light

namespace Aethers.Notebook.Controllers

open Microsoft.WindowsAzure
open Microsoft.WindowsAzure.StorageClient

module Storage = Aethers.Notebook.Storage

[<System.Web.Mvc.HandleError>]
type ReceiverController() =
    inherit System.Web.Mvc.Controller()
    
    do()
    
    [<System.Web.Mvc.HttpPost>]
    [<System.Web.Mvc.ValidateInput(false)>]
    //[<System.Web.Mvc.RequireHttpsAttribute>]
    member this.Index() = 
        let openID = this.Request.Headers.["X-AethersNotebook-OpenID"]
        let secret = this.Request.Headers.["X-AethersNotebook-Secret"]
        if Storage.verifyUser openID secret
        then 
            let storageAccount = CloudStorageAccount.FromConfigurationSetting("DataConnectionString")
            let blobClient = CloudStorageAccountStorageClientExtensions.CreateCloudBlobClient(storageAccount)
            let queueClient = CloudStorageAccountStorageClientExtensions.CreateCloudQueueClient(storageAccount)
            let blobContainer = blobClient.GetContainerReference(ServiceRuntime.RoleEnvironment.GetConfigurationSettingValue("UploadedDataContainer"))
            let queueContainer = queueClient.GetQueueReference(ServiceRuntime.RoleEnvironment.GetConfigurationSettingValue("UploadedDataQueue"))
            blobContainer.CreateIfNotExist() |> ignore
            queueContainer.CreateIfNotExist() |> ignore
            let id = System.Guid.NewGuid().ToString()
            let blob = blobContainer.GetBlockBlobReference(id)
            blob.Properties.ContentType <- "application/x-gzip"
            blob.UploadFromStream(this.Request.InputStream)
            blob.Metadata.["openID"] <- openID
            blob.SetMetadata()
            queueContainer.AddMessage(new CloudQueueMessage(id))
            new System.Web.Mvc.EmptyResult()
        else
            this.Response.StatusCode <- 401
            new System.Web.Mvc.EmptyResult()