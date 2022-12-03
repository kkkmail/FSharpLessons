namespace Primes

open Open.Numeric.Primes
open System.Linq
open System.Diagnostics
open FSharp.Collections.ParallelSeq

module PrimeSolver2 =

    type SolverParam =
        {
            numberOfPrimes : int
            numberCount : int
            gcd : int
            takeNumber: int
            numberOfSteps : int
            numberOfFaces : int
        }


    let getBest p primes numbers =
        printfn "Calculating best numbers with primes..."

        let r =
            numbers
            |> PSeq.map (fun ae -> (ae, primes |> List.map (fun pe -> pe + ae) |> List.filter (fun (e : uint64) -> Number.IsPrime(e)) |> Set.ofList))
            |> PSeq.sortBy (fun (_, b) -> - b.Count)
            |> Seq.take p.takeNumber

        printfn "Calculating best primes..."

        let q =
            primes
            |> PSeq.map (fun pe -> (pe, numbers |> List.map (fun ae -> pe + ae) |> List.filter (fun (e : uint64) -> Number.IsPrime(e)) |> Set.ofList))
            |> PSeq.sortBy (fun (_, b) -> - b.Count)
            |> Seq.take p.takeNumber
            |> Seq.map fst
            |> Set.ofSeq

        printfn "Constraining primes to the best primes..."

        let w =
            r
            |> PSeq.map (fun (ae, pe) -> ae, (pe, q) ||> Set.intersect)
            |> PSeq.filter (fun (ae, pe) -> pe.Count >= p.numberOfFaces)
            |> PSeq.sortBy (fun (_, b) -> - b.Count)
            |> List.ofSeq

        w

    let printBest b =
        printfn "Printing best combos ..."
        b
        |> List.mapi(fun i (a, p) -> printfn $"""i = {i}, a = {a}, P = {p |> Set.toList |> List.sort |> List.map (fun e -> ($"{e}")) |> String.concat ", "}""" )
        |> ignore

    let solve p =
        let sw = Stopwatch.StartNew()
        let primes = Prime.Numbers.Take(p.numberOfPrimes) |> Seq.toList
        let numbers = [ for i in 1..p.numberCount -> uint64 (i * p.gcd)]
        let best = getBest p primes numbers

        printBest best
        0
