namespace FSharp.Lessons

module DataTransformations =

    type ErrorData =
        | StringError of string
        | SomeOtherError of string


    let toStringError s = s |> StringError |> Error


    type Employee =
        {
            name : string
            salary : double
        }


    type IncomeRaise =
        | RaiseByPct of double
        | RaiseByAmount of double

        static member tryCreateByPctRaise p =
            if p < 0.0 || p > 1.0 then toStringError $"The percentage parameter: '{p}' is invalid."
            else Ok (RaiseByPct p)

        static member tryCreateByAmountRaise p =
            if p < 0.0 || p > 10_000.0 then toStringError $"The amount parameter: '{p}' is invalid."
            else Ok (RaiseByAmount p)


    let raiseIncome r e =
        match r with
        | RaiseByPct p -> { e with salary = e.salary * (1.0 + p) }
        | RaiseByAmount a -> { e with salary = e.salary + a }


    let raiseAll r e = e |> List.map (raiseIncome r)


    let tryRaiseAllByPct p e =
        IncomeRaise.tryCreateByPctRaise p
        |> Result.map raiseAll
        |> Result.bind (fun f -> f e |> Ok)
