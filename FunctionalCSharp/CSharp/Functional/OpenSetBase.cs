namespace CSharp.Lessons.Functional;

public abstract record OpenSetBase<T, TRule, TValue, TError> : SetBase<T, TRule, TValue, TError>
    where T : OpenSetBase<T, TRule, TValue, TError>
    where TRule : ValidationRule<T, TRule, TValue, TError>
    where TValue : IComparable<TValue>
{
    protected OpenSetBase(TValue value) : base(value)
    {
    }
}
