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
            numberToPrint : int
        }


    let take n l =
        if l |> List.length > n then l |> List.take n
        else l


    let printBest p b =
        printfn "\n\nPrinting best combos ..."
        b
        |> take p.numberToPrint
        |> List.map (fun (a, p) -> a, p |> Seq.toList)
        |> List.mapi(fun i (a, p) -> printfn $"""i = {i}, nA = {a |> List.length}, nP = {p.Length}, a = {a |> List.map (fun e -> ($"{e}")) |> String.concat ", "}, P = {p |> List.sort |> List.map (fun e -> ($"{e}")) |> String.concat ", "}""" )
        |> ignore

        printfn "Completed."


    let private sw = Stopwatch.StartNew()

    let printRuntime restart printElapsed =
        if printElapsed then printfn $"    ... took: {sw.Elapsed.TotalSeconds} seconds."
        if restart then sw.Restart()

    let restartStopWatch () = printRuntime true false
    let printElapsed () = printRuntime true true


    let getBest p primes numbers =
        restartStopWatch()

        printfn "Calculating best numbers with primes..."

        let r =
            numbers
            |> PSeq.map (fun ae -> (ae, primes |> List.map (fun pe -> pe + ae) |> List.filter (fun (e : uint64) -> Number.IsPrime(e)) |> Set.ofList))
            |> PSeq.sortBy (fun (_, b) -> - b.Count)
            |> Seq.take p.takeNumber

        printElapsed()

        printfn "Calculating best primes..."

        let q =
            primes
            |> PSeq.map (fun pe -> (pe, numbers |> List.map (fun ae -> pe + ae) |> List.filter (fun (e : uint64) -> Number.IsPrime(e)) |> Set.ofList))
            |> PSeq.sortBy (fun (_, b) -> - b.Count)
            |> Seq.take p.takeNumber
            |> Seq.map fst
            |> Set.ofSeq

        printElapsed()

        printfn "Constraining primes to the best primes..."

        let w =
            r
            |> PSeq.map (fun (ae, pe) -> ae, (pe, q) ||> Set.intersect)
            |> PSeq.filter (fun (ae, pe) -> pe.Count >= p.numberOfFaces)
            |> PSeq.sortBy (fun (_, b) -> - b.Count)
            |> List.ofSeq

        printElapsed()

        w


    let getBest2 p primes numbers =
        restartStopWatch()
        printfn "Calculating best numbers with primes..."

        let n =
            numbers
            |> PSeq.map (fun ae -> ae, primes |> List.map (fun pe -> pe + ae) |> List.filter (fun (e : uint64) -> Number.IsPrime(e)))
            |> PSeq.sortBy (fun (_, b) -> - b.Length)
            |> Seq.take p.takeNumber
            |> Seq.map fst
            |> Seq.sort
            |> List.ofSeq

        printElapsed()

        printfn "Calculating best primes..."

        let q =
            primes
            |> PSeq.map (fun pe -> pe, n |> List.map (fun ae -> pe + ae) |> List.filter (fun (e : uint64) -> Number.IsPrime(e)))
            |> PSeq.sortBy (fun (_, b) -> - b.Length)
            |> Seq.take p.takeNumber
            |> Seq.map fst
            |> Seq.sort
            |> List.ofSeq

        printElapsed()

        printfn "Choosing best primes and numbers..."

        let w =
            n
            |> PSeq.map (fun ae -> ae, q |> List.map (fun pe -> pe + ae) |> List.filter (fun (e : uint64) -> Number.IsPrime(e)) |> Set.ofList)
            |> PSeq.sortBy (fun (_, b) -> - b.Count)
            |> Seq.take p.takeNumber
            |> List.ofSeq

        printElapsed()

        printfn "Choosing best x 2..."

        let w1 =
            w
            |> List.map (fun (a, pa) -> w |> List.filter (fun (e, _) -> e <> a) |> List.map (fun (b, pb) -> [a; b] |> List.sort, (pa, pb) ||> Set.intersect))
            |> List.concat
            |> List.sortBy (fun (_, b) -> - b.Count)
            |> List.distinct
            |> take p.takeNumber

        printElapsed()

        printfn "Choosing best x 4..."

        let w2 =
            w1
            |> List.map (fun (a, pa) -> w1 |> List.filter (fun (e, _) -> e <> a) |> List.map (fun (b, pb) -> List.concat [a; b] |> List.distinct |> List.sort, (pa, pb) ||> Set.intersect))
            |> List.concat
            |> List.filter (fun (a, _) -> a.Length = 4)
            |> List.sortBy (fun (_, b) -> - b.Count)
            |> List.distinct
            |> take p.takeNumber

        printElapsed()

        printfn "Choosing best x 8..."

        let w3 =
            w2
            |> List.map (fun (a, pa) -> w2 |> List.filter (fun (e, _) -> e <> a) |> List.map (fun (b, pb) -> List.concat [a; b] |> List.distinct |> List.sort, (pa, pb) ||> Set.intersect))
            |> List.concat
            |> List.filter (fun (a, _) -> a.Length = 8)
            |> List.sortBy (fun (_, b) -> - b.Count)
            |> List.distinct
            |> take p.takeNumber

        printBest p w3
        printElapsed()

        printfn "Choosing best x 12..."

        let w4 =
            w3
            |> List.map (fun (a, pa) -> w2 |> List.filter (fun (e, _) -> e <> a) |> List.map (fun (b, pb) -> List.concat [a; b] |> List.distinct |> List.sort, (pa, pb) ||> Set.intersect))
            |> List.concat
            |> List.filter (fun (a, _) -> a.Length = 12)
            |> List.sortBy (fun (_, b) -> - b.Count)
            |> List.distinct
            |> take p.takeNumber

        printBest p w4
        printElapsed()

        printfn "Choosing best x 16..."

        let w5 =
            w4
            |> List.map (fun (a, pa) -> w2 |> List.filter (fun (e, _) -> e <> a) |> List.map (fun (b, pb) -> List.concat [a; b] |> List.distinct |> List.sort, (pa, pb) ||> Set.intersect))
            |> List.concat
            |> List.filter (fun (a, _) -> a.Length = 16)
            |> List.sortBy (fun (_, b) -> - b.Count)
            |> List.distinct
            |> take p.takeNumber

        printElapsed()

        w5


    let solve p =
        let primes = Prime.Numbers.Take(p.numberOfPrimes) |> Seq.toList
        let numbers = [ for i in 1..p.numberCount -> uint64 (i * p.gcd)]
        let best = getBest2 p primes numbers

        printBest p best
        0
