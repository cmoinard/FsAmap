module FsAmap.Web.PanierDuJourRoute

open System.Threading.Tasks
open Saturn
open FSharp.Control.Tasks.V2

open FsAmap.Domain
open FsAmap.Infra.InMemory
open FsAmap.Web.Helpers
open FsAmap.Web.Routes


type PanierDuJourDto =
    {
        prix: decimal
        produits: QuantitéDeProduitDto list
    }

let private toDto (panier: PanierDuJour) =
    {
        prix =  panier.prix
        produits =
            panier.produits
            |> List.map QuantitéDeProduitDto.fromQuantitéProduit
    }
    
let private getPanierDuJour () =
    task {
        do! Task.Delay(200)
        
        let panierDuJour =
            InMemoryPanierDuJour.panierDuJour ()
            |> toDto
            
        return panierDuJour
    }

let route =
    router {
        get "" (getPanierDuJour |> toJson)
    }
    