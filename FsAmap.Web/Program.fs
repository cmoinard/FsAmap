open Saturn
open FsAmap.Web

let port = 8085us

let mainRouter =
    router {
        forward "/panierDuJour" PanierDuJourRoute.route
        forward "/reservations" ReservationsRoutes.route
    }

(*
GET /api/produits
    {
        id: int
        nom: string
        prix: decimal
        typePrix: unité | kg
    }[]

GET /api/panierDuJour
    prix: euro
    produits: {
        id: int
        nom: string
        quantité:
            unité: int | poidsEnKg: decimal
    }[]
    
POST /api/panierDuJour/reserver
    idClient: int
    quantité: int
*)
let app =
    application {
        use_router mainRouter
        url ("http://0.0.0.0:" + port.ToString() + "/")
        memory_cache
        use_gzip
    }

let exitCode = 0

[<EntryPoint>]
let main _ =        
    run app
    exitCode