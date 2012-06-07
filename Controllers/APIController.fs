#light

namespace Aethers.Notebook.Controllers

open Aethers.Notebook.Model

open System.Web.Mvc

module DA = Aethers.Notebook.DataAccess

type LoginResult = { 
        openID : string
        secret : string
        friendlyID : string 
}

[<HandleError>]
type APIController() =
    inherit OpenIDController()
    
    static let encoding = System.Text.Encoding.UTF8

    do()

    override this.LogOn() =
        new EmptyResult() :> ActionResult

    override this.PostLogOnAction id friendlyID =
        let (user, isNew) = DA.addOrUpdateUser id friendlyID
        let action = new ContentResult()
        action.ContentEncoding <- encoding
        action.ContentType <- "application/json"
        action.Content <- 
                Newtonsoft.Json.JsonConvert.SerializeObject(
                    { 
                        openID = user.claimedIdentifier;
                        secret = user.secret;
                        friendlyID = user.friendlyIdentifier; 
                    })
        action :> ActionResult

    override this.PostLogOffAction 
        with get () = 
            new EmptyResult() :> ActionResult