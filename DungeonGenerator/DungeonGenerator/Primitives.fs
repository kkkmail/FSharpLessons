namespace DungeonGenerator

module Primitives =
    
//    type ArrayMap<'K, 'V when 'K : comparison and 'V : comparison > =
//        {
//            array : array<'K * 'V>
//            map : Map<'K, 'V>
//        }
//        
//        member am.add k v =
//            if am.array.Length > am.map.Count
//            then { am with array = am.} 
        

    type Coordinates =
        {
            x : int
            y : int
        }

        static member defaultValue =
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
            maxOpenedDoors : int
            closedDoors : Set<DoorType>
            openedDoors : Set<DoorType>
            invalidDoors : Set<DoorType> // Doors that cannot be used - they are closed and cannot be opened.
        }

        static member defaultValue n m =
            {
                roomNumber = n
                maxOpenedDoors = m
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
        member r.invalidCount = r.invalidDoors.Count

        member r.limitOpened maxOpenedDoors =
            if r.openedCount < maxOpenedDoors
            then r
            else { r with invalidDoors = r.invalidDoors |> Set.union r.closedDoors; closedDoors = Set.empty }


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

                    {
                        coordinates = h.coordinates
                        doorType = (h.room.closedDoors |> Set.toList).[d]
                    }
                    |> Some

        let a = rooms |> Seq.toList |> List.map (fun e -> { coordinates = e.Key; room = e.Value })
        inner a doorNumber


    let tryFindInvalidDoorToOpen (rooms : Map<Coordinates, Room>) (doorNumber : int) : DoorToOpen option =
        let rec inner (remaining : List<RoomOnMap>) d =
            match remaining with
            | [] -> None
            | h :: t ->
                if d >= h.room.invalidCount
                then inner t (d - h.room.invalidCount)
                else

                    {
                        coordinates = h.coordinates
                        doorType = (h.room.invalidDoors |> Set.toList).[d]
                    }
                    |> Some

        let a = rooms |> Seq.toList |> List.map (fun e -> { coordinates = e.Key; room = e.Value })
        inner a doorNumber

    type DungeonGenerationParam =
        {
            minOpenedDoors : int
            maxOpenedDoors : int
            minDoorThreshold : double
            openInvalid : bool
            nextInt : int -> int
            nextDouble : unit -> double
        }

    let getMaxOpenedDoors (p : DungeonGenerationParam) =
        if p.nextDouble() <= p.minDoorThreshold
        then p.minOpenedDoors
        else p.maxOpenedDoors

    type Dungeon =
        {
            rooms : Map<Coordinates, Room>
            generationParams : DungeonGenerationParam
        }

        member d.tryAddRoom () =
            let p = d.generationParams

            let allClosed =
                d.rooms
                |> Seq.map (fun e -> e.Value.closedCount)
                |> Seq.sum

            let doorNumberToOpen = p.nextInt (allClosed + 1)

            let g v =
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
                        let m = getMaxOpenedDoors p
                        let r = Room.defaultValue d.rooms.Count m

                        match r0.tryOpen v.doorType, r.tryOpen v.doorType.reverse with
                        | Some x0, Some x1 ->
                            {
                                d with
                                    rooms =
                                        d.rooms
                                        |> Map.add v.coordinates (x0.limitOpened x0.maxOpenedDoors)
                                        |> Map.add nextRoomCoordinates (x1.limitOpened x1.maxOpenedDoors)
                            }
                        | _ ->
                            printfn $"Something is wrong. Coordinates: {v.coordinates}, room: {r0}, door type: {v.doorType}."
                            d
                | None ->
                    printfn $"Unable to find room with coordinates {v.coordinates} in the dungeon."
                    d

            match tryFindDoorToOpen d.rooms doorNumberToOpen with
            | Some v -> g v
            | None ->
                printfn "Unable to find closed door to open."
                if d.generationParams.openInvalid
                then
                    let allInvalid =
                        d.rooms
                        |> Seq.map (fun e -> e.Value.invalidCount)
                        |> Seq.sum

                    let invalidDoorNumberToOpen = p.nextInt (allInvalid + 1)

                    match tryFindInvalidDoorToOpen d.rooms invalidDoorNumberToOpen with
                    | Some v -> g v
                    | None ->
                        printfn $"Unable to find invalid room to open."
                        d
                else d


        member d.toList() =
            d.rooms
            |> Seq.toList
            |> List.map (fun e -> { coordinates = e.Key; room = e.Value })
            |> List.sortBy (fun e -> e.room.roomNumber)

        static member create m dt p =
            let r = Room.defaultValue 0 m

            {
                rooms = [ (Coordinates.defaultValue, { r with openedDoors = r.openedDoors |> Set.add dt; closedDoors = r.closedDoors |> Set.remove dt }) ] |> Map.ofList
                generationParams = p
            }
