namespace CSharp.Lessons.Functional
{
    using static F;

    public record SetBase<T, TValue>
        where T : SetBase<T, TValue>
        where TValue : IComparable<TValue>
    {
        public TValue Value { get; }
        protected SetBase(TValue value) => Value = value;
        private static Func<TValue, Result<Unit, TError>> NoValidation<TError>() => _ => Ok(Unit);

        protected static Result<T, TError> TryCreate<TError>(
            TValue value,
            Func<TValue, T> creator,
            Func<TValue, Result<Unit, TError>>? validator = null) =>
            (validator ?? NoValidation<TError>())(value).Map<Unit, T, TError>(r => creator(value));
    }
}
