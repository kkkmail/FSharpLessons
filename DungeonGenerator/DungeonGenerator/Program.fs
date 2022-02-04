open System
open DungeonGenerator
open DungeonGenerator.Primitives

printfn "Hello from F#"

let rnd = Random(1)

let dungeon = Dungeon.create rnd.Next DoorType.Upper

let steps = [ for i in 1..10 -> i ]

let makeStep i (d : Dungeon) =
    printfn "\n\n========================================================"
    printfn $"Making step # {i}."
    let d1 = d.tryAddRoom()

    d1.rooms
    |> Seq.toList
    |> List.map (fun e -> { coordinates = e.Key; room = e.Value })
    |> List.sortBy (fun e -> e.room.roomNumber)
    |> List.map (fun e -> printf $"{e}\n\n")
    |> ignore

    d1


printfn "Starting..."
steps
|> List.fold (fun acc r -> makeStep r acc) dungeon
|> ignore

printfn "End."
Console.ReadLine() |> ignore

