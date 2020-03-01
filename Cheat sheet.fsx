// I) Types

// I.1) types primitifs

let i = 5 // int
let d = 3.2 // float
let m = 4.3m // decimal
let s = "toto" // string
let b = true // bool
let l = [1; 2] // int list (list est une liste chainée immuable)
let l2 =  [1..10] // int list de 1 à 10

s = "tutu" // false car = est l'opérateur d'égalité

let s' = "tutu"
let ``Nom de variable avec des espaces`` = "bonjour"

let i': int = 7

let unit' = () // équivalent de void

// Unités de mesure
type [<Measure>] km
type [<Measure>] h

let distance = 300.0<km>
let duree = 2.0<h>

// distance + duree // ne compile pas

let vitesse = distance / duree // = 150.0<km/h>


// I.2) Produits (tuples et records)

type PointTuple = int * int
let pointTuple = 1, 3
let x, y = pointTuple // x = 1, y = 3
let x', _ = pointTuple // _ = on s'en fout

type PointRecord = { 
    x: int
    y: int
} // ou type Point = { x: int; y: int }

let point = { 
    x = 1
    y = 3
} // ou let point = { x = 1; y = 3 }

let { x = xRecord; y = _ } = point

// I.3) Sommes (unions)

type Booleen = Vrai | Faux

type Shape =
    | Point
    | Circle of int
    | Square of int
    | Rectangle of int * int
    
let p = Point
let circle = Circle 3
let rectangle = Rectangle (2, 5)

// Équivalent de Nullable<'a>
(*
type Option<'a> =
    | Some of 'a
    | None
*)
let some1: Option<int> = Some 1
let none: int option = None

// Remplace les exceptions
type Result<'a, 'error> =
    | Ok of 'a
    | Error of 'error


// II) Fonctions

// II.1) Usages simples
// string -> string
let shout message = message + "!!1"
let shout' = fun message -> message + "!!1"

// int -> int -> int
let add x y = x + y
let addAlias = add
let (+++) x y = x + y // ou let (+++) = add
let douze = 10 +++ 2

let bonjour = shout "Bonjour"
let bonjour' = "Bonjour" |> shout

let de2a6 =
    [1..10] // [1;2;3;4;5;6;7;8;9;10]
    |> List.filter (fun i -> i <= 5) // [1;2;3;4;5]
    |> List.map ((+) 1) // [2;3;4;5;6]
    
// int -> int
let add1 x = x + 1

// int -> int
let mult3 x = x * 3

let add1mult3 x =
    x |> add1 |> mult3

// int -> int
let add1mult3' = add1 >> mult3
// ('a -> 'b) >> ('b -> 'c) = ('a -> 'c)

// II.2) Modules et namespace

(*
namespace MyNamespace

type MyType = string * string

module Toto =
    type MyType2 = int * int
    
    let myFun () = "toto"
*)

(* ou
module MyNamespace.Toto

type MyType = int * int

let myFun () = "toto"
*)



// II.3) Conditionnelles

let salutations prenom =
    if prenom <> "Christophe" then
        sprintf "Bonjour %s" prenom
    else
        "Oh vous etes grand ! Vous mesurez combien ? Vous faites du basket ?"


// if-then-else = C# ? :
let booleenStringIfThenElse =
    if b then "Vrai" else "Faux"

let booleenToString b =
    match b with
    | Faux -> "C'est faux"
    | Vrai -> "C'est pas faux"
    
let booleenToString' =
    function
    | Faux -> "C'est faux"
    | Vrai -> "C'est pas faux"
    
let fizzbuzz i =
    match i % 3, i % 5 with
    | 0, 0 -> "FizzBuzz"
    | 0, _ -> "Fizz"
    | _, 0 -> "Buzz"
    | _ -> string i


let shapeToString =
    function
    | Point -> "Point"
    | Circle rayon -> sprintf "Cercle de rayon %i" rayon
    | Square 3 -> "Superbe carré de 3"
    | Square x when x > 10 -> sprintf "Très gros carré de %i" x
    | Square _ -> "Carré"
    | Rectangle (3, 14) -> "Rectangle pi !"
    | Rectangle (3, y) -> sprintf "Joli rectangle de 3×%i" y
    | Rectangle (x,y) -> sprintf "Rectangle de %i×%i" x y
    
// II.4) Partialisation

let increment = add 1

let add' = fun x y -> x + y
let add'' =
    fun x ->
        fun y -> x + y
        
let treize = add 3 10
let treize' = 10 |> add 3


