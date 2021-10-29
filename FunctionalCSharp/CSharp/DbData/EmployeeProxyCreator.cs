namespace CSharp.Lessons.DbData;

public static class EmployeeProxyCreator
{

    private static Func<EmployeeId, Result<Employee, ErrorData>> LoadEmployee(this ConnectionString c) =>
        null;

    private static Func<EmployeeEmail, Result<Employee, ErrorData>> LoadEmployeeByEmail(this ConnectionString c) =>
        null;

    private static Func<Employee, Result<Employee, ErrorData>> SaveEmployee(this ConnectionString c) =>
        null;

    private static Func<EmployeeId, ImmutableList<Result<Employee, ErrorData>>> LoadSubordinates(this ConnectionString c) =>
        null;


    public static EmployeeProxy CreateEmployeeProxy(this ConnectionString c)
    {
        return new EmployeeProxy(
            LoadEmployee: LoadEmployee(c),
            LoadEmployeeByEmail: LoadEmployeeByEmail(c),
            SaveEmployee: SaveEmployee(c),
            LoadSubordinates: LoadSubordinates(c));
    }
}
