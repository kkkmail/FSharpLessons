namespace CSharp.Lessons.Primitives;

public record EmpoloyeeId : OpenSetBase<EmpoloyeeId, long, ErrorData>
{
    public EmpoloyeeId(long value) : base(value)
    {
    }
}
