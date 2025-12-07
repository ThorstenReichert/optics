namespace ThorSoft.Optics
{
    /// <summary>
    ///     Extension methods for the <see cref="Maybe{T}"/> type.
    /// </summary>
    public static class MaybeExtensions
    {
        public static Maybe<U> Map<T, U>(this Maybe<T> source, Func<T, U> map) =>
            source.TryGetValue(out var value)
            ? Maybe.Some(map(value))
            : Maybe.None<U>();
    }
}
