module FsAmap.Web.ReservationsRoutes

open System.Threading.Tasks
open Saturn
open FsAmap.Domain
open FsAmap.Web.Helpers
open FSharp.Control.Tasks.V2
open FsAmap.Infra

let patates = { id = 1; nom = "Patates" }
let oignons = { id = 2; nom = "Oignons" }
let œufs = { id = 3; nom = "Œufs" }
    
let panierDuJour: PanierDuJour =
    {
        prix = 10m
        produits = [
            patates, ``Au poids`` 2m<kg>
            oignons, ``Au poids`` 0.7m<kg>
            œufs, ``À l'unité`` 12
        ]
    }
    
let getPanierDuJour () =
    task {
        do! Task.Delay(200)
        return panierDuJour |> Dtos.panierDuJourToDto
    }

let getReservations (lister: ListerRéservations) () =
    lister ()
    |> Async.StartAsTask

let reserverPanierDuJour
    (reserver: RéserverPanierDuJour)
    (reservation: ReservationPanierDuJour) =    
    task {
        do! reserver reservation
            
        return reservation
    }
       
    
let route =
    router {
        get "" (
            getReservations InMemoryPanierDuJour.lister
            |> toJson
        )
        
        post "/panierDuJour" (
            reserverPanierDuJour InMemoryPanierDuJour.reserver
            |> withJsonBody
        )
    }
