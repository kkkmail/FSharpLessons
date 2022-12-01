open Primes.PrimeSolver
open System

printfn "Starting..."

let p =
    {
        numberOfPrimes = 100
        numberCount = 100
        gcd = 30
        takeNumber = 20
        numberOfSteps = 10
    }

let result = solve p
printfn $"Result: {result}"
Console.ReadLine() |> ignore
