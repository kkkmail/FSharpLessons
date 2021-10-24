namespace FSharp.Lessons
open System
open FSharp.Data.Sql

module DataTransformations =

    type ErrorData =
        | StringError of string
        | SomeOtherError of string


    let toStringError s = s |> StringError |> Error


    type ResultData<'T> = Result<'T, ErrorData>
    type UnitResult = ResultData<unit>
    type ListResult<'T> = ResultData<List<'T>>


    type LoggerProxy =
        {
            logError : ErrorData -> unit
        }


    type EmployeeId =
        | EmployeeId of bigint

        member e.value = let (EmployeeId v) = e in v


    type EmployeeName =
        | EmployeeName of string

        member e.value = let (EmployeeName v) = e in v


    type Employee =
        {
            employeeId : EmployeeId
            employeeName : EmployeeName
            dateHired : DateTime
            salary : decimal
            description : string option
        }


    type EmployeeProxy =
        {
            loadEmployee : EmployeeId -> ResultData<Employee>
            saveEmployee : Employee -> UnitResult
            loadEmployees : (Employee -> bool) -> ListResult<Employee>
        }


    type IncomeRaise =
        | RaiseByPct of decimal
        | RaiseByAmount of decimal

        static member tryCreateByPctRaise p =
            if p < 0.0m || p > 1.0m then toStringError $"The percentage parameter: '{p}' is invalid."
            else Ok (RaiseByPct p)

        static member tryCreateByAmountRaise p =
            if p < 0.0m || p > 10_000.0m then toStringError $"The amount parameter: '{p}' is invalid."
            else Ok (RaiseByAmount p)


    let raiseIncome r e =
        match r with
        | RaiseByPct p -> { e with salary = e.salary * (1.0m + p) }
        | RaiseByAmount a -> { e with salary = e.salary + a }


    let raiseAll r e = e |> List.map (raiseIncome r)


    let tryRaiseAllByPct p e =
        IncomeRaise.tryCreateByPctRaise p
        |> Result.map raiseAll
        |> Result.bind (fun f -> f e |> Ok)
