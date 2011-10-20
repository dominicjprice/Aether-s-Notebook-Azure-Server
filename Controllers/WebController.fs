#light

namespace Aethers.Notebook.Controllers

open Aethers.Notebook.Model
open DotNetOpenAuth.OpenId
open DotNetOpenAuth.Messaging
open DotNetOpenAuth.OpenId.RelyingParty

module Storage = Aethers.Notebook.Storage

[<System.Web.Mvc.HandleError>]
type WebController() =
    inherit System.Web.Mvc.Controller()

    static let SESSION_VARIABLE_USER = "user"
    static let relyingParty = new OpenIdRelyingParty()

    do()
    
    member this.Index() = 
        this.View()

    member this.LogOn() =
        this.View()

    member this.LogOff() =
        System.Web.Security.FormsAuthentication.SignOut()
        this.RedirectToAction("Index", "Web")

    [<System.Web.Mvc.HttpGet>]
    //[<System.Web.Mvc.RequireHttpsAttribute>]
    member this.AuthenticateOpenID() =
        let response = relyingParty.GetResponse()
        match response.Status with
            | AuthenticationStatus.Authenticated  -> 
              let claimedID = response.ClaimedIdentifier.ToString()
              let friendlyID = response.FriendlyIdentifierForDisplay
              let user = Storage.addOrUpdateUser claimedID friendlyID
              this.Session.[SESSION_VARIABLE_USER] <- user
              System.Web.Security.FormsAuthentication.SetAuthCookie(claimedID, false)
              this.RedirectToAction("Index", "Web") :> System.Web.Mvc.ActionResult
            | AuthenticationStatus.Canceled ->
              this.ModelState.AddModelError("OpenID", "Authentication cancelled at user request.")
              this.View("LogOn") :> System.Web.Mvc.ActionResult
            | AuthenticationStatus.Failed -> 
              this.ModelState.AddModelError("OpenID", "Your OpenID provider did not authenticate you, please check and try again.")
              this.ModelState.AddModelError("OpenID", response.Exception.Message)
              this.View("LogOn") :> System.Web.Mvc.ActionResult
            | _ -> new System.Web.Mvc.EmptyResult() :> System.Web.Mvc.ActionResult

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
                        this.ModelState.AddModelError("OpenID", e.Message)
                        this.View("LogOn", model) :> System.Web.Mvc.ActionResult
            | _ -> 
                this.ModelState.AddModelError("OpenID", 
                        "Sorry, we were unable to determine your OpenID provider, please check and try again.");
                this.View("LogOn", model) :> System.Web.Mvc.ActionResult