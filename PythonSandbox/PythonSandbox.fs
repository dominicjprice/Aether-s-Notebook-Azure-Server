#light

module Aethers.Notebook.PythonSandbox

open System.Linq.Expressions

let parse (variables : Map<string, obj>) (code : string) (paths : string list) =
    try
        let engine = IronPython.Hosting.Python.CreateRuntime().GetEngine("python")
        let spaths = new System.Collections.Generic.List<string>()
        paths |> List.iter (fun path -> spaths.Add(path))
        engine.SetSearchPaths(spaths)
        let scope = engine.CreateScope()
        variables |> Map.iter (fun k v -> scope.SetVariable(k, v))
        engine.Execute(code, scope)
    with _ as e ->
        e :> obj