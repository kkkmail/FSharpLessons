namespace CSharp.Lessons.Functional;

public abstract record OpenSetBase<T, TValue> : SetBase<T, TValue>
    where T : OpenSetBase<T, TValue>
    where TValue : IComparable<TValue>
{
    protected OpenSetBase(TValue value) : base(value)
    {
    }
}
