namespace CSharp.Lessons.Functional;

public record SetBase<T, TValue>
    where T : SetBase<T, TValue>
    where TValue : IComparable<TValue>
{
    public TValue Value { get; }
    protected SetBase(TValue value) => Value = value;
    public static Func<TValue, Result<TValue, TError>> NoValidation<TError>() => v => Ok(v);

    protected static Result<T, TError> TryCreate<TError>(
        TValue value,
        Func<TValue, T> creator,
        Func<TValue, Result<TValue, TError>>? validator = null) =>
        (validator ?? NoValidation<TError>())(value).Map(creator);
}
