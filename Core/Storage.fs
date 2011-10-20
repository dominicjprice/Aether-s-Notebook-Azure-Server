#light

module Aethers.Notebook.Storage

open System.IO
open System.IO.Compression
open System.Linq

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

module DAO = Aethers.Notebook.DataAccess.DataContext
module Log = Aethers.Notebook.Log

let utf8 = new System.Text.UTF8Encoding()

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

let insertCellLocation (context : DAO.t) logEntry data = 
    let text = utf8.GetString(System.Convert.FromBase64String(data))
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
                context.NeighbouringCells1.InsertAllOnSubmit ns
                context.NeighbouringCells.InsertOnSubmit(n)
                context.CdmaCellLocations.InsertOnSubmit(ob)
        | "android.telephony.gsm.GsmCellLocation" -> 
                let ob = new GsmCellLocation()                
                ob.LogEntry <- logEntry
                ob.cid <- (int)o.["cid"]
                ob.lac <- (int)o.["lac"]
                let n = new NeighbouringCells()
                ob.NeighbouringCells <- n
                let ns = getNeighbouringCells o.["neighbouringCells"] n
                context.NeighbouringCells1.InsertAllOnSubmit ns
                context.NeighbouringCells.InsertOnSubmit(n)
                context.GsmCellLocations.InsertOnSubmit(ob)
        | _ -> ()

let insertDataConnectionState (context : DAO.t) logEntry data = 
    let text = utf8.GetString(System.Convert.FromBase64String(data))
    let ob = JsonConvert.DeserializeObject<DataConnectionState>(text)
    ob.LogEntry <- logEntry

let insertServiceState (context : DAO.t) logEntry data = 
    let text = utf8.GetString(System.Convert.FromBase64String(data))
    let ob = JsonConvert.DeserializeObject<ServiceState>(text)
    ob.LogEntry <- logEntry

let insertSignalStrength (context : DAO.t) logEntry data = 
    let text = utf8.GetString(System.Convert.FromBase64String(data))
    let ob = JsonConvert.DeserializeObject<SignalStrength>(text)
    ob.LogEntry <- logEntry

let insertWifi (context : DAO.t) logEntry data =
    let text = utf8.GetString(System.Convert.FromBase64String(data))
    let obs = JsonConvert.DeserializeObject<WifiScanResult[]>(text) 
    obs |> Array.iter (fun o -> o.LogEntry <- logEntry)
    context.WifiScanResults.InsertAllOnSubmit(obs)

let insertPosition (context : DAO.t) logEntry data = 
    let text = utf8.GetString(System.Convert.FromBase64String(data))
    let o = JObject.Parse(text)
    match ((string)o.["event"]) with
        | "statusChanged" -> 
                let ob = new PositionStatusChanged()
                ob.LogEntry <- logEntry
                ob.provider <- (string)o.["provider"]
                ob.status <- (string)o.["status"]
                ob.extras <- (string)o.["extras"]
                context.PositionStatusChangeds.InsertOnSubmit(ob)
        | "enabled" ->
                let ob = new PositionProviderEnabledDisabled()
                ob.LogEntry <- logEntry
                ob.provider <- (string)o.["provider"]
                ob.isEnabled <- true
                context.PositionProviderEnabledDisableds.InsertOnSubmit(ob)
        | "disabled" ->
                let ob = new PositionProviderEnabledDisabled()
                ob.LogEntry <- logEntry
                ob.provider <- (string)o.["provider"]
                ob.isEnabled <- false
                context.PositionProviderEnabledDisableds.InsertOnSubmit(ob)
        | "locationChanged" ->
                let ob = new PositionLocationChanged()
                ob.LogEntry <- logEntry
                ob.provider <- (string)o.["provider"]
                if o.["location"].["accuracy"] <> null
                then ob.accuracy <- new System.Nullable<float>(System.Double.Parse(o.["location"].["accuracy"].ToString()))
                if o.["location"].["altitude"] <> null 
                then ob.altitude <- new System.Nullable<float>(System.Double.Parse(o.["location"].["altitude"].ToString()))
                if o.["location"].["bearing"] <> null 
                then ob.bearing <- new System.Nullable<float>(System.Double.Parse(o.["location"].["bearing"].ToString()))
                ob.latitude <- (float)o.["location"].["latitude"]
                ob.longitude <- (float)o.["location"].["longitude"]
                if o.["location"].["speed"] <> null
                then ob.speed <- new System.Nullable<float>(System.Double.Parse(o.["location"].["speed"].ToString()))
                ob.time <- new System.Nullable<int64>((int64)o.["location"].["time"])
                if o.["location"].["time"] <> null
                then ob.extras <- (string)o.["location"].["extras"]
                context.PositionLocationChangeds.InsertOnSubmit(ob)
        | _ -> ()

let insertUnknown (context : DAO.t) logEntry data = 
    let u = new Unrecognised()
    u.LogEntry <- logEntry
    let storageAccount = CloudStorageAccount.FromConfigurationSetting("DataConnectionString")
    let blobClient = CloudStorageAccountStorageClientExtensions.CreateCloudBlobClient(storageAccount)
    let blobContainer = blobClient.GetContainerReference(ServiceRuntime.RoleEnvironment.GetConfigurationSettingValue("UnrecognisedDataContainer"))
    blobContainer.CreateIfNotExist() |> ignore
    let id = System.Guid.NewGuid().ToString()
    let blob = blobContainer.GetBlockBlobReference(id)
    blob.UploadText(data)
    u.blobUri <- blob.Uri.ToString()
    context.Unrecogniseds.InsertOnSubmit(u)

let getIdentifier uniqueID version (context : DAO.t) =
    try
        context.LoggerIdentifiers.Single(fun (i : LoggerIdentifier) -> i.uniqueID.Equals(uniqueID) && i.version = version)
    with _ -> 
        let li = new LoggerIdentifier()
        li.uniqueID <- uniqueID
        li.version <- version
        context.LoggerIdentifiers.InsertOnSubmit li
        li

let parseLine line openID =
    let o = JObject.Parse(line)
    let context = DAO.get()
    let logEntry = new LogEntry()
    let user = context.Users.Single(new System.Func<_,_>(fun (u : User) -> u.claimedIdentifier.Equals(openID)))
    logEntry.User <- user
    let identifier = getIdentifier ((string)o.["identifier"].["uniqueID"]) ((int)o.["identifier"].["version"]) context
    logEntry.LoggerIdentifier <- identifier
    let timestamp = new Timestamp()
    timestamp.systemTime <- (int64)o.["timestamp"].["systemTime"]
    timestamp.timezone <- (string)o.["timestamp"].["timezone"]
    context.Timestamps.InsertOnSubmit timestamp
    logEntry.Timestamp <- timestamp
    match o.["location"] with
        | x when x.Type = JTokenType.Null -> ()
        | _ as loc ->
            let location = new Location()
            location.accuracy <- new System.Nullable<float>((float)loc.["accuracy"])
            location.altitude <- new System.Nullable<float>((float)loc.["altitude"])
            location.bearing <- new System.Nullable<float>((float)loc.["bearing"])
            location.latitude <- (float)loc.["latitude"]
            location.longitude <- (float)loc.["longitude"]
            location.provider <- (string)loc.["provider"]
            location.speed <- new System.Nullable<float>((float)loc.["speed"])
            location.time <- new System.Nullable<int64>((int64)loc.["time"])
            location.extras <- loc.["extras"].ToString()
            context.Locations.InsertOnSubmit location
            logEntry.Location <- location
    context.LogEntries.InsertOnSubmit logEntry
    (match identifier.uniqueID with
        | "aethers.notebook.logger.managed.celllocation.CellLocationLogger" -> insertCellLocation
        | "aethers.notebook.logger.managed.dataconnectionstate.DataConnectionStateLogger" -> insertDataConnectionState
        | "aethers.notebook.logger.managed.servicestate.ServiceStateLogger" -> insertServiceState
        | "aethers.notebook.logger.managed.signalstrength.SignalStrengthLogger" -> insertSignalStrength
        | "aethers.notebook.logger.managed.wifi.WifiLogger" -> insertWifi
        | "aethers.notebook.logger.managed.position.PositionLogger" -> insertPosition
        | _ -> insertUnknown) context logEntry ((string)o.["data"])
     
    context.SubmitChanges()
    DAO.release context

let store (msg : CloudQueueMessage) =
    let storageAccount = CloudStorageAccount.FromConfigurationSetting("DataConnectionString")
    let blobClient = CloudStorageAccountStorageClientExtensions.CreateCloudBlobClient(storageAccount)
    let blobContainer = blobClient.GetContainerReference(ServiceRuntime.RoleEnvironment.GetConfigurationSettingValue("UploadedDataContainer"))
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
    processLine (new StreamReader(new GZipStream(new BufferedStream(stream), CompressionMode.Decompress), System.Text.Encoding.UTF8))
    blob.Delete()

let addOrUpdateUser (claimedID : string) (friendlyID : string) : User =
    let context = DAO.get()
    try
        let user =
                context.Users.SingleOrDefault(
                        new System.Func<_,_>(fun (u : User) -> u.claimedIdentifier.Equals(claimedID)))
        match user with
            | null ->
                let u = new User()
                u.claimedIdentifier <- claimedID
                u.friendlyIdentifier <- friendlyID
                u.secret <- System.Guid.NewGuid().ToString()
                context.Users.InsertOnSubmit u
                context.SubmitChanges()
                u
            | _ -> 
                if user.friendlyIdentifier.Equals(friendlyID) |> not
                then
                    user.friendlyIdentifier <- friendlyID
                    context.SubmitChanges()
                user
    finally
        DAO.release context

let verifyUser (openID : string) (secret : string) : bool =
    let context = DAO.get()
    try
        try
            let user = context.Users.Single(
                                            System.Func<_,_>(
                                                fun (u : User) -> 
                                                                u.claimedIdentifier.Equals(openID) 
                                                                && u.secret.Equals(secret)))
            true
        with
            | _ -> false
    finally
        DAO.release context