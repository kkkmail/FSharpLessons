namespace CSharp.Lessons.Primitives;

public record EmployeeName : OpenSetBase<EmployeeName, string>
{
    private EmployeeName(string value) : base(value)
    {
    }

    public static Func<string, Result<string, ErrorData>> Validator { get; } =
        v => Ok(v.ToUpper().Trim());

    public static Result<EmployeeName, ErrorData> TryCreate(
        string name,
        Func<string, Result<string, ErrorData>>? validator = null) =>
        TryCreate(
            name,
            n => new EmployeeName(n),
            Validator.Compose(r => r.Bind(validator ?? NoValidation<ErrorData>())));
}
