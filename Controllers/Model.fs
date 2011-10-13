#light

namespace Aethers.Notebook.Model

type OpenIdLogOn() =
    let mutable openID = System.String.Empty
    do()
    [<System.ComponentModel.DataAnnotations.Required>]
    member this.OpenID
        with get() = openID
        and  set o = openID <- o
    