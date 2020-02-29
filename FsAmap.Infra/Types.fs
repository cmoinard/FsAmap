namespace FsAmap.Infra

open FsAmap.Domain

type RéserverPanierDuJour = ReservationPanierDuJour -> Async<unit>
type ListerRéservations = unit -> Async<ReservationPanierDuJour list>

