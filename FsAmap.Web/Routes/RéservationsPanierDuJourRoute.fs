module FsAmap.Web.RéservationsPanierDuJourRoute

open System.Threading.Tasks
open Saturn
open FSharp.Control.Tasks.V2

open FsAmap.Infra.InMemory
open FsAmap.Web.Helpers

let private getRéservations () =
    task {
        do! Task.Delay 300
        return InMemoryReservationPanierDuJour.lister ()
    }

let private réserver réservation =    
    task {
        do! Task.Delay 300
        InMemoryReservationPanierDuJour.réserver réservation
    }
       
let route =
    router {
        get "" (getRéservations |> toJson)
        post "" (réserver |> withJsonBody)
    }
