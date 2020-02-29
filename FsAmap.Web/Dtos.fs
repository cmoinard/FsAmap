module FsAmap.Web.Dtos

open System
open FsAmap.Domain

type QuantitéDeProduitDto =
    {
        produit: Produit
        qteUnitaire: Nullable<int>
        qteAuPoids: Nullable<decimal>
    }
type PanierDuJourDto =
    {
        prix: decimal
        produits: QuantitéDeProduitDto list
    }

let produitToDto (produit: Produit) (quantité: Quantité) =
    let (qteUnitaire, qteAuPoids) =
        match quantité with
        | ``À l'unité`` q -> (Some q, None)
        | ``Au poids`` q -> (None, Some (decimal q))
        
    {
        produit = produit
        qteUnitaire = qteUnitaire |> Option.toNullable
        qteAuPoids = qteAuPoids |> Option.toNullable
    }

let panierDuJourToDto (panier: PanierDuJour) =
    {
        prix =  panier.prix
        produits =
            panier.produits
            |> List.map (fun (p, q) -> produitToDto p q)
    }