open Primes.PrimeSolver
open System

printfn "Starting..."

let p =
    {
        numberOfPrimes = 200_000
        numberCount = 20_000
        gcd = 30
        takeNumber = 2_000
        numberOfSteps = 25
    }

let result = solve p
//printfn $"Result: {result}"
Console.ReadLine() |> ignore
