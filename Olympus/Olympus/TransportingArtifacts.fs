namespace Olympus

open System

module TransportingArtifacts =
    type Brick =
        {
            a : int
            b : int
        }

    let createBrick a b =
        {
            a = a
            b = b
        }

    let turn brick r =
        match r with
        | true ->
            {
                a = brick.b
                b = brick.a
            }
        | false -> brick


    type Barge =
        | OneRow of Brick * Brick * Brick
        | TwoRows of (Brick * Brick) * Brick


    let calculateArea barge =
        match barge with
        | OneRow (x, y, z) ->
            let a = [ x.a; y.a; z.a ] |> List.max
            let b = [ x.b; y.b; z.b ] |> List.sum
            a * b
        | TwoRows ((x, y), z) ->
            let a = ([ x.a; y.a ] |> List.max) + z.a
            let b = [ x.b + y.b; z.b ] |> List.max
            a * b


    let r = [ false; true ]

    let allRotations = List.allPairs r r |> List.allPairs r |> List.map (fun (a, (b, c)) -> a, b, c)

    let create (x, y, z) =
        [
            OneRow (x, y, z)
            TwoRows ((x, y), z)
            TwoRows ((y, z), x)
            TwoRows ((z, x), y)
        ]


    let findBest x y z =
        allRotations
        |> List.map (fun (a, b, c) -> turn x a, turn y b, turn z c)
        |> List.map create
        |> List.concat
        |> List.map calculateArea
        |> List.min

    let readInt() = Console.ReadLine() |> Int32.Parse

    let run() =
        let c() = (readInt(), readInt()) ||> createBrick
        let x = c()
        let y = c()
        let z = c()
        let r = findBest x y z
        printfn "%A" r
