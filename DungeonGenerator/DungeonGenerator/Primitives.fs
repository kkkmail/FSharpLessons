namespace DungeonGenerator

module Primitives =

    type Coordinates =
        {
            x : int
            y : int
        }

        static member defUltValue =
            {
                x = 0
                y = 0
            }


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

        member dt.nextRoom c =
            match dt with
            | Upper -> { c with y = c.y - 1 }
            | Left -> { c with x = c.x - 1 }
            | Bottom -> { c with y = c.y + 1 }
            | Right -> { c with x = c.x + 1 }

        member dt.reverse =
            match dt with
            | Upper -> Bottom
            | Left -> Right
            | Bottom -> Upper
            | Right -> Left


    type DoorToOpen =
        {
            coordinates : Coordinates
            doorType : DoorType
        }


    type Room =
        {
            roomNumber : int
            closedDoors : Set<DoorType>
            openedDoors : Set<DoorType>
            invalidDoors : Set<DoorType> // Doors that cannot be used - they are closed and cannot be opened.
        }

        static member defaultValue n =
            {
                roomNumber = n
                closedDoors = DoorType.all
                openedDoors = Set.empty
                invalidDoors = Set.empty
            }

        member r.tryOpen dt =
            match r.closedDoors |> Set.contains dt with
            | true -> { r with openedDoors = r.openedDoors |> Set.add dt; closedDoors = r.closedDoors |> Set.remove dt } |> Some
            | false -> None

        member r.tryInvalidate dt =
            match r.closedDoors |> Set.contains dt with
            | true -> { r with invalidDoors = r.invalidDoors |> Set.add dt; closedDoors = r.closedDoors |> Set.remove dt } |> Some
            | false -> None

        member r.closedCount = r.closedDoors.Count
        member r.openedCount = r.openedDoors.Count


    type RoomOnMap =
        {
            coordinates : Coordinates
            room : Room
        }


    let tryFindDoorToOpen (rooms : Map<Coordinates, Room>) (doorNumber : int) : DoorToOpen option =
        let rec inner (remaining : List<RoomOnMap>) d =
            match remaining with
            | [] -> None
            | h :: t ->
                if d >= h.room.closedCount
                then inner t (d - h.room.closedCount)
                else
//                    printfn $"d = {d}, closed doors = {h.room.closedDoors}."

                    {
                        coordinates = h.coordinates
                        doorType = (h.room.closedDoors |> Set.toList).[d]
                    }
                    |> Some

//        printfn $"doorNumber = {doorNumber}."
        let a = rooms |> Seq.toList |> List.map (fun e -> { coordinates = e.Key; room = e.Value })
        inner a doorNumber


    type Dungeon =
        {
            rooms : Map<Coordinates, Room>
            nextInt : int -> int
        }

        member d.tryAddRoom () =
            let allClosed =
                d.rooms
                |> Seq.map (fun e -> e.Value.closedCount)
                |> Seq.sum

            let doorNumberToOpen = d.nextInt (allClosed + 1)

            match tryFindDoorToOpen d.rooms doorNumberToOpen with
            | Some v ->
                let nextRoomCoordinates = v.doorType.nextRoom v.coordinates

                match d.rooms |> Map.tryFind v.coordinates with
                | Some r0 ->
                    match d.rooms |> Map.tryFind nextRoomCoordinates with
                    | Some r ->
                        match r0.tryInvalidate v.doorType, r.tryInvalidate v.doorType.reverse with
                        | Some x0, Some x1 -> { d with rooms = d.rooms |> Map.add v.coordinates x0 |> Map.add nextRoomCoordinates x1 }
                        | _ ->
                            printfn $"Unable to invalidate door type: {v.doorType} in the room with coordinates {nextRoomCoordinates}. Room: {r}."
                            d
                    | None ->
                        let r = Room.defaultValue d.rooms.Count

                        match r0.tryOpen v.doorType, r.tryOpen v.doorType.reverse with
                        | Some x0, Some x1 -> { d with rooms = d.rooms |> Map.add v.coordinates x0 |> Map.add nextRoomCoordinates x1 }
                        | _ ->
                            printfn $"Something is wrong. Coordinates: {v.coordinates}, room: {r0}, door type: {v.doorType}."
                            d
                | None ->
                    printfn $"Unable to find room with coordinates {v.coordinates} in the dungeon."
                    d
            | None ->
                printfn "Unable to find door to open."
                d

        static member create nextInt dt =
            let r = Room.defaultValue 0

            {
                rooms = [ (Coordinates.defUltValue, { r with openedDoors = r.openedDoors |> Set.add dt; closedDoors = r.closedDoors |> Set.remove dt }) ] |> Map.ofList
                nextInt = nextInt
            }
