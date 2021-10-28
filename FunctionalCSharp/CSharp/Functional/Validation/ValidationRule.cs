namespace CSharp.Lessons.Functional.Validation;

public abstract record ValidationRule<TRule, TValue, TError>(
    Func<TValue, bool> IsValid,
    Func<TValue, TError> GetError,
    bool CanAggregate = true)
    where TRule : ValidationRule<TRule, TValue, TError>
    where TValue : IComparable<TValue>
{
    public Result<TValue, TError> Validate(TValue value) =>
        IsValid(value) ? Ok(value) : GetError(value);
}
