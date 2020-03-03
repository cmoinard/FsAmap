module FsAmap.Infra.Sql.SqlReservationPanierDuJour

open FSharp.Data
open FsAmap.Domain
    
let réserver (réservation: RéservationPanierDuJour) =
    async {
        use cmd =
            new SqlCommandProvider<"
                INSERT INTO ReservationPanierDuJour (clientId, quantite)
                VALUES (@clientId, @quantite)
                ", ConnectionString.amap>(ConnectionString.amap)
        
        let! _ = cmd.AsyncExecute(clientId = réservation.clientId, quantite = réservation.quantité)
        
        return ()
    }
    
let lister () : Async<RéservationPanierDuJour list> =
    async {
        use cmd =
            new SqlCommandProvider<"
                SELECT clientId, quantite
                FROM ReservationPanierDuJour
                ", ConnectionString.amap>(ConnectionString.amap)

        let! reservationsDb = cmd.AsyncExecute ()
        
        let reservations =
            reservationsDb
            |> Seq.toList
            |> List.map (fun r -> { clientId = r.clientId; quantité = r.quantite})
        
        return reservations
    }

