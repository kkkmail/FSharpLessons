// This looks ugly.
// Unfortunately C# 10 requires fully qualified names here and cannot use already known namespaces.
using EmployeeResult = CSharp.Lessons.Functional.Result<CSharp.Lessons.BusinessEntities.Employee, CSharp.Lessons.Primitives.ErrorData>;

namespace CSharp.Lessons.DbData;

public static class EmployeeProxyCreator
{
    private static ErrorData MapExceptionToError(Exception e) =>
        new ErrorData($"Exception: '{e}'.");

    private static ErrorData ToInvalidDataError(string s) =>
        new ErrorData($"Some data is invalid: '{s}'.");

    private static Result<T, ErrorData> TryDbFun<T>(Func<T> f)
    {
        try
        {
            return f();
        }
        catch (Exception e)
        {
            return MapExceptionToError(e);
        }
    }

    private static EmployeeResult MapEmployee(EFEmployee employee)
    {
        var name = EmployeeName.TryCreate(employee.EmployeeName);
        var email = EmployeeEmail.TryCreate(employee.EmployeeEmail);

        return name.Match(
            n => email.Match(
                e => (EmployeeResult)new Employee(
                    EmployeeId: new EmployeeId(employee.EmployeeId),
                    EmployeeName: n,
                    EmployeeEmail: e,
                    ManagedBy: new EmployeeId(employee.ManagedByEmployeeId ?? 0),
                    DateHired: employee.DateHired,
                    Salary: employee.Salary,
                    Description: employee.Description ?? "",
                    Data: ImmutableDictionary<EmployeeDataType, EmployeeData>.Empty),
                err => err),
            err => err);
    }

    private static Result<EmployeeData, ErrorData> MapEmployeeData(EFEmployeeData employeeData) =>
        EmployeeDataType.TryGetElement(employeeData.EmployeeDataTypeId)
            .Map(e => new EmployeeData(
                EmployeeDataType: e,
                EmploeeDataValue: employeeData.EmployeeDataValue ?? ""));

    private static Func<EmployeeId, EmployeeResult> LoadEmployee(this ConnectionString c) =>
        null;

    private static Func<EmployeeEmail, EmployeeResult> LoadEmployeeByEmail(this ConnectionString c) =>
        null;

    private static Func<Employee, EmployeeResult> SaveEmployee(this ConnectionString c) =>
        null;

    private static Func<EmployeeId, ImmutableList<EmployeeResult>> LoadSubordinates(this ConnectionString c) =>
        null;


    public static EmployeeProxy CreateEmployeeProxy(this ConnectionString c)
    {
        return new EmployeeProxy(
            LoadEmployee: c.LoadEmployee(),
            LoadEmployeeByEmail: c.LoadEmployeeByEmail(),
            SaveEmployee: c.SaveEmployee(),
            LoadSubordinates: c.LoadSubordinates());
    }
}
