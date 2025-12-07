namespace ThorSoft.Optics
{
    /// <summary>
    ///     Extension methods for <see cref="BoundLens{T, U}"/> types.
    /// </summary>
    public static class BoundLensExtensions
    {
        /// <summary>
        ///     Gets the focused property value on the bound instance.
        /// </summary>
        /// <typeparam name="T">The source type of the lens.</typeparam>
        /// <typeparam name="U">The target type of the lens.</typeparam>
        /// <param name="source">The bound lens instance.</param>
        /// <returns>
        ///     The value of the focused property on the bound instance.
        /// </returns>
        public static U Get<T, U>(this BoundLens<T, U> source) =>
            source.Lens.Get(source.Instance);

        /// <summary>
        ///     Sets the focused property on the bound instance.
        /// </summary>
        /// <typeparam name="T">The source type of the lens.</typeparam>
        /// <typeparam name="U">The target type of the lens.</typeparam>
        /// <param name="source">The bound lens instance.</param>
        /// <param name="value">The new value of the focused property.</param>
        /// <returns>
        ///     A copy of the bound instance with the focused value updated to <paramref name="value"/>.
        /// </returns>
        public static T Set<T, U>(this BoundLens<T, U> source, U value) =>
            source.Lens.Set(value, source.Instance);
    }
}
