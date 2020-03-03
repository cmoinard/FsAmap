module FsAmap.Infra.Sql.SqlReservationPanierPersonnalise

open System.Data.SqlClient

open FSharp.Data

open FsAmap.Domain
open FsAmap.Infra
open FsAmap.Infra.Sql

type AmapDb = SqlProgrammabilityProvider<ConnectionString.amap>
        
let réserver réservation =
    async {
        use reservationsTable = new AmapDb.dbo.Tables.ReservationProduitPanierPersonnalise()
     
        réservation.panier
        |> List.iter (fun (p,q) ->
            let (qteU, qteP) = Quantité.toTuple q
                
            reservationsTable.AddRow(réservation.clientId, p.id, qteP, qteU)
            )
        
        reservationsTable.BulkCopy(copyOptions = SqlBulkCopyOptions.TableLock)        
    }

let lister () : Async<RéservationPanierPersonnalisé list> =
    async {
        use cmd =
            new SqlCommandProvider<"
                SELECT clientId, quantiteAuPoids, quantiteUnitaire, produitId, p.nom
                FROM ReservationProduitPanierPersonnalise
                JOIN Produits p ON produitId = p.Id
                ", ConnectionString.amap>(ConnectionString.amap)

        let! reservationsDb = cmd.AsyncExecute ()
        
        let reservations =
            reservationsDb
            |> Seq.toList
            |> List.groupBy (fun r -> r.clientId)
            |> List.map (fun (clientId, reservations) ->                    
                {
                    clientId = clientId
                    panier =
                        reservations
                        |> List.map (fun r ->
                                let produit = { id = r.produitId; nom = r.nom }
                                let quantité = Quantité.créer r.quantiteUnitaire r.quantiteAuPoids
                                
                                produit, quantité
                            )
                })
        
        return reservations
    }
