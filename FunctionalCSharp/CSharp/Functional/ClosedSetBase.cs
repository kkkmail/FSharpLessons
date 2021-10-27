namespace CSharp.Lessons.Functional;

public abstract record ClosedSetBase<T, TValue, TError> : SetBase<T, TValue, TError>
    where T : ClosedSetBase<T, TValue, TError>
    where TValue : IComparable<TValue>
{
    protected ClosedSetBase(TValue value) : base(value)
    {
    }
}
