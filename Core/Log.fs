#light

module Aethers.Notebook.Log
      
let inline log kind message = System.Diagnostics.Trace.WriteLine(message, kind)

let verbose = log "Verbose"

let information = log "Information"

let warning = log "Warning"

let error = log "Error"

let critical = log "Critical"