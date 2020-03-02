module FsAmap.Web.Routes.ProduitsRoute

open System.Threading.Tasks
open Saturn
open FSharp.Control.Tasks.V2

open FsAmap.Domain
open FsAmap.Infra.InMemory
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
        do! Task.Delay(200)
        
        let panierDuJour =
            InMemoryProduits.lister ()
            |> List.map toDto
            
        return panierDuJour
    }

let route =
    router {
        get "" (getProduits |> toJson)
    }