namespace FsAmap.Domain

type ClientId = int

type RéservationPanierDuJour =
    {
        clientId: ClientId
        quantité: int
    }
    
type RéservationPanierPersonnalisé =
    {
        clientId: ClientId
        panier: PanierPersonnalisé
    }