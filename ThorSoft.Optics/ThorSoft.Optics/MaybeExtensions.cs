namespace ThorSoft.Optics
{
    /// <summary>
    ///     Extension methods for the <see cref="Maybe{T}"/> type.
    /// </summary>
    public static class MaybeExtensions
    {
        /// <summary>
        ///     Maps the contained value if it exists.
        /// </summary>
        /// <typeparam name="T">The source type contained in the maybe.</typeparam>
        /// <typeparam name="U">The target type of the mapped maybe.</typeparam>
        /// <param name="source">The maybe instance to map.</param>
        /// <param name="map">The map to transform the contained value.</param>
        /// <returns>
        ///     A new <see cref="Maybe{U}"/> instance containing the mapped value if the source contained a value, 
        ///     or <see cref="Maybe.None{T}"/> otherwise.
        /// </returns>
        public static Maybe<U> Map<T, U>(this Maybe<T> source, Func<T, U> map) =>
            source.TryGetValue(out var value)
            ? Maybe.Some(map(value))
            : Maybe.None<U>();
    }
}
