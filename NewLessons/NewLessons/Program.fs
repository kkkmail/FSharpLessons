open NewLessons.Lessons

[<EntryPoint>]
let main argv =
    printfn "%A" argv

    lessons
    |> List.map (fun (i, s, f) -> printfn "Lesson number: %A - %A" i s)
    |> ignore

    let (a, b, c) = lessons.[0]
    c() |> ignore

    0 // return an integer exit code
