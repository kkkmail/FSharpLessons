namespace Olympus

open System

module Olymp_03 =

    let readInt32() = Console.ReadLine() |> Int32.Parse


    type Pole =
        {
            poleNumber : int
            height : int
        }


    type Hill =
        {
            startPole : Pole
            endPole : Pole
        }

        member h.length = h.endPole.poleNumber - h.startPole.poleNumber + 1


    type Progress =
        | NotStarted
        | Start of Pole
        | InProgress of Pole * Pole
        | Found of Hill

        member p.isFound =
            match p with
            | Found _ -> true
            | _ -> false

        member s.isBestCoaster =
            match s with
            | Found x when x.length = 3 -> true
            | _ -> false


    let check p x =
        match p with
        | NotStarted -> Start x
        | Start s ->
            match s.height < x.height with
            | true -> InProgress (s, x)
            | false -> Start x
        | InProgress (s, p) ->
            match p.height > x.height with
            | true -> Found { startPole = s; endPole = x }
            | false ->
                match p.height = x.height with
                | true -> InProgress (s, x)
                | false -> InProgress (p, x)
        | Found a -> Found a


    let shortestHill (h1 : Hill) (h2 : Hill) =
        if h1.length <= h2.length then h1 else h2

    let findRollerCoaster r =
        let rec inner m p x =
            match x with
            | [] -> m
            | h :: t ->
                let n = check p h

                match n, m with
                | Found n1, Some m1 -> inner (shortestHill n1 m1 |> Some) n t
                | Found n1, None -> inner (Some n1) n t
                | _ -> inner m n t

        inner None NotStarted r


    let runRollerCoaster() =
        let n = readInt32()
        let r = [ for i in 1..n -> { poleNumber = i; height = readInt32() } ]
        let result = findRollerCoaster r

        match result with
        | Some h -> printfn "%i %i" h.startPole.poleNumber h.endPole.poleNumber
        | None -> printfn "0"


