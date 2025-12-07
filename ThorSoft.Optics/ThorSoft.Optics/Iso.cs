namespace ThorSoft.Optics
{
    /// <summary>
    ///     An optic that losslessly transforms between types <typeparamref name="T"/> and <typeparamref name="U"/>.
    /// </summary>
    /// <typeparam name="T">The source type of the iso.</typeparam>
    /// <typeparam name="U">The target type of the iso.</typeparam>
    public readonly struct Iso<T, U>
    {
        /// <summary>
        ///     Creates a new iso with specified conversion functions.
        /// </summary>
        /// <param name="directMap">The direct map of the iso.</param>
        /// <param name="inverseMap">The inverse map of the iso.</param>
        public Iso(Func<T, U> directMap, Func<U, T> inverseMap)
        {
            DirectMap = directMap;
            InverseMap = inverseMap;
        }

        /// <summary>
        ///     Maps the source type of the iso to its target type.
        /// </summary>
        public Func<T, U> DirectMap { get; }

        /// <summary>
        ///     Maps the target type of the iso to its source type.
        /// </summary>
        public Func<U, T> InverseMap { get; }
    }

    /// <summary>
    ///     Factory methods for <see cref="Iso{T, U}"/> types.
    /// </summary>
    public static class Iso
    {
        /// <summary>
        ///     An iso representing the identity mapping.
        /// </summary>
        /// <typeparam name="T">The mapped type of the iso.</typeparam>
        /// <returns>
        ///     An iso that maps each value to itself.
        /// </returns>
        public static Iso<T, T> Identity<T>() =>
            new(
                static t => t,
                static u => u);
    }
}
