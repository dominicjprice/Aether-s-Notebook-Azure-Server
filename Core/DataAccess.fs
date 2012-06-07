#light

module Aethers.Notebook.DataAccess

open Aethers.Notebook.Model

open Microsoft.FSharp.Linq
open Microsoft.FSharp.Linq.Query
open Microsoft.FSharp.Linq.QuotationEvaluation

open Microsoft.WindowsAzure
open Microsoft.WindowsAzure.Diagnostics
open Microsoft.WindowsAzure.ServiceRuntime
open Microsoft.WindowsAzure.StorageClient

open Newtonsoft.Json
open Newtonsoft.Json.Linq

open System
open System.IO
open System.IO.Compression
open System.Linq

module Conf = Aethers.Notebook.Configuration
module Log = Aethers.Notebook.Log

type t = Aethers.Notebook.DataAccess.ModelDataContext

let UTF8 = Text.Encoding.UTF8

type Microsoft.FSharp.Quotations.Expr with
    member this.ToLambdaExpression() = 
        let e = this.ToLinqExpression() :?> Linq.Expressions.MethodCallExpression
        e.Arguments.[0] :?> Linq.Expressions.LambdaExpression

let connect () = new t(Conf.value<string>("Aethers.Notebook.DataAccess.ConnectionString"))

let getNeighbouringCells (data : JToken) (neighbouringCells : NeighbouringCells) =
    data |> Seq.map (fun d -> 
            let nc = new NeighbouringCell()
            nc.NeighbouringCells <- neighbouringCells
            nc.cid <- (int)d.["cid"]
            nc.lac <- (int)d.["lac"]
            nc.psc <- (int)d.["psc"]
            nc.rssi <- (int)d.["rssi"]
            nc.networkType <- (string)d.["networkType"]
            nc)

let insertCellLocation (ctx : t) logEntry data = 
    let text = UTF8.GetString(Convert.FromBase64String(data))
    let o = JObject.Parse(text)
    match ((string)o.["type"]) with
        | "android.telephony.cdma.CdmaCellLocation" -> 
                let ob = new CdmaCellLocation()
                ob.LogEntry <- logEntry
                ob.baseStationId <- (int)o.["baseStationId"]
                ob.baseStationLatitude <- (int)o.["baseStationLatitude"]
                ob.baseStationLongitude <- (int)o.["baseStationLongitude"]
                ob.networkId <- (int)o.["networkId"]
                ob.systemId <- (int)o.["systemId"]
                let n = new NeighbouringCells()
                ob.NeighbouringCells <- n
                let ns = getNeighbouringCells o.["neighbouringCells"] n
                ctx.NeighbouringCells1.InsertAllOnSubmit ns
                ctx.NeighbouringCells.InsertOnSubmit(n)
                ctx.CdmaCellLocations.InsertOnSubmit(ob)
        | "android.telephony.gsm.GsmCellLocation" -> 
                let ob = new GsmCellLocation()                
                ob.LogEntry <- logEntry
                ob.cid <- (int)o.["cid"]
                ob.lac <- (int)o.["lac"]
                let n = new NeighbouringCells()
                ob.NeighbouringCells <- n
                let ns = getNeighbouringCells o.["neighbouringCells"] n
                ctx.NeighbouringCells1.InsertAllOnSubmit ns
                ctx.NeighbouringCells.InsertOnSubmit(n)
                ctx.GsmCellLocations.InsertOnSubmit(ob)
        | _ -> ()

let insertDataConnectionState (ctx : t) logEntry data = 
    let text = UTF8.GetString(Convert.FromBase64String(data))
    let ob = JsonConvert.DeserializeObject<DataConnectionState>(text)
    ob.LogEntry <- logEntry

let insertServiceState (ctx : t) logEntry data = 
    let text = UTF8.GetString(Convert.FromBase64String(data))
    let ob = JsonConvert.DeserializeObject<ServiceState>(text)
    ob.LogEntry <- logEntry

let insertSignalStrength (ctx : t) logEntry data = 
    let text = UTF8.GetString(Convert.FromBase64String(data))
    let ob = JsonConvert.DeserializeObject<SignalStrength>(text)
    ob.LogEntry <- logEntry

let insertWifi (ctx : t) logEntry data =
    let text = UTF8.GetString(Convert.FromBase64String(data))
    let obs = JsonConvert.DeserializeObject<WifiScanResult[]>(text) 
    obs |> Array.iter (fun o -> o.LogEntry <- logEntry)
    ctx.WifiScanResults.InsertAllOnSubmit(obs)

let insertPosition (ctx : t) logEntry data = 
    let text = UTF8.GetString(Convert.FromBase64String(data))
    let o = JObject.Parse(text)
    match ((string)o.["event"]) with
        | "statusChanged" -> 
                let ob = new PositionStatusChanged()
                ob.LogEntry <- logEntry
                ob.provider <- (string)o.["provider"]
                ob.status <- (string)o.["status"]
                ob.extras <- (string)o.["extras"]
                ctx.PositionStatusChangeds.InsertOnSubmit(ob)
        | "enabled" ->
                let ob = new PositionProviderEnabledDisabled()
                ob.LogEntry <- logEntry
                ob.provider <- (string)o.["provider"]
                ob.isEnabled <- true
                ctx.PositionProviderEnabledDisableds.InsertOnSubmit(ob)
        | "disabled" ->
                let ob = new PositionProviderEnabledDisabled()
                ob.LogEntry <- logEntry
                ob.provider <- (string)o.["provider"]
                ob.isEnabled <- false
                ctx.PositionProviderEnabledDisableds.InsertOnSubmit(ob)
        | "locationChanged" ->
                let ob = new PositionLocationChanged()
                ob.LogEntry <- logEntry
                ob.provider <- (string)o.["provider"]
                if o.["location"].["accuracy"] <> null
                then ob.accuracy <- new Nullable<float>(Double.Parse(o.["location"].["accuracy"].ToString()))
                if o.["location"].["altitude"] <> null 
                then ob.altitude <- new Nullable<float>(Double.Parse(o.["location"].["altitude"].ToString()))
                if o.["location"].["bearing"] <> null 
                then ob.bearing <- new Nullable<float>(Double.Parse(o.["location"].["bearing"].ToString()))
                ob.latitude <- (float)o.["location"].["latitude"]
                ob.longitude <- (float)o.["location"].["longitude"]
                if o.["location"].["speed"] <> null
                then ob.speed <- new Nullable<float>(Double.Parse(o.["location"].["speed"].ToString()))
                ob.time <- new Nullable<int64>((int64)o.["location"].["time"])
                if o.["location"].["time"] <> null
                then ob.extras <- (string)o.["location"].["extras"]
                ctx.PositionLocationChangeds.InsertOnSubmit(ob)
        | _ -> ()

let insertUnknown (ctx : t) logEntry data = 
    let u = new Unrecognised()
    u.LogEntry <- logEntry
    let storageAccount = CloudStorageAccount.FromConfigurationSetting("Aethers.Notebook.Storage.ConnectionString")
    let blobClient = CloudStorageAccountStorageClientExtensions.CreateCloudBlobClient(storageAccount)
    let blobContainer = blobClient.GetContainerReference(Conf.value("Aethers.Notebook.Storage.Container.UnknownData"))
    blobContainer.CreateIfNotExist() |> ignore
    let id = Guid.NewGuid().ToString()
    let blob = blobContainer.GetBlockBlobReference(id)
    blob.UploadText(data)
    u.blobUri <- blob.Uri.ToString()
    ctx.Unrecogniseds.InsertOnSubmit(u)

let getIdentifier uniqueID version (ctx : t) =
    try
        ctx.LoggerIdentifiers.Single(fun (i : LoggerIdentifier) -> i.uniqueID.Equals(uniqueID) && i.version = version)
    with _ -> 
        let li = new LoggerIdentifier()
        li.uniqueID <- uniqueID
        li.version <- version
        ctx.LoggerIdentifiers.InsertOnSubmit li
        li

let parseLine line openID =
    let o = JObject.Parse(line)
    use ctx = connect ()
    let logEntry = new LogEntry()
    let user = ctx.Users.Single(new Func<_,_>(fun (u : User) -> u.claimedIdentifier.Equals(openID)))
    logEntry.User <- user
    let identifier = getIdentifier ((string)o.["identifier"].["uniqueID"]) ((int)o.["identifier"].["version"]) ctx
    logEntry.LoggerIdentifier <- identifier
    let timestamp = new Timestamp()
    timestamp.systemTime <- (int64)o.["timestamp"].["systemTime"]
    timestamp.timezone <- (string)o.["timestamp"].["timezone"]
    ctx.Timestamps.InsertOnSubmit timestamp
    logEntry.Timestamp <- timestamp
    match o.["location"] with
        | x when x.Type = JTokenType.Null -> ()
        | _ as loc ->
            let location = new Location()
            location.accuracy <- new Nullable<float>((float)loc.["accuracy"])
            location.altitude <- new Nullable<float>((float)loc.["altitude"])
            location.bearing <- new Nullable<float>((float)loc.["bearing"])
            location.latitude <- (float)loc.["latitude"]
            location.longitude <- (float)loc.["longitude"]
            location.provider <- (string)loc.["provider"]
            location.speed <- new Nullable<float>((float)loc.["speed"])
            location.time <- new Nullable<int64>((int64)loc.["time"])
            location.extras <- loc.["extras"].ToString()
            ctx.Locations.InsertOnSubmit location
            logEntry.Location <- location
    ctx.LogEntries.InsertOnSubmit logEntry
    (match identifier.uniqueID with
        | "aethers.notebook.logger.managed.celllocation.CellLocationLogger" -> insertCellLocation
        | "aethers.notebook.logger.managed.dataconnectionstate.DataConnectionStateLogger" -> insertDataConnectionState
        | "aethers.notebook.logger.managed.servicestate.ServiceStateLogger" -> insertServiceState
        | "aethers.notebook.logger.managed.signalstrength.SignalStrengthLogger" -> insertSignalStrength
        | "aethers.notebook.logger.managed.wifi.WifiLogger" -> insertWifi
        | "aethers.notebook.logger.managed.position.PositionLogger" -> insertPosition
        | _ -> insertUnknown) ctx logEntry ((string)o.["data"])
     
    ctx.SubmitChanges()

let store (msg : CloudQueueMessage) =
    let storageAccount = CloudStorageAccount.FromConfigurationSetting("Aethers.Notebook.Storage.ConnectionString")
    let blobClient = CloudStorageAccountStorageClientExtensions.CreateCloudBlobClient(storageAccount)
    let blobContainer = blobClient.GetContainerReference(Conf.value("Aethers.Notebook.Storage.Container.UploadedData"))
    blobContainer.CreateIfNotExist() |> ignore
    let blob = blobContainer.GetBlockBlobReference(msg.AsString)
    let stream = new MemoryStream()
    blob.DownloadToStream(stream)
    blob.FetchAttributes()
    let openID = blob.Metadata.["openID"]
    stream.Position <- int64 0
    let rec processLine (s : StreamReader) = 
        match s.ReadLine() with
        | null -> s.Close()
        | line -> 
            parseLine line openID
            processLine s
    processLine (new StreamReader(new GZipStream(new BufferedStream(stream), CompressionMode.Decompress), Text.Encoding.UTF8))
    blob.Delete()

let addOrUpdateUser (claimedID : string) (friendlyID : string) : User * bool =
    use ctx = connect ()
    let user = ctx.Users.SingleOrDefault(
                new Func<_,_>(fun (u : User) -> u.claimedIdentifier.Equals(claimedID)))
    match user with
        | null ->
            let u = new User()
            u.claimedIdentifier <- claimedID
            u.friendlyIdentifier <- friendlyID
            u.secret <- Guid.NewGuid().ToString()
            ctx.Users.InsertOnSubmit u
            ctx.SubmitChanges()
            (u, true)
        | _ -> 
            if user.friendlyIdentifier.Equals(friendlyID) |> not
            then
                user.friendlyIdentifier <- friendlyID
                ctx.SubmitChanges()
            (user, false)

let verifyUser (openID : string) (secret : string) : bool =
    use ctx = connect ()
    try
        let user = ctx.Users.Single(
                    Func<_,_>(
                        fun (u : User) -> 
                                        u.claimedIdentifier.Equals(openID) 
                                        && u.secret.Equals(secret)))
        true
    with
        | _ -> false

let getCatalogByUri catalogUri =
    use ctx = connect ()
    try
        Some(ctx.Catalogs.Single(
                Func<_,_>(
                    fun (c : Catalog) -> c.catalogUri.Equals(catalogUri))))
    with
        | _ -> None

let updateDisplayName openID displayName =
    use ctx = connect ()
    try
        let u = ctx.Users.Single(
                    Func<_,_>(
                        fun (u : User) -> u.claimedIdentifier.Equals(openID)))
        u.displayName <- displayName
        ctx.SubmitChanges()
    with
        | _ -> ()

let getUserByOpenID openID =
    use ctx = connect ()
    try
        Some(ctx.Users.Single(
                    Func<_,_>(
                        fun (u : User) -> u.claimedIdentifier.Equals(openID))))
    with
        | _ -> None

let insertCatalog (catalog : Catalog) =
    use ctx = connect ()
    ctx.Catalogs.InsertOnSubmit(catalog)
    ctx.SubmitChanges()
    catalog

let createUserCatalog openID catalogUri =
    let uc = new UserCatalog()
    uc.status <- UserCatalog.StatusCode.InstallationRequestSent
    uc.state <- Guid.NewGuid().ToString()
    use ctx = connect ()
    uc.User <- ctx.Users.Single(
                    Func<_,_>(
                        fun (u : User) -> u.claimedIdentifier.Equals(openID)))
    uc.Catalog <- ctx.Catalogs.Single(
                    Func<_,_>(
                        fun (c : Catalog) -> c.catalogUri.Equals(catalogUri)))
    ctx.UserCatalogs.InsertOnSubmit uc
    ctx.SubmitChanges()
    uc
   
let getUserCatalog state openID status =
    use ctx = connect ()
    ctx.DeferredLoadingEnabled <- false
    let dlo = new Data.Linq.DataLoadOptions()
    dlo.LoadWith(<@ fun (uc : UserCatalog) -> uc.Catalog @>.ToLambdaExpression())
    dlo.LoadWith(<@ fun (uc : UserCatalog) -> uc.User @>.ToLambdaExpression())
    ctx.LoadOptions <- dlo
    ctx.UserCatalogs.Single(
        Func<_,_>(
            fun (uc : UserCatalog) -> 
                uc.status = status 
                && uc.User.claimedIdentifier.Equals(openID) 
                && uc.state.Equals(state)))
    

let markUserCatalogInstallAsRejected (catalog : UserCatalog) =
    use ctx = connect ()
    let ucat = 
        ctx.UserCatalogs.Single(
            Func<_,_>(fun (u : UserCatalog) -> u.ID = catalog.ID))
    ucat.status <- UserCatalog.StatusCode.InstallationRejected
    ctx.SubmitChanges()

let markUserCatalogAccessTokenRequest (catalog : UserCatalog) code =
    use ctx = connect ()
    let ucat = 
        ctx.UserCatalogs.Single(
            Func<_,_>(fun (u : UserCatalog) -> u.ID = catalog.ID))
    ucat.auth_code <- code
    ucat.status <- UserCatalog.StatusCode.AccessTokenRequestSent
    ctx.SubmitChanges()

let markUserCatalogAccessTokenRejected (catalog : UserCatalog) =
    use ctx = connect ()
    let ucat = 
        ctx.UserCatalogs.Single(
            Func<_,_>(fun (u : UserCatalog) -> u.ID = catalog.ID))
    ucat.status <- UserCatalog.StatusCode.AccessTokenRequestFailed
    ctx.SubmitChanges()

let completeUserCatalogInstallation (catalog : UserCatalog) accessToken =
    use ctx = connect ()
    let ucat = 
        ctx.UserCatalogs.Single(
            Func<_,_>(
                fun (u : UserCatalog) -> u.ID = catalog.ID))
    ucat.access_token <- accessToken
    ucat.status <- UserCatalog.StatusCode.AccessTokenRequestSucceeded
    ctx.SubmitChanges()  

let getUsersCatalogs (user : User) =
    use ctx = connect ()
    let dlo = new Data.Linq.DataLoadOptions()
    dlo.LoadWith(<@ fun (uc : UserCatalog) -> uc.Catalog @>.ToLambdaExpression())
    dlo.LoadWith(<@ fun (uc : UserCatalog) -> uc.User @>.ToLambdaExpression())
    ctx.LoadOptions <- dlo
    ctx.UserCatalogs.Where(
        Func<_,_>(
            fun (uc : UserCatalog) -> uc.User.ID = user.ID)) |> Seq.toList

let getUserCatalogByAccessToken accessToken =
    use ctx = connect ()
    ctx.DeferredLoadingEnabled <- false
    let dlo = new Data.Linq.DataLoadOptions()
    dlo.LoadWith(<@ fun (uc : UserCatalog) -> uc.Catalog @>.ToLambdaExpression())
    dlo.LoadWith(<@ fun (uc : UserCatalog) -> uc.User @>.ToLambdaExpression())
    ctx.LoadOptions <- dlo
    ctx.UserCatalogs.Single(
        Func<_,_>(
            fun (uc : UserCatalog) -> uc.access_token = accessToken))

let saveQueryRequest (resourceQuery : ResourceQuery) =
    use ctx = connect ()
    let uc = ctx.UserCatalogs.Single(
                Func<_,_>(
                        fun (uc : UserCatalog) -> uc.ID = resourceQuery.userCatalogID))
    resourceQuery.UserCatalog <- uc
    ctx.ResourceQueries.InsertOnSubmit(resourceQuery)
    ctx.SubmitChanges()

let getQueryRequestByAccessToken (accessToken : string) =
    use ctx = connect ()
    ctx.ResourceQueries.Single(
        Func<_,_>(
            fun (rq : ResourceQuery) -> rq.access_token = accessToken))