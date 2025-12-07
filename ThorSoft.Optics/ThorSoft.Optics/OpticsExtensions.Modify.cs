namespace ThorSoft.Optics
{
    public partial class OpticsExtensions
    {
        /// <summary>
        ///     Modifies the value of the property focused by this <see cref="Lens{T,U}"/>.
        /// </summary>
        /// <typeparam name="T">The base type of the lens.</typeparam>
        /// <typeparam name="U">The property type of the lens.</typeparam>
        /// <param name="source">The lens.</param>
        /// <param name="update">The delegate calculating the modified value from the current value of the focused property.</param>
        /// <param name="instance">The instance to modify.</param>
        /// <returns>
        ///     A copy of <typeparamref name="T"/> with the focused property modified.
        /// </returns>
        public static T Modify<T, U>(this Lens<T, U> source, Func<U, U> update, T instance)
        {
            return source.Set(update(source.Get(instance)), instance);
        }

        /// <summary>
        ///     Modifies the value of the property focused by this <see cref="BoundLens{T,U}"/>.
        /// </summary>
        /// <typeparam name="T">The base type of the lens.</typeparam>
        /// <typeparam name="U">The property type of the lens.</typeparam>
        /// <param name="source">The lens.</param>
        /// <param name="update">The delegate calculating the modified value from the current value of the focused property.</param>
        /// <returns>
        ///     A copy of <typeparamref name="T"/> with the focused property modified.
        /// </returns>
        public static T Modify<T, U>(this BoundLens<T, U> source, Func<U, U> update)
        {
            return source.Lens.Modify(update, source.Instance);
        }

        /// <summary>
        ///     Modifies the value of the property focused by this <see cref="Optional{T,U}"/>.
        /// </summary>
        /// <typeparam name="T">The base type of the optional.</typeparam>
        /// <typeparam name="U">The property type of the optional.</typeparam>
        /// <param name="source">The optional.</param>
        /// <param name="update">The delegate calculating the modified value from the current value of the focused property.</param>
        /// <param name="instance">The instance to modify.</param>
        /// <returns>
        ///     A copy of <paramref name="instance"/> with the focused property modified, if it exists.
        /// </returns>
        public static T Modify<T, U>(this Optional<T, U> source, Func<U, U> update, T instance)
        {
            var current = source.Get(instance);
            if (current.TryGetValue(out var value))
            {
                return source.Set(update(value), instance);
            }
            else
            {
                return instance;
            }
        }

        /// <summary>
        ///     Modifies the value of the property focused by this <see cref="BoundOptional{T,U}"/>.
        /// </summary>
        /// <typeparam name="T">The base type of the optional.</typeparam>
        /// <typeparam name="U">The property type of the optional.</typeparam>
        /// <param name="source">The optional.</param>
        /// <param name="update">The delegate calculating the modified value from the current value of the focused property.</param>
        /// <returns>
        ///     A copy of <typeparamref name="T"/> with the focused property modified, if it exists.
        /// </returns>
        public static T Modify<T, U>(this BoundOptional<T, U> source, Func<U, U> update)
        {
            return source.Optional.Modify(update, source.Instance);
        }
    }
}
