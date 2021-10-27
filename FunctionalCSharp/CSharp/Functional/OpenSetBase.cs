namespace CSharp.Lessons.Functional;

public abstract record OpenSetBase<T, TValue, TError> : SetBase<T, TValue, TError>
    where T : OpenSetBase<T, TValue, TError>
    where TValue : IComparable<TValue>
{
    protected OpenSetBase(TValue value) : base(value)
    {
    }
}
