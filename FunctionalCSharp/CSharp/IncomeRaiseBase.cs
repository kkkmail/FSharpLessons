namespace CSharp.Lessons;

public abstract record IncomeRaiseBase(Func<Employee, Employee> RaiseSalary);
