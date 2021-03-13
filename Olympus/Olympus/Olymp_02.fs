namespace Olympus

open System

module Olymp_02 =

    let toInt32() = Console.ReadLine() |> Int32.Parse


    let toBool() =
        match toInt32() with
        | 0 -> false
        | _ -> true


    let findFirstBroken f =
        let rec inner x =
            match x with
            | [] -> []
            | h :: t ->
                match h with
                | false -> inner t
                | true -> x
        inner f


    let repair f n =
        match f with
        | [] -> []
        | _ ->
            if n >= f.Length
            then []
            else f |> List.skip n

    let repairAll f n =
        let rec inner count x =
            match x with
            | [] -> count
            | _ ->
                let b = findFirstBroken x

                match b with
                | [] -> count
                | _ -> repair b n |> inner (count + 1)

        inner 0 f


    let createFence k = [ for _ in 1..k -> toBool() ]

    let runFence() =
        let n = toInt32()
        let k = toInt32()

        let f = createFence k
        let i = repairAll f n
        printfn "%i" i
