#light

namespace Aethers.Notebook.Controllers

open Aethers.Notebook.Model
open DotNetOpenAuth.OpenId
open DotNetOpenAuth.Messaging
open DotNetOpenAuth.OpenId.RelyingParty

module Storage = Aethers.Notebook.Storage

type LoginResult = { openID : string; secret : string; friendlyID : string }

[<System.Web.Mvc.HandleError>]
type APIController() =
    inherit System.Web.Mvc.Controller()
    
    static let relyingParty = new OpenIdRelyingParty()
    static let encoding = new System.Text.UTF8Encoding()

    do()

    [<System.Web.Mvc.HttpGet>]
    //[<System.Web.Mvc.RequireHttpsAttribute>]
    member this.AuthenticateOpenID() =
       let response = relyingParty.GetResponse()
       match response.Status with
        | AuthenticationStatus.Authenticated -> 
            let claimedID = response.ClaimedIdentifier.ToString()
            let friendlyID = response.FriendlyIdentifierForDisplay
            let user = Storage.addOrUpdateUser claimedID friendlyID
            this.Response.StatusCode <- 200
            this.Response.StatusDescription <- "OK"
            this.Response.ContentType <- "application/json"
            this.Response.Charset <- "UTF-8"
            let action = new System.Web.Mvc.ContentResult()
            action.ContentEncoding <- encoding
            action.ContentType <- "application/json"
            action.Content <- Newtonsoft.Json.JsonConvert.SerializeObject({ openID = user.claimedIdentifier; secret = user.secret; friendlyID = user.friendlyIdentifier; })
            action :> System.Web.Mvc.ActionResult
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
                    relyingParty.CreateRequest(id).RedirectingResponse.AsActionResult()
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