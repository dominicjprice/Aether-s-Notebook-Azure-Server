#light

namespace Aethers.Notebook.Controllers

open System.Web.Mvc

module DA = Aethers.Notebook.DataAccess

[<HandleError>]
type AJAXController() =
    inherit Controller()
    
    do()

    [<Authorize>]
    member this.UpdateDisplayName(name : string) =
        DA.updateDisplayName System.Web.HttpContext.Current.User.Identity.Name name
        new EmptyResult()