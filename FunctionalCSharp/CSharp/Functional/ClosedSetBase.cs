namespace CSharp.Lessons.Functional;

public abstract record ClosedSetBase<TSetElement, TValue, TError>
    : SetBase<TSetElement, TValue, TError>
    where TSetElement : ClosedSetBase<TSetElement, TValue, TError>
    where TValue : IComparable<TValue>
{
    protected ClosedSetBase(TValue value) : base(value)
    {
    }
}
