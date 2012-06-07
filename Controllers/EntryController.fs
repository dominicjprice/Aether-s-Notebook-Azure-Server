#light

namespace Aethers.Notebook.Controllers

[<System.Web.Mvc.HandleError>]
type EntryController() =
    inherit System.Web.Mvc.Controller()
    
    do ()

    member this.Index() =
        new System.Web.Mvc.RedirectResult("~/Web/Index")