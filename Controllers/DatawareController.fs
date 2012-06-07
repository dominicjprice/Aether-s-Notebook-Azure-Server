#light

namespace Aethers.Notebook.Controllers

open System.Web.Mvc

module DW = Aethers.Notebook.Dataware

[<HandleError>]
type DatawareController () =
    inherit Controller()

    static let encoding = System.Text.Encoding.UTF8

    let openID () = System.Web.HttpContext.Current.User.Identity.Name

    [<Authorize>]
    [<HttpPost>]
#if HTTPS
    [<RequireHttpsAttribute>]
#endif
    member this.Install (catalogUri : string) =
        if DW.isRegisteredWithCatalog catalogUri |> not then
            let (success, m1, m2) = DW.registerWithCatalog catalogUri
            if not success then 
                raise (new System.Exception())
        new RedirectResult(
            DW.createUserCatalog 
                (openID ())
                catalogUri)

    [<Authorize>]
#if HTTPS
    [<RequireHttpsAttribute>]
#endif
    member this.Install_Complete () =
        DW.completeInstallation (openID()) this.Request.QueryString
        new RedirectResult("/Web/Profile")

    [<HttpPost>]
#if HTTPS
    [<RequireHttpsAttribute>]
#endif
    member this.Permit_Processor () =
        let tr = this.Request
        let it = tr.["install_token"]
        let ci = tr.["client_id"]
        let q = tr.["query"]
        let et = System.Convert.ToDouble(tr.["expiry_time"])
        let response = DW.registerQueryRequest it ci q et
        let r = new ContentResult()
        r.Content <-
            match response with
                | DW.RegisterFailure f ->
                    Newtonsoft.Json.JsonConvert.SerializeObject(f)
                | DW.RegisterSuccess s ->
                    Newtonsoft.Json.JsonConvert.SerializeObject(s)
        r.ContentType <- "application/json"
        r.ContentEncoding <- encoding
        r

#if HTTPS
    [<RequireHttpsAttribute>]
#endif
    member this.Invoke_Processor () =
        let response = DW.invokeQueryRequest
                        (this.Request.["access_token"])
                        (this.Request.["parameters"])
        // if (this.Request.["response_uri"]) != null then do asynchronous processing
        let r = new ContentResult()
        r.Content <-
            match response with
                | DW.InvokeFailure f ->
                    Newtonsoft.Json.JsonConvert.SerializeObject(f)
                | DW.InvokeSuccess s ->
                    Newtonsoft.Json.JsonConvert.SerializeObject(s)
        r.ContentType <- "application/json"
        r.ContentEncoding <- encoding
        r