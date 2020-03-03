module FsAmap.Infra.Sql.SqlProduits

open FSharp.Data
open FsAmap.Domain
    
let lister () : Async<ProduitEnVrac list> =
    async {
        use cmd = new SqlCommandProvider<"SELECT id, nom, prix FROM Produits", ConnectionString.amap>(ConnectionString.amap)

        let! produitsDb = cmd.AsyncExecute ()
        
        let produits =
            produitsDb
            |> Seq.toList
            |> List.map (fun r -> { id = r.id; nom = r.nom }, r.prix)
        
        return produits
    }
    
