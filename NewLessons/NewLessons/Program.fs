open System
open NewLessons.Lessons

[<EntryPoint>]
let main argv =
    printfn "%A" argv

    lessons
    |> List.map (fun (i, s, f) -> printfn "Lesson number: %A - %A" i s)
    |> ignore

    printfn "Input lesson number:"
    let n = readInt()

    let (a, b, c) = lessons.[n - 1]
    c() |> ignore
    Console.ReadLine() |> ignore

    0 // return an integer exit code
