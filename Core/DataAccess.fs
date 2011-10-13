#light

module Aethers.Notebook.DataAccess

open Microsoft.FSharp.Linq.QuotationEvaluation

let toLambdaExpression (expr : Microsoft.FSharp.Quotations.Expr) = 
    let e = expr.ToLinqExpression() :?> System.Linq.Expressions.MethodCallExpression
    e.Arguments.[0] :?> System.Linq.Expressions.LambdaExpression

module DataContext =
    type t = Aethers.Notebook.DataAccess.ModelDataContext

    let get () = new t(Microsoft.WindowsAzure.ServiceRuntime.RoleEnvironment
                .GetConfigurationSettingValue("DataContextConnectionString"))                    

    let release (context : t) = context.Dispose()