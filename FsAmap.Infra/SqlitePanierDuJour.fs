module FsAmap.Infra.SqlitePanierDuJour

open FsAmap.Domain
open FSharp.Data.Sql
open System.Data

let [<Literal>] connectionString = "Data Source=" + dbPath

type Sql = SqlDataProvider<
            ConnectionString = connectionString,
            DatabaseVendor = Common.DatabaseProviderTypes.MYSQL>

let lister: ListerRéservations =
    fun () ->
//        use cmd = new SqlCommandProvider<"
//            select * from Produits
//            ", connectionString>(connectionString)
        
        async {
//            let! values = cmd.AsyncExecute()
            
            return []
        }