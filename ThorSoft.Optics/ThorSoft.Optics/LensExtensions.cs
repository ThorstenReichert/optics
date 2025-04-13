using System.Linq.Expressions;

namespace ThorSoft.Optics
{
    /// <summary>
    ///     Extension methods for <see cref="Lens{T, U}"/> and <see cref="BoundLens{T, U}"/> types.
    /// </summary>
    public static class LensExtensions
    {
        /// <summary>
        ///     Generate a <see cref="BoundLens{T, U}"/> that focuses in on a nested property of <typeparamref name="T"/> 
        ///     through the path <paramref name="propertySelector"/>.
        /// </summary>
        /// <typeparam name="T">The base type of the resulting lens.</typeparam>
        /// <typeparam name="U">The type of the property selected through <paramref name="propertySelector"/>.</typeparam>
        /// <param name="instance">The instance for which the <see cref="BoundLens{T, U}"/> shall be constructed.</param>
        /// <param name="propertySelector">The expression selecting the nested property to focus on.</param>
        /// <returns>
        ///     A <see cref="BoundLens{T, U}"/> focusing on the nested property selected by <paramref name="propertySelector"/>.
        /// </returns>
        public static BoundLens<T, U> Focus<T, U>(this T instance, Expression<Func<T, U>> propertySelector)
        {
            throw new NotImplementedException("Method is only a marker to be intercepted via the accompanying source-generator");
        }

        /// <summary>
        ///     Composes two compatible lenses to create a total lens representing the application of one lens after the other.
        /// </summary>
        /// <typeparam name="T">The base type of the outer lens.</typeparam>
        /// <typeparam name="U">The base type of the inner lens and the property type of the outer lens.</typeparam>
        /// <typeparam name="V">The property type of the inner lens.</typeparam>
        /// <param name="source">The outer lens.</param>
        /// <param name="nested">The inner lens.</param>
        /// <returns>
        ///     A lens representing property access through following first <paramref name="source"/> then <paramref name="nested"/>.
        /// </returns>
        public static Lens<T, V> Compose<T, U, V>(this Lens<T, U> source, Lens<U, V> nested)
        {
            return new Lens<T, V>(
                instance => nested.Get(source.Get(instance)),
                (value, instance) => source.Set(nested.Set(value, source.Get(instance)), instance));
        }

        /// <summary>
        ///     Composes two compatible lenses to create a total lens representing the application of one lens after the other.
        /// </summary>
        /// <typeparam name="T">The base type of the outer lens.</typeparam>
        /// <typeparam name="U">The base type of the inner lens and the property type of the outer lens.</typeparam>
        /// <typeparam name="V">The property type of the inner lens.</typeparam>
        /// <param name="source">The outer lens.</param>
        /// <param name="nested">The inner lens.</param>
        /// <returns>
        ///     A lens representing property access through following first <paramref name="source"/> then <paramref name="nested"/>.
        /// </returns>
        public static BoundLens<T, V> Compose<T, U, V>(this BoundLens<T, U> source, Lens<U, V> nested)
        {
            return new BoundLens<T, V>(
                source.Lens.Compose(nested),
                source.Instance);
        }

        /// <summary>
        ///     Composes two compatible lenses to create a total lens representing the application of one lens after the other.
        /// </summary>
        /// <typeparam name="T">The base type of the outer lens.</typeparam>
        /// <typeparam name="U">The base type of the inner lens and the property type of the outer lens.</typeparam>
        /// <typeparam name="V">The property type of the inner lens.</typeparam>
        /// <param name="source">The outer lens.</param>
        /// <param name="nested">The inner lens.</param>
        /// <returns>
        ///     A lens representing property access through following first <paramref name="source"/> then <paramref name="nested"/>.
        /// </returns>
        public static Lens<T, V> Compose<T, U, V>(this Lens<T, U> source, BoundLens<U, V> nested)
        {
            return source.Compose(nested.Lens);
        }

        /// <summary>
        ///     Composes two compatible lenses to create a total lens representing the application of one lens after the other.
        /// </summary>
        /// <typeparam name="T">The base type of the outer lens.</typeparam>
        /// <typeparam name="U">The base type of the inner lens and the property type of the outer lens.</typeparam>
        /// <typeparam name="V">The property type of the inner lens.</typeparam>
        /// <param name="source">The outer lens.</param>
        /// <param name="nested">The inner lens.</param>
        /// <returns>
        ///     A lens representing property access through following first <paramref name="source"/> then <paramref name="nested"/>.
        /// </returns>
        public static BoundLens<T, V> Compose<T, U, V>(this BoundLens<T, U> source, BoundLens<U, V> nested)
        {
            return source.Compose(nested.Lens);
        }

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
            return source.Set(update(source.Get()));
        }
    }
}
