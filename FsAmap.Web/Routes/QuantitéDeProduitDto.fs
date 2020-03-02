namespace FsAmap.Web.Routes

open System
open FsAmap.Domain

type QuantitéDeProduitDto =
    {
        produit: Produit
        quantitéUnitaire: Nullable<int>
        quantitéAuPoids: Nullable<decimal>
    }
    
module QuantitéDeProduitDto =
    let toQuantitéProduit dto =
        let quantité =
            let qteUnitaire = dto.quantitéUnitaire |> Option.ofNullable
            let qteAuPoids = dto.quantitéAuPoids |> Option.ofNullable
            match qteUnitaire, qteAuPoids with
            | Some u, _ -> ``À l'unité`` u
            | _, Some p -> ``Au poids`` (1m<kg> * p)
            | _ -> invalidOp "Non pris en charge"
        
        (dto.produit, quantité)
        
    let fromQuantitéProduit (quantitéDeProduit: Produit * Quantité) =
        let (produit, quantité) = quantitéDeProduit
        
        let (qteUnitaire, qteAuPoids) =
            match quantité with
            | ``À l'unité`` q -> (Some q, None)
            | ``Au poids`` q -> (None, Some (decimal q))
            
        {
            produit = produit
            quantitéUnitaire = qteUnitaire |> Option.toNullable
            quantitéAuPoids = qteAuPoids |> Option.toNullable
        }