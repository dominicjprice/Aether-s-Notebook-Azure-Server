#light

module Aethers.Notebook.Dataware

open Aethers.Notebook.Model

open Newtonsoft.Json.Linq

open System
open System.IO
open System.Net

module Conf = Aethers.Notebook.Configuration
module DA = Aethers.Notebook.DataAccess
module Log = Aethers.Notebook.Log
module SB = Aethers.Notebook.TrustedInterpreter

type RegisterQueryResponseSuccess = { success : bool; access_token : string }
type RegisterQueryResponseFailure = { success : bool; error : string; error_description : string }
type RegisterQueryResponse =
    | RegisterSuccess of RegisterQueryResponseSuccess
    | RegisterFailure of RegisterQueryResponseFailure

type InvokeQueryResponseSuccess = { success : bool; response : string }
type InvokeQueryResponseFailure = { success : bool; error : string; error_description : string }
type InvokeQueryResponse =
    | InvokeSuccess of InvokeQueryResponseSuccess
    | InvokeFailure of InvokeQueryResponseFailure

let inline urlEncode (s : string) =
    System.Web.HttpUtility.UrlEncode(s)

let inline getUtf8Bytes (str : string) =
    let b = Text.Encoding.UTF8.GetBytes(str)
    (b, Array.length b)

let (REGISTER_DATA, REGISTER_DATA_LENGTH) = 
        sprintf "resource_name=%s&redirect_uri=%s&namespace=%s&description=%s&logo_uri=%s&web_uri=%s" 
                (urlEncode(Conf.value("Aethers.Notebook.Dataware.ResourceName")))
                (urlEncode(Conf.value("Aethers.Notebook.Dataware.ResourceUri")))
                (urlEncode(Conf.value("Aethers.Notebook.Dataware.Namespace")))
                (urlEncode(Conf.value("Aethers.Notebook.Dataware.Description")))
                (urlEncode(Conf.value("Aethers.Notebook.Dataware.LogoUri")))
                (urlEncode(Conf.value("Aethers.Notebook.Dataware.WebUri"))) |> getUtf8Bytes

let setRequestDefaults (req : HttpWebRequest) =
    req.KeepAlive <- false
    req.Headers.Set("Pragma", "no-cache")
    req.Timeout <- 300000

let isRegisteredWithCatalog (catalogUri : string) =
    match DA.getCatalogByUri catalogUri with
        | Some(_) -> true
        | None -> false

let registerWithCatalog (catalogUri : string) = 
    let req = 
            WebRequest.Create(
                sprintf "%s/resource_register" catalogUri) :?> HttpWebRequest
    setRequestDefaults req
    req.Method <- "POST"
    req.ContentType <- "application/x-www-form-urlencoded"
    req.ContentLength <- int64 REGISTER_DATA_LENGTH
    let out = req.GetRequestStream()
    out.Write(REGISTER_DATA, 0, REGISTER_DATA_LENGTH)
    out.Close()
    let res = req.GetResponse()
    let r = new StreamReader(res.GetResponseStream())
    let body = r.ReadToEnd()
    let ro = JObject.Parse(body)
    res.Close()
    match ro.["success"].Value<bool>() with
        | true -> 
            let c = new Catalog()
            c.catalogUri <- catalogUri
            c.resourceID <- ro.["resource_id"].Value<string>()
            DA.insertCatalog c |> ignore
            (true, c.resourceID, None)
        | false -> (false, ro.["error"].Value<string>(), Some(ro.["error_description"].Value<string>()))

let createUserCatalog openID catalogUri =
    let uc = DA.createUserCatalog openID catalogUri
    sprintf "%s/resource_request?resource_id=%s&state=%s&redirect_uri=%s"
        catalogUri
        (urlEncode uc.Catalog.resourceID)
        (urlEncode uc.state)
        (urlEncode(Conf.value("Aethers.Notebook.Dataware.ResourceUri")))

let completeInstallation (openID : string) (query : System.Collections.Specialized.NameValueCollection) =
    let uc = DA.getUserCatalog query.["state"] openID UserCatalog.StatusCode.InstallationRequestSent
    match query.["error"] with
        | null -> 
            DA.markUserCatalogAccessTokenRequest uc query.["code"]
            let req = 
                    WebRequest.Create(
                        sprintf 
                            "%s/resource_access?grant_type=authorization_code&redirect_uri=%s&code=%s"
                            uc.Catalog.catalogUri
                            (urlEncode(Conf.value("Aethers.Notebook.Dataware.ResourceUri")))
                            (urlEncode query.["code"])
                    ) :?> HttpWebRequest
            setRequestDefaults req
            req.Method <- "GET"
            let res = req.GetResponse()
            let r = new StreamReader(res.GetResponseStream())
            let body = r.ReadToEnd()
            let ro = JObject.Parse(body)
            res.Close()
            match ro.["success"].Value<bool>() with
                | true -> 
                    DA.completeUserCatalogInstallation uc (ro.["access_token"].Value<string>())
                | false -> 
                    raise (new System.Exception(ro.["error_description"].Value<string>()))
        | _ ->
            DA.markUserCatalogInstallAsRejected uc
            raise (new System.Exception(query.["error_description"]))

let registerQueryRequest install_token client_id query (expiry_time : float) =
    try
        let uc = DA.getUserCatalogByAccessToken install_token
        let q = new ResourceQuery()
        q.userCatalogID <- uc.ID
        q.client_id <- client_id
        q.expiry_time <- (new System.DateTime(1970,1,1,0,0,0,0)).AddSeconds(expiry_time).ToLocalTime()
        q.processed <- false
        q.query <- query
        let at = System.Guid.NewGuid().ToString()
        q.access_token <- at
        DA.saveQueryRequest q
        RegisterSuccess { success = true; access_token = at}
    with
        | _ as e -> RegisterFailure { 
            success = false;
            error = "registration_failure";
            error_description = "Unauthorised request" }

let invokeQueryRequest access_token parameters =
    try
        let q = DA.getQueryRequestByAccessToken access_token
        if q.expiry_time < DateTime.UtcNow then
            InvokeFailure { 
            success = false;
            error = "expired";
            error_description = "The processor expiry time has passed" }
        else
            let run = 
                sprintf "import clr\nclr.AddReference(\"Model\")\nimport json\r\n%s%s" q.query
                    (match parameters with
                        | s when String.IsNullOrWhiteSpace(s) -> "\r\nrun({})"
                        | _ -> sprintf "\r\nrun(json.loads(\"%s\"))" (parameters.Replace("\"", "\\\"")))
            let response = SB.parseUntrusted Map.empty run :?> string
            InvokeSuccess { success = true; response = response }
    with _ as e -> 
        sprintf "%s\n%s" e.Message e.StackTrace |> Log.error 
        InvokeFailure { 
            success = false;
            error = "unknown_error";
            error_description = e.Message }