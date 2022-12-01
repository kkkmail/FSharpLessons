namespace Primes

open Open.Numeric.Primes
open System.Linq
open FSharp.Collections.ParallelSeq

module PrimeSolver =

    type SolverParam =
        {
            numberOfPrimes : int
            numberCount : int
            gcd : int
            takeNumber: int
            numberOfSteps : int
        }


    let getBest p primes numbers =
        let r =
            numbers
            |> List.map (fun ae -> (ae, primes |> List.map (fun pe -> pe + ae) |> List.filter (fun (e : uint64) -> Number.IsPrime(e)) |> Set.ofList))
            |> List.sortBy (fun (_, b) -> - b.Count)
            |> List.take p.takeNumber

        r


    let makeStep p a b n =
        printfn $"Step: {n}."

        let w =
            match b with
            | [] -> b
            | (x, y) :: _ ->
                if (x |> List.length) + 1 >= (y |> Set.count)
                then
                    printfn "Solution cannot be improved."
                    b
                else
                    let r =
                        b
                        |> List.map (fun (be, qe) -> a |> List.map (fun (ae, pe) -> (if be |> List.contains ae then [] else ae :: be |> List.sort), (pe, qe) ||> Set.intersect))
                        |> List.concat
                        |> List.filter (fun (a, _) -> a.Length > 0)
                        |> List.sortBy (fun (_, b) -> - b.Count)
                        |> List.take p.takeNumber

                    match r with
                    | [] ->
                        failwith ""
                    | (x, y) :: _ ->
                        if (x |> List.length) + 1 >= (y |> Set.count)
                        then
                            printfn $"Solution will be worse for step {n}."
                            b
                        else r

        w


    let printResult (a, b) =
        let numbers = (uint64 0) :: a |> List.sort
        printfn $"N = {numbers.Length}, M = {(b |> Set.count)}"
        printfn $"""{numbers |> List.map (fun e -> ($"{e}")) |> String.concat ", "}"""
        printfn $"""{b |> Set.toList |> List.sort |> List.map (fun e -> ($"{e}")) |> String.concat ", "}"""
        ignore()


    let solve p =
        let primes = Prime.Numbers.Take(p.numberOfPrimes) |> Seq.toList
        let numbers = [ for i in 1..p.numberCount -> uint64 (i * p.gcd)]
        let best = getBest p primes numbers
        let startBest = best |> List.map (fun (a, b) -> [ a ], b)

        let steps = [for i in 1 .. p.numberOfSteps -> i]

        let result =
            steps
            |> List.fold (fun acc e -> makeStep p best acc e) startBest
            |> List.tryHead

        match result with
        | Some r -> printResult r
        | None -> printfn "Failed!"

        result
