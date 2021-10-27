namespace CSharp.Lessons.Functional;

public abstract record ValidationRule<T, TError>(
    Func<T, bool> IsValid,
    Func<T, TError> GetError,
    bool CanAggregate = true)
{
    public Result<T, TError> Validate(T value) =>
        IsValid(value) ? Ok(value) : GetError(value);
}
