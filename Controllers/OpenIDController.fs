#light

namespace Aethers.Notebook.Controllers

open DotNetOpenAuth.OpenId
open DotNetOpenAuth.Messaging
open DotNetOpenAuth.OpenId.RelyingParty

open System.Web.Mvc

type ErrorMessages = {
        Cancelled : string
        Failed: string
        Unknown: string
}

type OpenIdLogOn () =
    let mutable openID = System.String.Empty
    do ()
    [<System.ComponentModel.DataAnnotations.Required>]
    member this.OpenID
        with get () = openID
        and  set o = openID <- o
        
[<AbstractClass>]
[<HandleError>]
type OpenIDController (messages : ErrorMessages) =
    inherit Controller()
    
    static let relyingParty = new OpenIdRelyingParty()

    do ()

    new () = new OpenIDController({ Cancelled = ""; Failed = ""; Unknown = ""})

    abstract LogOn : unit -> ActionResult

    abstract PostLogOnAction : string -> string -> ActionResult

    abstract PostLogOffAction : ActionResult with get   

#if HTTPS
    [<RequireHttpsAttribute>]
#endif
    member this.LogOff () =
        System.Web.Security.FormsAuthentication.SignOut()
        this.PostLogOffAction

    [<HttpGet>]
#if HTTPS
    [<RequireHttpsAttribute>]
#endif
    member this.AuthenticateOpenID () =
       let response = relyingParty.GetResponse()
       match response.Status with
        | AuthenticationStatus.Authenticated  -> 
            let claimedID = response.ClaimedIdentifier.ToString()
            let friendlyID = response.FriendlyIdentifierForDisplay
            this.PostLogOnAction claimedID friendlyID
        | AuthenticationStatus.Canceled ->
            this.ModelState.AddModelError("OpenID", messages.Cancelled)
            this.LogOn()
        | AuthenticationStatus.Failed -> 
            this.ModelState.AddModelError("OpenID", messages.Failed)
            this.ModelState.AddModelError("OpenID", response.Exception.Message)
            this.LogOn()
        | _ -> new EmptyResult() :> ActionResult

    [<HttpPost>]
#if HTTPS
    [<RequireHttpsAttribute>]
#endif
    member this.AuthenticateOpenID (identifier : OpenIdLogOn) =
        let mutable id = null
        match Identifier.TryParse(identifier.OpenID, &id) with
            | true -> 
                try 
                    relyingParty.CreateRequest(id).RedirectingResponse.AsActionResult()
                with
                    | _ as e ->
                        this.ModelState.AddModelError("OpenID", e.Message)
                        this.LogOn()
            | _ -> 
                this.ModelState.AddModelError("OpenID", messages.Unknown);
                this.LogOn()