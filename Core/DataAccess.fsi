#light

module Aethers.Notebook.DataAccess

val store : Microsoft.WindowsAzure.StorageClient.CloudQueueMessage -> unit
    
val addOrUpdateUser : string -> string -> Aethers.Notebook.Model.User * bool

val verifyUser : string -> string -> bool

val getCatalogByUri : string -> Aethers.Notebook.Model.Catalog option

val insertCatalog : Aethers.Notebook.Model.Catalog -> Aethers.Notebook.Model.Catalog

val updateDisplayName : string -> string -> unit

val getUserByOpenID : string -> Aethers.Notebook.Model.User option

val createUserCatalog : string -> string -> Aethers.Notebook.Model.UserCatalog

val getUserCatalog : string -> string -> Aethers.Notebook.Model.UserCatalog.StatusCode -> Aethers.Notebook.Model.UserCatalog

val markUserCatalogInstallAsRejected : Aethers.Notebook.Model.UserCatalog -> unit

val markUserCatalogAccessTokenRequest : Aethers.Notebook.Model.UserCatalog -> string -> unit

val markUserCatalogAccessTokenRejected : Aethers.Notebook.Model.UserCatalog -> unit
    
val completeUserCatalogInstallation :  Aethers.Notebook.Model.UserCatalog -> string -> unit

val getUsersCatalogs : Aethers.Notebook.Model.User -> Aethers.Notebook.Model.UserCatalog list

val getUserCatalogByAccessToken : string -> Aethers.Notebook.Model.UserCatalog

val saveQueryRequest : Aethers.Notebook.Model.ResourceQuery -> unit

val getQueryRequestByAccessToken : string -> Aethers.Notebook.Model.ResourceQuery