namespace CSharp.Lessons.BusinessEntities;

public record Employee(
    EmployeeId EmployeeId,
    EmployeeName EmployeeName,
    EmployeeEmail EmployeeEmail,
    EmployeeId ManagedBy,
    DateTime DateHired,
    decimal Salary,
    string Description,
    ImmutableDictionary<EmployeeDataType, EmployeeData> Data);
