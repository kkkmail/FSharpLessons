open Decoder.Core


// For more information see https://aka.ms/fsharp-console-apps
printfn "Starting..."

//runAll 1 2
let minSeed = 1
let maxSeed = 1024

//generateBatchFileAndInputs minSeed maxSeed

let results = readResults minSeed maxSeed
// outputResultsWithHeader results
outputResultsToCSV results
