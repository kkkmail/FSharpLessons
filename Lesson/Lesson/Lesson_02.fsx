// Arrays and Lists

let a = [| "went"; "I"; "home" |]
let b = [ "went"; "I"; "home" ]


// Adding element to list
let c = "today" :: b
let d = "kjhsd" :: (a |> Array.toList)


// Full array / list
printfn "a = %A" a
printfn "b = %A" b
printfn "c = %A" c
printfn "d = %A" d


// Elements of array / list
printfn "First element of a = %A" a.[0]
printfn "Third element of b = %A" b.[2]

let makeSense a =
    let rec inner a s = 
        match a with
        | [] -> s
        | h :: t -> inner t (s + " " + h)

    inner a ""


let x = makeSense b
printfn "x = %A" x


let y =
    b
    |> List.sort
    |> List.fold (fun acc e -> acc + " " + e) ""

printfn "y = %A" y


let z = 
    a
    |> Array.sort
    |> Array.fold (fun acc e -> acc + " " + e) ""

printfn "z = %A" z
