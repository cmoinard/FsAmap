open Saturn

open FsAmap.Web.Routes
open FsAmap.Web

let port = 8085us

let réservationsRouter =
    router {
        forward "/panierDuJour" RéservationsPanierDuJourRoute.route
        forward "/panierPersonnalise" RéservationsPanierPersonnaliséRoute.route
    }
    
let mainRouter =
    router {
        forward "/produits" ProduitsRoute.route
        forward "/panierDuJour" PanierDuJourRoute.route
        forward "/reservations" réservationsRouter
    }
    
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