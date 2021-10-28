namespace CSharp.Lessons.Primitives.Validation;

public record NoValidationRule<T, TValue> : NoValidationRuleBase<T, TValue, ErrorData>
    where T : SetBase<T, NoValidationRuleBase<T, TValue, ErrorData>, TValue, ErrorData>
    where TValue : IComparable<TValue>
{
    //public NoValidationRule()
    //{
    //}
}
