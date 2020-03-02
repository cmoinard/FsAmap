module FsAmap.Infra.InMemory.InMemoryReservationPanierDuJour

open FsAmap.Domain

let mutable private réservations: RéservationPanierDuJour list = []

let lister () =
    réservations
        
let réserver réservation =
    réservations <-
        réservations @ [réservation]