namespace CSharp.Lessons.Functional;

public abstract record SetBase<T, TRule, TValue, TError>
    where T : SetBase<T, TRule, TValue, TError>
    where TRule : ValidationRule<T, TRule, TValue, TError>
    where TValue : IComparable<TValue>
{
    public TValue Value { get; }
    protected SetBase(TValue value) => Value = value;
    public static Func<TValue, Result<TValue, TError>> NoValidation() => v => Ok(v);

    private static Func<TValue, Result<TValue, TError>> ShouldNotBeNull(Func<TValue, TError> errorCreator) =>
        v => ValidationRule<T, TRule, TValue, TError>.ShouldNotBeNull(v) ? Ok(v) : errorCreator(v);

    protected static Result<T, TError> TryCreate(
        TValue value,
        Func<TValue, T> creator,
        Func<TValue, TError> errorCreator,
        Func<TValue, Result<TValue, TError>>? validator = null) =>
        ShouldNotBeNull(errorCreator).Compose(r => r.Bind(validator ?? NoValidation()))(value).Map(creator);
}
