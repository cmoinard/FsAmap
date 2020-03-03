module FsAmap.Web.PanierDuJourRoute

open Saturn
open FSharp.Control.Tasks.V2

open FsAmap.Domain
open FsAmap.Infra.Sql
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
        let! panierDuJour = SqlPanierDuJour.lister ()        
        return panierDuJour |> toDto
    }

let route =
    router {
        get "" (getPanierDuJour |> toJson)
    }
    