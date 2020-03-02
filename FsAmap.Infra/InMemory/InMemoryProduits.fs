module FsAmap.Infra.InMemory.InMemoryProduits

open FsAmap.Domain
open FsAmap.Infra.InMemory.Produits

let lister () : ProduitEnVrac list =
    [
        patates, 2.3m
        oignons, 3.4m
        poireaux, 4.5m
        œufs, 0.2m
    ]