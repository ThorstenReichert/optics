using System.Linq.Expressions;

namespace ThorSoft.Optics
{
    public partial class OpticsExtensions
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
        public static BoundLens<T, U> Focus<T, U>(this T instance, Expression<Func<T, U>> propertySelector) =>
            throw MarkerMethod();

        private static NotImplementedException MarkerMethod() =>
            new("Method is only a marker to be intercepted via the Focus() source-generator");
    }
}
