module FsAmap.Web.Routes.ProduitsRoute

open Saturn
open FSharp.Control.Tasks.V2

open FsAmap.Domain
open FsAmap.Infra.Sql
open FsAmap.Web.Helpers

type ProduitVracDto =
    {
        produit: Produit
        prix: Prix
    }
    
let private toDto (produitVrac: ProduitEnVrac) =
    let (produit, prix) = produitVrac
    {
        produit = produit
        prix = prix
    }

let private getProduits () =
    task {
        let! produits = SqlProduits.lister ()
        
        let dtos =
            produits
            |> List.map toDto
            
        return dtos
    }

let route =
    router {
        get "" (getProduits |> toJson)
    }