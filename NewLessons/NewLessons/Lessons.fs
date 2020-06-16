namespace NewLessons
open System

module Lessons =

    /// Tournament
    let lesson1 () =
        let n = Console.ReadLine() |> Int32.Parse
        let results = [ for _ in 1..(n - 1) -> Console.ReadLine() |> Int32.Parse ]
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

    let lessons =
        [
            "Tournament", lesson1
        ]
        |> List.mapi (fun i (s, f) -> i + 1, s, f)

