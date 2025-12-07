namespace ThorSoft.Optics
{
    public partial class OpticsExtensions
    {
        /// <summary>
        ///     Reverses the direction of an <see cref="Iso{U, T}"/>."/>
        /// </summary>
        /// <typeparam name="T">The source type of the iso.</typeparam>
        /// <typeparam name="U">The target type of the iso.</typeparam>
        /// <param name="iso">The iso.</param>
        /// <returns>
        ///     A new <see cref="Iso{U, T}"/> representing the same transformation as the original one,
        ///     but with directions reversed."/>
        /// </returns>
        public static Iso<U, T> Reverse<T, U>(this Iso<T, U> iso)
        {
            return new Iso<U, T>(
                iso.InverseMap,
                iso.DirectMap);
        }
    }
}
