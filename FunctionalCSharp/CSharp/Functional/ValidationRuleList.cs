namespace CSharp.Lessons.Functional;

public abstract record ValidationRuleList<T, TError, TRule>
    where TRule : ValidationRule<T, TError>
{
    private ImmutableList<TRule> NonAggregatableRules { get; }
    private ImmutableList<TRule> AggregatableRules { get; }
    private Func<TError, TError, TError> CombineErrors { get; }

    public ValidationRuleList(
        IEnumerable<TRule> rules,
        Func<TError, TError, TError> combineErrors)
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
