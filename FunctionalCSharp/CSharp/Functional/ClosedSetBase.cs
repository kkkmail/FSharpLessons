namespace CSharp.Lessons.Functional;

public abstract record ClosedSetBase<T, TValue> : SetBase<T, TValue>
    where T : ClosedSetBase<T, TValue>
    where TValue : IComparable<TValue>
{
    protected ClosedSetBase(TValue value) : base(value)
    {
    }
}
