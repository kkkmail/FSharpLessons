namespace CSharp.Lessons.Primitives;

public abstract record EmailBase<T> : OpenSetBase<T, string, ErrorData>
    where T : OpenSetBase<T, string, ErrorData>
{
    protected EmailBase(string value) : base(value)
    {
    }

    private bool IsValidEmail(string email)
    {
        if (string.IsNullOrEmpty(email)) return false;

        return true;
    }

    protected static Func<string, Result<string, ErrorData>> Validator { get; } =
        v => Ok(v.ToLower().Trim());

    protected static Result<T, ErrorData> TryCreate(
        string email,
        Func<string, T> creator,
        Func<string, Result<string, ErrorData>>? validator = null) =>
        TryCreate(
            email,
            e => creator(e),
            Validator.Compose(r => r.Bind(validator ?? NoValidation())));
}
