#light

namespace Aethers.Notebook.Controllers

open DotNetOpenAuth.OpenId
open DotNetOpenAuth.Messaging
open DotNetOpenAuth.OpenId.RelyingParty

type ErrorMessages = {
        Cancelled : string
        Failed: string
        Unknown: string
}

type OpenIdLogOn =
    new : unit -> OpenIdLogOn
    member OpenID : string
    member OpenID : string with set

[<AbstractClass>]
type OpenIDController =
    inherit System.Web.Mvc.Controller
    new : ErrorMessages -> OpenIDController
    new : unit -> OpenIDController
    abstract LogOn : unit -> System.Web.Mvc.ActionResult
    abstract PostLogOnAction : string -> string -> System.Web.Mvc.ActionResult
    abstract PostLogOffAction : System.Web.Mvc.ActionResult with get
    member AuthenticateOpenID : OpenIdLogOn -> System.Web.Mvc.ActionResult
    member AuthenticateOpenID : unit -> System.Web.Mvc.ActionResult
    member LogOff : unit -> System.Web.Mvc.ActionResult