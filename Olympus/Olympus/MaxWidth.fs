namespace Olympus

open System

module MaxWidth =

    type CharMap = Map<char, List<int>>


    let findFirst m c i =
        let x =
            m
            |> Map.find c
            |> List.find(fun e -> e > i)
        x

    let findLast m c i =
        let a =
            m
            |> Map.find c

        let x =
            a
            |> List.rev
            |> List.find(fun e -> e < i)
        x

    let shortestFromStart sm t =
        let g (a, i) c =
            let n = findFirst sm c i
            (n :: a), n

        let x =
            t
            |> List.fold g ([], -1)
            |> snd

        x

    let shortestFromEnd sm t =
        let g (a, i) c =
            let n = findLast sm c i
            (n :: a), n

        let x =
            t
            |> List.rev
            |> List.fold g ([], Int32.MaxValue)
            |> snd

        x

    let doSomethingUseless sm t =
        let tr = t |> List.rev

        let rec inner x y maxVal =
            match x with
            | [] -> maxVal
            | h :: x1 ->
                let a = shortestFromStart sm (x1 |> List.rev)
                let y1 = h :: y
                let b = shortestFromEnd sm y1
                let maxVal1 = max (b - a) maxVal
                inner x1 y1 maxVal1

        inner tr [] 0


    let run() =
        let s = "abbbc".ToCharArray() |> List.ofArray
        let t = "abc".ToCharArray() |> List.ofArray

        let sm =
            s
            |> List.mapi (fun i c -> (c, i))
            |> List.groupBy fst
            |> List.map (fun (c, x) -> c, x |> List.map snd)
            |> Map.ofList

        let r = doSomethingUseless sm t

        printfn $"{r}"
        Console.ReadLine() |> ignore
