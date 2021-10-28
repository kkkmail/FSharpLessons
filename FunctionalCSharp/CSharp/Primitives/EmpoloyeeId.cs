namespace CSharp.Lessons.Primitives;

public record EmpoloyeeId : OpenSetBase<EmpoloyeeId, long, ErrorData>
{
    /// <summary>
    /// Any values are allowed.
    /// </summary>
    public EmpoloyeeId(long value) : base(value)
    {
    }
}
