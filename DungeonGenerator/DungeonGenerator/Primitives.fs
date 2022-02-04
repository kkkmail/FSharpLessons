namespace DungeonGenerator

module Primitives =

    /// Coordinates of a vertex (dot) on a grid.
    type Coordinates =
        {
            x : int
            y : int
        }


    // A square room has only two doors belonging to it: upper and left.
    /// Bottom and right doors belong to other rooms.
    type DoorType =
        | Upper
        | Left
        | Bottom
        | Right

        member dt.value =
            match dt with
            | Upper -> 0
            | Left -> 1
            | Bottom -> 2
            | Right -> 3

        static member all = [ Upper; Left ; Bottom ; Right ] |> Set.ofList


    /// Coordinates are defined by two doors (upper and left) meeting in the vertex.
    type Door =
        {
            coordinates : Coordinates
            doorType : DoorType
        }

        member d.traverse = 0


    type Room =
        {
            coordinates : Coordinates
            closedDoors : Set<DoorType>
            openedDoors : Set<DoorType>
            invalidDoors : Set<DoorType> // Doors that cannot be used - they are closed and cannot be opened.
        }

        static member create c =
            {
                coordinates = c
                closedDoors = DoorType.all
                openedDoors = Set.empty
                invalidDoors = Set.empty
            }

        member r.tryOpen dt =
            match r.closedDoors |> Set.contains dt with
            | true -> { r with openedDoors = r.openedDoors |> Set.add dt; closedDoors = r.closedDoors |> Set.remove dt } |> Some
            | false -> None

        member r.closedCount = r.closedDoors.Count
        member r.openedCount = r.openedDoors.Count


    type Dungeon =
        {
            rooms : List<Room>
        }

        member d.tryAddRoom nextInt =
            let allOpened =
                d.rooms
                |> List.map (fun e -> e.closedCount)
                |> List.sum

            let tryOpen () =
                let doorNumberToOpen = nextInt (allOpened + 1)

                0
            0
