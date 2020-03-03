module FsAmap.Infra.Sql.SqlPanierDuJour

open FSharp.Data
open FsAmap.Domain
open FsAmap.Infra.Sql

type private ProduitPanierDto =
    {
        produit: Produit
        quantitéAuPoids: decimal option
        quantitéUnitaire: int option
    }

let private créerQuantité qteU qteP =
    match qteU, qteP with
    | Some u, _ -> ``À l'unité`` u
    | _, Some p -> ``Au poids`` (1m<kg> * p)
    | _ -> invalidOp "Doit être soit au poids soit à l'unité"
    
let private panierFromDb () =
    async {        
        use cmdPanier =
            new SqlCommandProvider<"
                SELECT *
                FROM PanierDuJour
                ", ConnectionString.amap>(ConnectionString.amap)
                
        let! panierDb = cmdPanier.AsyncExecute ()
        let panier = panierDb |> Seq.head
        return panier
    }
    
let private produitsPanierFromDb panierId =
    async {        
        use cmdProduitsPanier =
            new SqlCommandProvider<"
                SELECT produitId, quantiteAuPoids, quantiteUnitaire, p.nom
                FROM ProduitsPanierDuJour
                JOIN Produits p ON p.Id = produitId
                WHERE panierId = @panierId
                ", ConnectionString.amap>(ConnectionString.amap)
                
        let! produitsPanierDb = cmdProduitsPanier.AsyncExecute(panierId = panierId)
        let produitsPanier =
            produitsPanierDb
            |> Seq.toList
            |> List.map (fun r ->
                {
                    produit = { id = r.produitId; nom = r.nom }
                    quantitéUnitaire = r.quantiteUnitaire
                    quantitéAuPoids = r.quantiteAuPoids
                })
            
        return produitsPanier
    }
    
let lister () : Async<PanierDuJour> =
    async {
        let! panier = panierFromDb ()
        let! produitsPanier = produitsPanierFromDb panier.id
        
        let panierDuJour =
            {
                prix = panier.prix
                produits =
                    produitsPanier
                    |> List.map (fun p -> p.produit, créerQuantité p.quantitéUnitaire p.quantitéAuPoids)
            }
        return panierDuJour
    }
