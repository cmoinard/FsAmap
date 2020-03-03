module FsAmap.Web.RéservationsPanierPersonnaliséRoute

open Saturn
open FSharp.Control.Tasks.V2

open FsAmap.Domain
open FsAmap.Infra.Sql
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
        let! réservationsDb = SqlReservationPanierPersonnalise.lister ()
        
        let réservations =
            réservationsDb
            |> List.map toDto
        
        return réservations
    }

let private réserver réservation =    
    task {
        let dto =  
            réservation
            |> fromDto
        
        do! SqlReservationPanierPersonnalise.réserver dto
    }
       
let route =
    router {
        get "" (getRéservations |> toJson)
        post "" (réserver |> withJsonBody)
    }
