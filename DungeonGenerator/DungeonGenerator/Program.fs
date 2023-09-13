open System
open DungeonGenerator.Primitives

// http://www.visualfsharp.com/graphics/drawing.htm
open System.Drawing
open System.Windows.Forms

let rnd1 = Random(2)
let rnd2 = Random(2)

let mutable doubleCnt = 0
let mutable intCnt = 0

let nextDouble() =
    doubleCnt <- doubleCnt + 1
    rnd2.NextDouble()


let nextInt maxValue =
    intCnt <- intCnt + 1
    rnd1.Next maxValue


let p =
    {
        minOpenedDoors = 2
        maxOpenedDoors = 3
        minDoorThreshold = 0.93
        openInvalid = true
        nextInt = nextInt
        nextDouble = nextDouble
    }

let noOfRooms = 600

let xZero = 1500
let yZero = 1300
let sizeX = 40
let sizeY = 40
let delta = 5


let dungeon = Dungeon.create 2 DoorType.Left p

let steps = [ for i in 1..noOfRooms -> i ]

let makeStep i (d : Dungeon) =
    printfn "\n\n========================================================"
    printfn $"Making step # {i}."

    if i >= (noOfRooms - 5) && i <= noOfRooms
    then
        printfn ""

    let d1 = d.tryAddRoom()

    d1.toList()
    |> List.map (fun e -> printf $"{e}\n\n")
    |> ignore

    d1


printfn "Starting..."
let rnd = new Random 2

let numbers = [ for i in 0..79 -> (i, rnd.NextDouble()) ]

let fullDungeon =
    steps
    |> List.fold (fun acc r -> makeStep r acc) dungeon

printfn "End."

let drawWall (e : PaintEventArgs) (c : Coordinates) (dt : DoorType) (i : bool) =
    let x, y = xZero + c.x * sizeX, yZero + c.y * sizeY

    let x1, y1, x2, y2 =
        match dt with
        | Upper -> x - sizeX / 2, y - sizeY / 2, x + sizeX / 2, y - sizeY / 2
        | Left -> x - sizeX / 2, y - sizeY / 2, x - sizeX / 2, y + sizeY / 2
        | Bottom -> x - sizeX / 2, y + sizeY / 2, x + sizeX / 2, y + sizeY / 2
        | Right -> x + sizeX / 2, y - sizeY / 2, x + sizeX / 2, y + sizeY / 2

    let penCurrent : Pen =
        match i with
        | false -> new Pen(Color.Blue, 2F)
        | true -> new Pen(Color.Red, 4F)

    e.Graphics.DrawLine(penCurrent, x1, y1, x2, y2)

let exercise = new Form()
exercise.WindowState <- FormWindowState.Maximized

let drawRoom (e : PaintEventArgs) m (r : RoomOnMap) =
    r.room.closedDoors
    |> Set.toList
    |> List.map (fun v -> drawWall e r.coordinates v false)
    |> ignore

    r.room.invalidDoors
    |> Set.toList
    |> List.map (fun v -> drawWall e r.coordinates v true)
    |> ignore

    let x, y = xZero + r.coordinates.x * sizeX, yZero + r.coordinates.y * sizeY

    if r.room.roomNumber = 0
    then
        let penCurrent = new Pen(Color.Red, 2F)
        e.Graphics.DrawEllipse (penCurrent, Rectangle(x - delta, y - delta, 2 * delta, 2 * delta))

    if r.room.roomNumber = (m - 1)
    then
        let penCurrent = new Pen(Color.Gold, 2F)
        e.Graphics.DrawEllipse (penCurrent, Rectangle(x - delta, y - delta, 2 * delta, 2 * delta))

let exercisePaint(e : PaintEventArgs) =
//    let mutable penCurrent : Pen = new Pen(Color.Red)
//    e.Graphics.DrawLine(penCurrent, 20, 20, 205, 20)
//
//    penCurrent <- new Pen(Color.Green, 3.50F)
//    e.Graphics.DrawLine(penCurrent, 40, 40, 225, 40)
//
//    penCurrent <- new Pen(Color.Blue, 7.25F)
//    e.Graphics.DrawLine(penCurrent, 30, 60, 215, 60)
    let rooms = fullDungeon.toList()
//    let rooms = dungeon.toList()
    rooms |> List.map (drawRoom e rooms.Length) |> ignore
    ()

exercise.Paint.Add exercisePaint
do Application.Run exercise
