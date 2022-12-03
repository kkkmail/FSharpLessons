open Primes.PrimeSolver2
open System

printfn "Starting..."

// let p =
//     {
//         numberOfPrimes = 200_000
//         numberCount = 20_000
//         gcd = 30
//         takeNumber = 2_000
//         numberOfSteps = 25
//     }
//
// let result = solve p
// Console.ReadLine() |> ignore


let p =
    {
        numberOfPrimes = 20000
        numberCount = 20000
        gcd = 30
        takeNumber = 2000
        numberOfSteps = 25
        numberOfFaces = 10
    }


let result = solve p
Console.ReadLine() |> ignore
