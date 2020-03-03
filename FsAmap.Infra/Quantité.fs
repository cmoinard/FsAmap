module FsAmap.Infra.Quantité

open FsAmap.Domain

let créer qteU qteP =
    match qteU, qteP with
    | Some u, _ -> ``À l'unité`` u
    | _, Some p -> ``Au poids`` (1m<kg> * p)
    | _ -> invalidOp "Doit être soit au poids soit à l'unité"
    
let toTuple =
    function
    | ``À l'unité`` u -> Some u, None
    | ``Au poids`` p -> None, Some (decimal p)
    