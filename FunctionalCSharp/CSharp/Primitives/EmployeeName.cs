namespace CSharp.Lessons.Primitives
{
    using static F;

    public record EmployeeName : OpenSetBase<EmployeeName, string>
    {
        private EmployeeName(string value) : base(value)
        {
        }

        private static Func<string, Result<Unit, ErrorData>> EmployeeNameValidator { get; } = _ => Ok(Unit);

        public static Result<EmployeeName, ErrorData> TryCreate(string name, Func<string, Result<Unit, ErrorData>>? yourOwnValidator = null) =>
            TryCreate(name, n => new EmployeeName(n), yourOwnValidator ?? EmployeeNameValidator);
    }
}
