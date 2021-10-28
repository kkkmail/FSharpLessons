namespace CSharp.Lessons.Functional.Validation;

public abstract record ValidationRule<T, TRule, TValue, TError>(
    Func<TValue, bool> IsValid,
    Func<TValue, TError> GetError,
    bool CanAggregate = true)
    where T : SetBase<T, TRule, TValue, TError>
    where TRule : ValidationRule<T, TRule, TValue, TError>
    where TValue : IComparable<TValue>
{
    public Result<TValue, TError> Validate(TValue value) =>
        IsValid(value) ? Ok(value) : GetError(value);

    /// <summary>
    /// Disallowes reference types to be nulls and let any value type values pass.
    /// https://stackoverflow.com/questions/65351/null-or-default-comparison-of-generic-argument-in-c-sharp
    /// </summary>
    public static bool ShouldNotBeNull(TValue v) =>
            typeof(TValue).IsValueType || !EqualityComparer<TValue>.Default.Equals(v, default(TValue));
}
