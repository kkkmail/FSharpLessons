open System
open MathProblems.EnchantedForest

[<EntryPoint>]
let main argv =
    printfn "Starting..."

    let f =
        {
            vampires = 6
            spiders = 55
            unicorns = 17
        }

//    let f =
//        {
//            vampires = 4
//            spiders = 4
//            unicorns = 4
//        }

    let r = Random()
    let g() = r.Next(1_000_000)
    let n = 1_000_000

    let f1 =
        [ for i in 1..n -> i ]
        |> List.map (fun _ -> evolve g f)
        |> List.groupBy id
        |> List.map (fun (a, b) -> (a, b.Length))

    printfn "f = %A\n============================\n" f
    f1 |> List.map (printfn "%A\n") |> ignore
    Console.ReadLine() |> ignore
    0
