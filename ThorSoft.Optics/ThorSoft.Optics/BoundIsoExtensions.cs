using System.Runtime.CompilerServices;

namespace ThorSoft.Optics
{
    /// <summary>
    ///     Extension methods for <see cref="BoundIso{T, U}"/> types.
    /// </summary>
    public static class BoundIsoExtensions
    {
        /// <summary>
        ///     Creates a bound identity iso for the given source value.
        /// </summary>
        /// <typeparam name="T">The source and target type of the bound iso.</typeparam>
        /// <param name="source">The source instance of the bound iso.</param>
        /// <returns>
        ///     An iso that maps the source value to itself.
        /// </returns>
        public static BoundIso<T, T> Identity<T>(this T source) =>
            new(
                Iso.Identity<T>(),
                source);

        /// <summary>
        ///     Maps the bound instance.
        /// </summary>
        /// <typeparam name="T">The source type of the iso.</typeparam>
        /// <typeparam name="U">The target type of the iso.</typeparam>
        /// <param name="source">The bound iso instance.</param>
        /// <returns>
        ///     The application of the direct map of the bound iso to the bound instance.
        /// </returns>
        public static U DirectMap<T, U>(this BoundIso<T, U> source) =>
            source.Iso.DirectMap(source.Instance);
    }
}
