module FsAmap.Infra.InMemory.InMemoryReservationPanierPersonnalise

open FsAmap.Domain

let mutable private réservations: RéservationPanierPersonnalisé list = []

let lister () =
    réservations
        
let réserver réservation =
    réservations <-
        réservations @ [réservation]