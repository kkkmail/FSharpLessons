// Functions
// C# 
// int Add(x, y)
//     {
//         return x + y;
//     }


let add x y =
    x + y

let z = add 1 2
printfn "z = %A" z


let addTen = add 10
let z1 = addTen 5
printfn "z1 = %A" z1


let z2 =
    6
    |> addTen
printfn "z2 = %A" z2


let x = [ for i in 1..10 -> i ]
printfn "x = %A" x


let xNew =
    x
    |> List.map (fun e -> addTen e)

printfn "xNew = %A" xNew

let rec factorial n =
    match n with 
    | 1 -> 1
    | _ -> n * (factorial (n - 1))

let f5 = factorial 5
printfn "f5 = %A" f5


x
|> List.map (fun e -> factorial e)
|> printfn "! = %A"


let rec fibonacci n =
    match n with 
    | 0 -> 0
    | 1 -> 1
    | _ -> (fibonacci (n - 1)) + (fibonacci (n - 2))


x
|> List.map (fun e -> fibonacci e)
|> printfn "Fibonacci = %A"


type Number =
    | Infinity
    | Number of int
    with
    static member create n =
        match n with
        | x when x >=0 && x <= 10 -> Number n
        | _ -> Infinity

    static member (+) (x, y) =
        match (x, y) with 
        | Infinity, Infinity -> Infinity
        | Infinity, Number _ -> Infinity
        | Number _, Infinity -> Infinity
        | Number a, Number b -> a + b |> Number.create


let a = Number 5
let b = Number 8
let c = Number 2

printfn "a = %A" a
printfn "a + b = %A" (a + b)
printfn "a + c = %A" (a + c)
