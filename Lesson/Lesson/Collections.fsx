open System

// Pipe
let addOne x = x + 1
let multTwo x = x * 2
let add x y = x + y

let y = addOne 3
let y2 = 6

let z =
    3
    |> addOne
    |> multTwo
    |> addOne
    |> add

let adder a x = a (x + 1)

let adder2 = adder z

let z1 = addOne (multTwo (addOne 3))

/// Xml Comment
/// Part 2
let mult0 a b = a * b
let mult3 a b c = (a * b + c, a + b, c - a)

let v1 = (1, 2)
let z2 =
    v1
    ||> mult0


let v = (1, 2, 3)
let z3 =
    v
    |||> mult3
    |||> mult3

/// List
let xList : list<int> = [ 1; 2 ]
let xList22 = 5 :: xList

let xList1 =
    [
        1
        2
        3
        4
        5
    ]

let xList2 = 5 :: xList1

let tryGetElement g =
    match g with
    | [] -> None
    | h :: t -> Some h


let tryGetThirdElement g =
    match g with
    | [] -> None
    | h :: [] -> None
    | h :: h1 :: [] -> None
    | h :: h1 :: h2 :: t -> Some h2


let tryGetLastElement g =
    let rec inner last rest =
        match rest with
        | [] -> last
        | h :: t -> inner (Some h) t

    inner None g


type MyOption<'A> =
    | MySome of 'A
    | MyNone

let a1 = MySome 3
let b1 = MySome 2.0

/// Array
let xArray : int[] = [| 1; 2; 3 |]

let xArray1 : array<int> =
    [|
        2
        5
        3
        1
        87
    |]

let element0 = xArray1.[4]


/// Set
let xSet1 =
    [
        5
        2
        3
        5
        1
    ]
    |> Set.ofList
    |> Set.toList


printfn "%A" xSet1


/// Seq
let seq1 =
    seq { for i in 1 .. 10000000 -> (int64 i) * (int64 i) }


//printfn "xList1 = %A" xList1
//printfn "seq1 = %A" seq1

//#time
//let lst1 = seq1 |> Seq.toList
//#time

//printfn "lst1 = %A" lst1

//let filter e = e <= 100

//#time
//let seq2 =
//    seq1
//    |> Seq.filter (fun e -> e < 0L)
//#time

//printfn "seq2 = %A" seq2

//let maxInt = Int64.MaxValue
//let minInt = Int64.MinValue
//printfn "maxInt = %A, minInt = %A, (maxInt + 1) = %A" maxInt minInt (maxInt + 1L)


/// List operations
let tryExactlyOne x =
    match x with
    | [] -> None
    | h :: [] -> Some h
    | _ -> None


let chooser e =
    if e <=3
    then (e * e).ToString() |> Some
    else None

let xList3 =
    [ 1; 2; 3; 4; 5 ]
    |> List.choose chooser

printfn "xList3 = %A" xList3

let xList4 =
    [ 1; 2; 3; 4; 5 ]
    |> List.filter (fun e -> e <= 3)

printfn "xList4 = %A" xList4

let x5 =
    xList4
    |> List.map (fun e -> e - 3)

printfn "x5 = %A" x5

let w =
    xList4
    |> List.allPairs x5

printfn "w = %A" w

let tryDiv x y =
    try
        x/y
    with
    | e ->
        printfn "Exception: %A" e
        -1

let a = tryDiv 1 0

printfn "a = %A" a
