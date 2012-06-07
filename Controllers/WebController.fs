#light

namespace Aethers.Notebook.Controllers

open Aethers.Notebook.Model

open System.Web.Mvc

module DA = Aethers.Notebook.DataAccess

[<HandleError>]
type WebController() =
    inherit OpenIDController()

    do()
    
    member this.Index() =
        match this.Request.IsAuthenticated with
            | false -> this.IndexUnauthenticated ()
            | true -> this.IndexAuthenticated ()

    [<Authorize>]
    member this.IndexAuthenticated() =
        this.View("IndexAuthenticated")

    member this.IndexUnauthenticated() =
        this.View("IndexUnauthenticated")

    [<Authorize>]
    member this.Profile() =
        let u = DA.getUserByOpenID System.Web.HttpContext.Current.User.Identity.Name
        if u.IsSome then
            this.ViewData.["UsersCatalogs"] <- DA.getUsersCatalogs u.Value
            if u.Value.displayName = null || u.Value.displayName.Length = 0 then
                u.Value.displayName <- "Not given"
        else
            this.ViewData.["UsersCatalogs"] <- List.Empty |> List.toSeq
        this.ViewData.["User"] <- u.Value
        this.View()

    override this.LogOn() =
        this.RedirectToAction("Index") :> ActionResult

    override this.PostLogOnAction id friendlyID =
        System.Web.Security.FormsAuthentication.SetAuthCookie(id, false)
        let (user, isNew) = DA.addOrUpdateUser id friendlyID
        this.TempData.["IsNew"] <- isNew
        this.RedirectToAction("Index") :> ActionResult

    override this.PostLogOffAction 
        with get () = 
            System.Web.Security.FormsAuthentication.SignOut()
            this.RedirectToAction("Index") :> ActionResult