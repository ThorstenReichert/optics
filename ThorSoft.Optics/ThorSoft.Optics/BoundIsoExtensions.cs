using System.Runtime.CompilerServices;

namespace ThorSoft.Optics
{
    /// <summary>
    ///     Extension methods for <see cref="BoundIso{T, U}"/> types.
    /// </summary>
    public static class BoundIsoExtensions
    {
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
