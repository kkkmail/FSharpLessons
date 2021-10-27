namespace CSharp.Lessons.Primitives;

public record EmpoloyeeId : OpenSetBase<EmpoloyeeId, long>
{
    public EmpoloyeeId(long value) : base(value)
    {
    }
}
