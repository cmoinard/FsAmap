module FsAmap.Infra.InMemory.InMemoryPanierDuJour

open FsAmap.Domain
open FsAmap.Infra.InMemory.Produits

let panierDuJour () : PanierDuJour =
    {
        prix = 10m
        produits = [
            patates, ``Au poids`` 2.5m<kg>
            oignons, ``Au poids`` 0.8m<kg>
            poireaux, ``Au poids`` 0.75m<kg>
            œufs, ``À l'unité`` 6        
        ]
    }