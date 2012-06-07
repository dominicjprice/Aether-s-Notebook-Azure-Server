#light

namespace Aethers.Notebook.Controllers

open Microsoft.WindowsAzure
open Microsoft.WindowsAzure.StorageClient

open System.Web.Mvc

module Conf = Aethers.Notebook.Configuration
module DA = Aethers.Notebook.DataAccess

[<HandleError>]
type ReceiverController() =
    inherit Controller()

    static let storageAccount = CloudStorageAccount.FromConfigurationSetting("Aethers.Notebook.Storage.ConnectionString")
    static let blobClient = CloudStorageAccountStorageClientExtensions.CreateCloudBlobClient(storageAccount)
    static let queueClient = CloudStorageAccountStorageClientExtensions.CreateCloudQueueClient(storageAccount)
    static let blobContainer = blobClient.GetContainerReference(Conf.value "Aethers.Notebook.Storage.Container.UploadedData")
    static let queueContainer = queueClient.GetQueueReference(Conf.value "Aethers.Notebook.Storage.Queue.UploadedData")
  
    do()
    
    [<HttpPost>]
    [<ValidateInput(false)>]
#if HTTPS
    [<RequireHttpsAttribute>]
#endif
    member this.Index() = 
        let openID = this.Request.Headers.["X-AethersNotebook-OpenID"]
        let secret = this.Request.Headers.["X-AethersNotebook-Secret"]
        if DA.verifyUser openID secret
        then
            let id = System.Guid.NewGuid().ToString()
            let blob = blobContainer.GetBlockBlobReference(id)
            blob.Properties.ContentType <- "application/x-gzip"
            blob.UploadFromStream(this.Request.InputStream)
            blob.Metadata.["openID"] <- openID
            blob.SetMetadata()
            queueContainer.AddMessage(new CloudQueueMessage(id))
            new EmptyResult()
        else
            this.Response.StatusCode <- 401
            new EmptyResult()