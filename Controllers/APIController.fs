#light

namespace Aethers.Notebook.Controllers

open Aethers.Notebook.Model
open DotNetOpenAuth.OpenId
open DotNetOpenAuth.Messaging
open DotNetOpenAuth.OpenId.RelyingParty

module Storage = Aethers.Notebook.Storage

[<System.Web.Mvc.HandleError>]
type APIController() =
    inherit System.Web.Mvc.Controller()
    
    static let SESSION_VARIABLE_MODEL = "model"
    static let relyingParty = new OpenIdRelyingParty()

    do()

    [<System.Web.Mvc.HttpGet>]
    //[<System.Web.Mvc.RequireHttpsAttribute>]
    member this.AuthenticateOpenID() =
       let model = this.Session.[SESSION_VARIABLE_MODEL] :?> OpenIdLogOn
       this.Session.Remove(SESSION_VARIABLE_MODEL)
       let response = relyingParty.GetResponse()
       match response.Status with
        | AuthenticationStatus.Authenticated  -> 
            let claimedID = response.ClaimedIdentifier.ToString()
            let friendlyID = response.FriendlyIdentifierForDisplay
            let user = Storage.addOrUpdateUser claimedID friendlyID
            this.Response.StatusCode <- 200
            this.Response.StatusDescription <- "OK"
            this.Response.ContentType <- "application/json"
            this.Response.Charset <- "UTF-8"
            new System.Web.Mvc.EmptyResult() :> System.Web.Mvc.ActionResult
        | AuthenticationStatus.Canceled ->
            this.Response.StatusCode <- 400
            this.Response.StatusDescription <- "Authentication cancelled"
            this.Response.ContentType <- "application/json"
            this.Response.Charset <- "UTF-8"
            new System.Web.Mvc.EmptyResult() :> System.Web.Mvc.ActionResult
        | AuthenticationStatus.Failed -> 
            this.Response.StatusCode <- 401
            this.Response.StatusDescription <- "Authentication failed"
            this.Response.ContentType <- "application/json"
            this.Response.Charset <- "UTF-8"
            new System.Web.Mvc.EmptyResult() :> System.Web.Mvc.ActionResult
        | _ ->
            this.Response.StatusCode <- 500
            this.Response.StatusDescription <- "An unknown error occurred"
            this.Response.ContentType <- "application/json"
            this.Response.Charset <- "UTF-8"
            new System.Web.Mvc.EmptyResult() :> System.Web.Mvc.ActionResult

    [<System.Web.Mvc.HttpPost>]
    //[<System.Web.Mvc.RequireHttpsAttribute>]
    member this.AuthenticateOpenID(model : OpenIdLogOn) =
        let mutable id = null
        match Identifier.TryParse(model.OpenID, &id) with
            | true -> 
                try 
                    let a = relyingParty.CreateRequest(id).RedirectingResponse.AsActionResult()
                    this.Session.[SESSION_VARIABLE_MODEL] <- model
                    a
                with
                    | _ as e ->
                        this.Response.StatusCode <- 400
                        this.Response.StatusDescription <- e.Message
                        this.Response.ContentType <- "application/json"
                        this.Response.Charset <- "UTF-8"
                        new System.Web.Mvc.EmptyResult() :> System.Web.Mvc.ActionResult
            | _ -> 
                this.Response.StatusCode <- 400
                this.Response.StatusDescription <- "Unable to determine OpenID provider"
                this.Response.ContentType <- "application/json"
                this.Response.Charset <- "UTF-8"
                new System.Web.Mvc.EmptyResult() :> System.Web.Mvc.ActionResult