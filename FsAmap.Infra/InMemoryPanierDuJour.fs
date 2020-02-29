module FsAmap.Infra.InMemoryPanierDuJour

open FsAmap.Domain

let mutable private réservations: ReservationPanierDuJour list = []

let lister: ListerRéservations =
    fun () ->
        async {
            do! Async.Sleep 200
            return réservations
        }

let reserver: RéserverPanierDuJour =
    fun (reservation: ReservationPanierDuJour) ->
        async {
            do! Async.Sleep 200
         
            réservations <-
                réservations
                |> List.append [reservation] 
        }
