namespace ThorSoft.Optics
{
    /// <summary>
    ///     Extensions methods for <see cref="BoundOptional{T, U}"/> types.
    /// </summary>
    public static class BoundOptionalExtensions
    {
        /// <summary>
        ///     Gets the focused property value on the bound instance, if it exists.
        /// </summary>
        /// <typeparam name="T">The source type of the optional.</typeparam>
        /// <typeparam name="U">The target type of the optional.</typeparam>
        /// <param name="source">The bound optional instance.</param>
        /// <returns>
        ///     A <see cref="Maybe{U}"/> wrapping the focused property value on the bound instance, if it exists,
        ///     or <see cref="Maybe.None{U}"/> if it does not.
        /// </returns>
        public static Maybe<U> Get<T, U>(this BoundOptional<T, U> source) =>
            source.Optional.Get(source.Instance);

        /// <summary>
        ///     Sets the focused property on the bound instance, if it exists.
        /// </summary>
        /// <typeparam name="T">The source type of the optional.</typeparam>
        /// <typeparam name="U">The target type of the optional.</typeparam>
        /// <param name="source">The bound optional instance.</param>
        /// <param name="value">The new value of the focused property.</param>
        /// <returns>
        ///     A copy of the bound instance with the focused value updated to <paramref name="value"/>, if it exists,
        ///     or the original bound instance if it does not.
        /// </returns>
        public static T Set<T, U>(this BoundOptional<T, U> source, U value) =>
            source.Optional.Set(value, source.Instance);

    }
}
