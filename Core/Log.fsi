#light

module Aethers.Notebook.Log

val verbose : (string -> unit)

val information : (string -> unit)

val warning : (string -> unit)

val error : (string -> unit)

val critical : (string -> unit)