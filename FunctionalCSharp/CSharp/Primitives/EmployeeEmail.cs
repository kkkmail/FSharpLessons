namespace CSharp.Lessons.Primitives;

public record EmployeeEmail : EmailBase<EmployeeEmail>
{
    private EmployeeEmail(string value) : base(value)
    {
    }

    public static Result<EmployeeEmail, ErrorData> TryCreate(
        string email,
        Func<string, Result<string, ErrorData>>? validator = null) =>
        TryCreate(email, e => new EmployeeEmail(e), validator);
}
