namespace ThorSoft.Optics
{
    /// <summary>
    ///     Provides factory methods for optics starting at the type <typeparamref name="TSource"/>.
    /// </summary>
    /// <typeparam name="TSource">The type to create optics for.</typeparam>
    public static class Optics<TSource>
    {
        /// <summary>
        ///     Create a new optic starting at <typeparamref name="TSource"/> focusing on the target selected by
        ///     <paramref name="selector"/>.
        /// </summary>
        /// <typeparam name="TOptic">The type of optic being built.</typeparam>
        /// <param name="selector">The focus path of the optic being built.</param>
        /// <returns>
        ///     An optic starting at <typeparamref name="TSource"/> focusing on the target selected by <paramref name="selector"/>.
        /// </returns>
        public static TOptic Focus<TOptic>(Func<OpticsBuilder<Iso<TSource, TSource>>, OpticsBuilder<TOptic>> selector)
        {
            var proxy = new OpticsBuilder<Iso<TSource, TSource>>(Iso.Identity<TSource>());

            return selector(proxy).Optic;
        }

        /// <summary>
        ///     Create a new bound optic starting at <paramref name="source"/> focusing on the target selected by
        ///     <paramref name="selector"/>.
        /// </summary>
        /// <typeparam name="TOptic">The type of optic being built.</typeparam>
        /// <param name="source">The instance to bind the created optic to.</param>
        /// <param name="selector">The focus path of the optic being built.</param>
        /// <returns>
        ///     a bound optic starting at <paramref name="source"/> focusing on the target selected by <paramref name="selector"/>.
        /// </returns>
        public static TOptic Focus<TOptic>(
            TSource source,
            Func<OpticsBuilder<BoundIso<TSource, TSource>>, OpticsBuilder<TOptic>> selector)
        {
            var iso = new BoundIso<TSource, TSource>(Iso.Identity<TSource>(), source);
            var proxy = new OpticsBuilder<BoundIso<TSource, TSource>>(iso);

            return selector(proxy).Optic;
        }
    }
}
