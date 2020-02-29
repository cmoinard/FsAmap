namespace FsAmap.Domain

type Prix = decimal
type Produit =
    {
        id: int
        nom: string
    }

type [<Measure>] kg

type Quantité =
    | ``À l'unité`` of int
    | ``Au poids`` of decimal<kg>

type PanierDuJour =
    {
        prix: Prix
        produits: (Produit * Quantité) list
    }
    
type Panier =
    | PanierDuJour of PanierDuJour
    | PanierPersonnalisé of (Produit * Quantité * Prix) list 
