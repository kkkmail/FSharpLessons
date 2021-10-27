namespace CSharp.Lessons.Functional.Validation;

public abstract record AggregateValidationRule<T, TValue, TError> : ValidationRule<T, TValue, TError>
    where T : SetBase<T, TValue, TError>
    where TValue : IComparable<TValue>
{
    private ImmutableList<TRule> NonAggregatableRules { get; }
    private ImmutableList<TRule> AggregatableRules { get; }
    private Func<TError, TError, TError> CombineErrors { get; }

    public AggregateValidationRule(
        IEnumerable<TRule> rules,
        Func<TError, TError, TError> combineErrors) : base(
            IsValid: null!,
            GetError: null!,
            false)
    {
        var (t, f) = rules.Partition(e => e.CanAggregate);
        AggregatableRules = t.ToImmutableList();
        NonAggregatableRules = f.ToImmutableList();
        CombineErrors = combineErrors;
    }

    public Result<T, TError> Validate(T value, bool failOnFirst = false)
    {
        return default;
    }
}
