namespace CSharp.Lessons.Functional
{
    public record SetBase<T, TValue>
        where T : SetBase<T, TValue>
        where TValue : IComparable<TValue>
    {
        public TValue Value { get; }
        protected SetBase(TValue value) => Value = value;

        protected Result<T, TError> TryCreate<TError>(
            TValue value,
            Func<TValue, Result<Unit, TError>> validator,
            Func<TValue, T> creator) =>
            validator(value).Map<Unit, T, TError>(r => creator(value));
    }
}
