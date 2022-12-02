namespace Primes

open Open.Numeric.Primes
open System.Linq
open System.Diagnostics
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
            |> PSeq.map (fun ae -> (ae, primes |> List.map (fun pe -> pe + ae) |> List.filter (fun (e : uint64) -> Number.IsPrime(e)) |> Set.ofList))
            |> Seq.sortBy (fun (_, b) -> - b.Count)
            |> Seq.take p.takeNumber
            |> List.ofSeq

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
                        |> PSeq.map (fun (be, qe) -> a |> List.map (fun (ae, pe) -> (if be |> List.contains ae then [] else ae :: be |> List.sort), (pe, qe) ||> Set.intersect))
                        |> Seq.concat
                        |> Seq.filter (fun (a, _) -> a.Length > 0)
                        |> Seq.sortBy (fun (_, b) -> - b.Count)
                        |> Seq.take p.takeNumber
                        |> List.ofSeq

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
        let sw = Stopwatch.StartNew()
        let primes = Prime.Numbers.Take(p.numberOfPrimes) |> Seq.toList
        let numbers = [ for i in 1..p.numberCount -> uint64 (i * p.gcd)]
        let best = getBest p primes numbers
        let startBest = best |> List.map (fun (a, b) -> [ a ], b)

        let steps = [for i in 1 .. p.numberOfSteps -> i]

        let result =
            steps
            |> List.fold (fun acc e -> makeStep p best acc e) startBest
            |> List.tryHead

        printfn $"Took: {sw.Elapsed.Seconds} seconds."

        match result with
        | Some r -> printResult r
        | None -> printfn "Failed!"

        result
