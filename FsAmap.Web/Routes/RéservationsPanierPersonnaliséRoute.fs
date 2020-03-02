module FsAmap.Web.RéservationsPanierPersonnaliséRoute

open System.Threading.Tasks
open Saturn
open FSharp.Control.Tasks.V2

open FsAmap.Domain
open FsAmap.Infra.InMemory
open FsAmap.Web.Helpers
open FsAmap.Web.Routes


type RéservationPanierPersonnaliséDto = {
    clientId: ClientId
    panier: QuantitéDeProduitDto list
}

let private fromDto (réservation: RéservationPanierPersonnaliséDto) : RéservationPanierPersonnalisé =
    {
        clientId = réservation.clientId
        panier =
            réservation.panier
            |> List.map QuantitéDeProduitDto.toQuantitéProduit
    }
    
let private toDto (réservation: RéservationPanierPersonnalisé) =
    {
        clientId = réservation.clientId
        panier =
            réservation.panier
            |> List.map QuantitéDeProduitDto.fromQuantitéProduit
        
    }

let private getRéservations () =
    task {
        do! Task.Delay 300
        
        let réservations =
            InMemoryReservationPanierPersonnalise.lister ()
            |> List.map toDto
        
        return réservations
    }

let private réserver réservation =    
    task {
        do! Task.Delay 300
        
        réservation
        |> fromDto
        |> InMemoryReservationPanierPersonnalise.réserver
    }
       
let route =
    router {
        get "" (getRéservations |> toJson)
        post "" (réserver |> withJsonBody)
    }
