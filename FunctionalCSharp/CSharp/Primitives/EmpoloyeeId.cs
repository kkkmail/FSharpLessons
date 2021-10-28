namespace CSharp.Lessons.Primitives;

public record EmpoloyeeId
    : OpenSetBase<EmpoloyeeId, NoValidationRuleBase<EmpoloyeeId, long>, long, ErrorData>
{
    public EmpoloyeeId(long value) : base(value)
    {
    }
}
