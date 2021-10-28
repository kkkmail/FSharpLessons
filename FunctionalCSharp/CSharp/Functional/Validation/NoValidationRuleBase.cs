namespace CSharp.Lessons.Functional.Validation;

public record NoValidationRuleBase<T, TRule, TValue, TError>
    : ValidationRule<T, TRule, TValue, TError>
    where T : SetBase<T, TRule, TValue, TError>
    where TRule : ValidationRule<T, TRule, TValue, TError>
    where TValue : IComparable<TValue>
{
    private static Func<TValue, bool> IsValidImpl { get; } = v => true;

    /// <summary>
    /// This should never get hit as <see cref="IsValidImpl"/> always returns true.
    /// </summary>
    private static Func<TValue, TError> GetErrorImpl { get; } = null!;

    protected NoValidationRuleBase() : base(IsValidImpl, GetErrorImpl)
    {
    }
}
