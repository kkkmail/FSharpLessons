namespace NewLessons
open System

module Lessons =

    let readInt() = Console.ReadLine() |> Int32.Parse
    let readInt64() = Console.ReadLine() |> Int64.Parse

    /// Tournament
    let lesson1 () =
        let n = readInt()
        let results = [ for _ in 1..(n - 1) -> readInt() ]
        let teams = [ for i in 1..n -> i ]

        let getWinner (p, r) =
            match r with
            | 1 -> fst p
            | 2 -> snd p
            | _ -> failwith "GFUK #1"

        let getAllWinners r p = List.zip p r |> List.map getWinner

        let getPairs x =
            x
            |> List.mapi (fun i e -> i, e)
            |> List.partition (fun (i, _) -> i % 2 = 0)
            ||> List.zip
            |> List.map (fun (a, b) -> snd a, snd b)

        let getRoundResults r t = getPairs t |> getAllWinners r

        let getRounds r =
            let split x =
                let (a, b) =
                    x
                    |> List.mapi (fun i e -> i, e)
                    |> List.partition (fun (i, _) -> i < (x.Length + 1) / 2)

                a |> List.map snd, b |> List.map snd

            let rec splitAll acc x =
                match x with
                | [e] -> [e]::acc |> List.rev
                | _ ->
                    let a, b = split x
                    splitAll (a::acc) b

            splitAll [] r

        let rounds = getRounds results

        let winner =
            rounds
            |> List.fold (fun acc r -> getRoundResults r acc) teams
            |> List.head

        printfn "The winner is: %A" winner

        0


    /// Tiles
    let lesson2 () =
        let m = readInt()
        let w = readInt()
        let h = readInt()

        let countTiles x =
            match x % m with
            | 0 -> (x / m) + 1
            | _ -> (x / m) + 2

        let r = (countTiles h) * (countTiles w)
        printfn "Max number of tiles: %A" r

        0


    /// Elevator
    let lesson3() =
        let n = readInt64()
        let a = readInt64()
        let b = readInt64()
        let c = readInt64()

        let x = (a * n + b) / (a + b)

        let getMinTime f =
            let d = c * f + b * (f - 1L)
            let u = c * f + a * (n - f)
            max d u

        let r =
            [ x; x + 1L ]
            |> List.map(fun e -> e, getMinTime e)
            |> List.sortBy snd
            |> List.head
            |> fst

        printfn "Floor: %A" (int r)

        0


    let lessons =
        [
            "Tournament", lesson1
            "Tiles", lesson2
            "Elevator", lesson3
        ]
        |> List.mapi (fun i (s, f) -> i + 1, s, f)

