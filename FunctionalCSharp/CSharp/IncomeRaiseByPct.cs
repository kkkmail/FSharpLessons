namespace CSharp.Lessons;

public record IncomeRaiseByPct : IncomeRaiseBase
{
    private IncomeRaiseByPct(Func<Employee, Employee> RaiseSalary) : base(RaiseSalary)
    {
    }

    //public static IncomeRaiseByPct
}
