open System

open FSharp.Lessons.Primitives
open FSharp.Lessons.BusinessEntities
open FSharp.Lessons.Proxies
open FSharp.Lessons.BusinessLogic
open FSharp.Lessons.DbData

let proxy = EmployeeProxy.create ConnectionString.defaultValue
let email = EmployeeEmail (Email "John.Smith@nowhere.gg")
let wrongEmail = EmployeeEmail (Email "Joohn.Smith@nowhere.gg")

let employee =
    {
        employeeId = EmployeeId 0L
        employeeName = EmployeeName "John Smith"
        employeeEmail = email
        managedBy = None
        dateHired = DateTime.Now
        salary = 100.0m
        description = None
        data = Map.empty
    }

printfn "Trying to create employee in the database."
let result1 = proxy.saveEmployee employee
printfn $"Result: %A{result1}\n\n"
Console.ReadLine() |> ignore

printfn $"Trying to get employee with email: {email}."
let result2 = proxy.loadEmployeeByEmail email
printfn $"Result: %A{result2}\n\n"
Console.ReadLine() |> ignore

printfn $"Trying to get employee with wrong email: {wrongEmail}."
let result3 = proxy.loadEmployeeByEmail wrongEmail
printfn $"Result: %A{result3}\n\n"
Console.ReadLine() |> ignore
