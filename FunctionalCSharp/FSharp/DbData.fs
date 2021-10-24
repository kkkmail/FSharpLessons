﻿namespace FSharp.Lessons
open System
open FSharp.Data.Sql

open Primitives
open BusinessEntities
open Proxies

module DbData =

    [<Literal>]
    let DbConnectionString = "Data Source=localhost;Initial Catalog=Example;Integrated Security=SSPI"


    type ConnectionString =
        | ConnectionString of string

        member this.value = let (ConnectionString v) = this in v
        static member defaultValue() = ConnectionString DbConnectionString


    type private Database = SqlDataProvider<Common.DatabaseProviderTypes.MSSQLSERVER, DbConnectionString, UseOptionTypes = true>
    let private getContext (c : unit -> ConnectionString) = c().value |> Database.GetDataContext
    let private mapExceptionToError e = $"Exception: '%A{e}'." |> DatabaseError |> Error


    type private EmployeeTable = Database.dataContext.``dbo.EmployeeEntity``
    type private EmployeeDataTable = Database.dataContext.``dbo.EmployeeDataEntity``
    type private EmployeeDataTypeTable = Database.dataContext.``dbo.EmployeeDataTypeEntity``


    let private tryDbFun f =
        try
            f()
        with
        | e -> mapExceptionToError e


    let private mapEmployee (e : EmployeeTable) =
        match EmployeeName.tryCreate e.EmployeeName, EmployeeEmail.tryCreate e.EmployeeEmail with
        | Ok name, Ok email ->
            {
                employeeId = EmployeeId e.EmployeeId
                employeeName = name
                employeeEmail = email
                managedBy = e.ManagedByEmployeeId |> Option.map EmployeeId
                dateHired = e.DateHired
                salary = e.Salary
                description = e.Description
                data = Map.empty
            }
            |> Ok
        | _ -> toInvalidDataError $"Name: '%A{e.EmployeeName}' and/or email: '%A{e.EmployeeEmail}' are invalid."


    let private mapEmployeeData (d : EmployeeDataTable) =
        EmployeeDataType.tryCreate d.EmployeeDataTypeId
        |> Result.map (fun t ->
                            {
                                employeeDataType = t
                                emploeeDataValue = d.EmployeeDataValue
                            })


    let private mapEmployeeResult i (e, d) =
        match e |> Seq.map mapEmployee |> Seq.tryHead with
        | Some (Ok employee) ->
            let (data, errors) = d |> Seq.map mapEmployeeData |> List.ofSeq |> unzip
            match errors with
            | [] -> Ok { employee with data = data |> List.map (fun e -> (e.employeeDataType, e)) |> Map.ofList }
            | _ ->
                let m = errors |> List.map (fun e -> $"'%A{e}'") |> String.concat ", "
                toInvalidDataError $"Some data is invalid: {m}."
        | Some (Error e) -> Error e
        | None -> toInvalidDataError $"Cannot find employee with ID: '%A{i}'."


    let private loadEmployee c (EmployeeId i) =
        let g () =
            let ctx = getContext c

            let e =
                query {
                    for e in ctx.Dbo.Employee do
                    where (e.EmployeeId = i)
                    select e
                }

            let d =
                query {
                    for d in ctx.Dbo.EmployeeData do
                    where (d.EmployeeId = i)
                    select d
                }

            mapEmployeeResult i (e, d)

        tryDbFun g


    let private loadEmployeeByEmail c (EmployeeEmail (Email email)) =
        let g() =
            let ctx = getContext c

            let x =
                query {
                    for e in ctx.Dbo.Employee do
                    where (e.EmployeeEmail = email)
                    select (Some e.EmployeeId)
                    exactlyOneOrDefault
                }

            match x with
            | Some v -> loadEmployee c (EmployeeId v)
            | None -> toInvalidDataError $"Cannot find employee with email: '{email}'."

        tryDbFun g


    let private saveEmployee c e =
        let g() =
            let ctx = getContext c

            let x =
                query {
                    for d in ctx.Dbo.Employee do
                    where (d.EmployeeId = e.employeeId.value)
                    select (Some d)
                    exactlyOneOrDefault
                }

            match x with
            | Some v ->
                v.EmployeeName <- e.employeeName.value
                v.EmployeeEmail <- e.employeeEmail.value
                v.ManagedByEmployeeId <- e.managedBy |> Option.map (fun a -> a.value)
                v.DateHired <- e.dateHired
                v.Salary <- e.salary
                v.Description <- e.description
                ctx.SubmitUpdates()

                loadEmployee c e.employeeId
            | None ->
                let employee = ctx.Dbo.Employee.Create(
                    DateHired = e.dateHired,
                    EmployeeEmail = e.employeeEmail.value,
                    //EmployeeName = e.employeeName.value,
                    ManagedByEmployeeId = (e.managedBy |> Option.map (fun a -> a.value)),
                    Salary = e.salary,
                    Description = e.description)

                // Glitch in the library.
                employee.EmployeeName <- e.employeeName.value

                ctx.SubmitUpdates()
                loadEmployee c (EmployeeId employee.EmployeeId)

        tryDbFun g


    let private loadSubordinates c (EmployeeId i) =
        let g() =
            try
                let ctx = getContext c

                let e =
                    query {
                        for e in ctx.Dbo.Employee do
                        where (e.ManagedByEmployeeId = Some i)
                        select e.EmployeeId
                    }

                e
                |> List.ofSeq
                |> List.map (fun i -> loadEmployee c (EmployeeId i))
            with
            | e -> [ mapExceptionToError e ]

        g()


    type EmployeeProxy
        with
        static member create c =
            {
                loadEmployee = loadEmployee c
                loadEmployeeByEmail = loadEmployeeByEmail c
                saveEmployee = saveEmployee c
                loadSubordinates = loadSubordinates c
            }