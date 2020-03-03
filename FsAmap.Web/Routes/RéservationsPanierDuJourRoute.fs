module FsAmap.Web.RéservationsPanierDuJourRoute

open Saturn
open FSharp.Control.Tasks.V2

open FsAmap.Infra.Sql
open FsAmap.Web.Helpers

let private getRéservations () =
    task {
        return! SqlReservationPanierDuJour.lister ()
    }

let private réserver réservation =    
    task {
        do! SqlReservationPanierDuJour.réserver réservation
    }
       
let route =
    router {
        get "" (getRéservations |> toJson)
        post "" (réserver |> withJsonBody)
    }
