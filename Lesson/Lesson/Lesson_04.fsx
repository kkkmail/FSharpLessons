// Operations with lists and arrays

// Lists
let x = [ for i in 0..10 -> i ]
//printfn "x = %A " x

let y = [ for i in 0..2..20 -> i ]
//printfn "y = %A " y

// Arrays
let a = x |> List.toArray
let b = y |> List.toArray


// Map
let x1 = x |> List.map (fun e -> e * e)
//printfn "x1 = %A " x1

let x2 = x |> List.map (fun e -> (float e) / 10.0)
//printfn "x2 = %A " x2


type Cat =
    {
        lives : int
    }


type SuperCat =
    | Dead
    | Alive of Cat
    | MonsterCat
    with 
    static member create lives =
        match lives with 
        | x when x < 1 -> Dead
        | x when x >= 1 && x <= 9 -> Alive { lives = lives }
        | _ -> MonsterCat


let cats = x |> List.map (fun e -> SuperCat.create e)
//printfn "cats = %A " cats


// Filter
let myCatFilter c =
    match c with
    | Dead -> false
    | Alive c ->
        match c.lives with
        | x when x < 3 -> false
        | _ -> true
    | MonsterCat -> true


let myCats =
    cats
    |> List.filter (fun e -> myCatFilter e)
printfn "myCats = %A " myCats


let notMyCats =
    cats
    |> List.filter (fun e -> myCatFilter e |> not)
printfn "notMyCats = %A " notMyCats


type Smell =
    | Flower
    | Garbage
    | Skunk


type CatSoup =
    | Foo
    | Tasty of Smell
    | Yammy


let myCatChooser c =
    match c with
    | Dead -> Some Foo
    | Alive a -> 
        match a.lives with
        | 1 -> Foo |> Some
        | x when x < 3 -> Some (Tasty Skunk)
        | x when x >=3 && x < 5 -> Tasty Garbage |> Some
        | x when x >= 5 && x < 8 -> Tasty Flower |> Some
        | _ -> Yammy |> Some
    | MonsterCat -> None


let mySoups =
    cats
    |> List.choose (fun e -> myCatChooser e)
printfn "mySoups = %A " mySoups


let myNewSoups =
    cats
    |> List.filter (fun e -> myCatFilter e)
    |> List.filter (fun e -> myCatFilter e |> not)
    |> List.choose (fun e -> myCatChooser e)
printfn "myNewSoups = %A " myNewSoups


let extraSoups =
    [
        Tasty Flower
        Yammy
    ]


// List.map
// List.sort
// List.rev
let xyz =
    cats
    |> List.map (fun e -> myCatChooser e)
    |> List.choose (fun e -> e)
    |> List.append extraSoups
    |> List.sort
    //|> List.rev
    //|> List.distinct
    |> List.groupBy (fun e -> e)
    |> List.map (fun (s, l) -> (s, l.Length))
    // fst, snd
    |> List.sortBy (fun e -> snd e)
    |> List.rev


printfn "xyz = %A " xyz

// Write a function to remove just one soup of particular type.
