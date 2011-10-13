#light

module Aethers.Notebook.Log

let log message kind = System.Diagnostics.Trace.WriteLine(message, kind)

let verbose message = log message "Verbose"

let information message = log message "Information"

let warning message = log message "Warning"

let error message = log message "Error"

let critical message = log message "Fatal"