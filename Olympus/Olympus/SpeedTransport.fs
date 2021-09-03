namespace Olympus

open System

module SpeedTransport =

    let readInt() = Console.ReadLine() |> Int32.Parse


    type Track =
        {
            a : int
            b : int
            c : int
            d : int
        }


    let f t x y =
        if t.c - t.b - 1 + y >= x
        then (x + 1)
        else t.c - t.b + y


    let calculate t =
        let inner x =
            [ for y in 0..x -> f t x y ]
            |> List.sum

        [ for x in 0..(t.b - t.a) -> inner x ]
        |> List.sum


    let run() =
        let t =
            {
                a = readInt()
                b = readInt()
                c = readInt()
                d = readInt()
            }

        let r = calculate t
        printfn $"{r}"
