namespace CSharp.Lessons.Primitives;

public abstract record OpenSet<TSetElement, TValue>
    : OpenSetBase<TSetElement, TValue, ErrorData>
    where TSetElement : OpenSet<TSetElement, TValue>
    where TValue : IComparable<TValue>
{
    protected OpenSet(TValue value) : base(value)
    {
    }

    public static Result<TSetElement, ErrorData> TryCreate(
        TValue value,
        Func<TValue, TSetElement> creator,
        Func<TValue, Result<TValue, ErrorData>>? validator = null) =>
        TryCreate(
            value,
            creator,
            ErrorData.GetValueCannotBeNull,
            validator);
}
