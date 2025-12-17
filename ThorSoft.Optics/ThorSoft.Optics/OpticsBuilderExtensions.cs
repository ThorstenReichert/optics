namespace ThorSoft.Optics
{
    /// <summary>
    ///     Composite methods of optics lifted to the <see cref="OpticsBuilder{TOptic}" />.
    /// </summary>
    /// <remarks>
    ///     Should only be used by generated code.
    /// </remarks>
    public static class OpticsBuilderExtensions
    {
        extension<T, U, V>(OpticsBuilder<Iso<T, U>> proxy)
        {
            /// <summary>
            ///     Composes the iso of the builder with another iso.
            /// </summary>
            /// <param name="other">The iso to compose the builder iso.</param>
            /// <returns>
            ///     A new builder containing the composed iso.
            /// </returns>
            public OpticsBuilder<Iso<T, V>> Compose(Iso<U, V> other) =>
                new(proxy.Optic.Compose(other));

            /// <summary>
            ///     Composes the iso of the builder with another lens.
            /// </summary>
            /// <param name="other">The lens to compose the builder iso.</param>
            /// <returns>
            ///     A new builder containing the composed lens.
            /// </returns>
            public OpticsBuilder<Lens<T, V>> Compose(Lens<U, V> other) =>
                new(proxy.Optic.Compose(other));

            /// <summary>
            ///     Composes the iso of the builder with another optional.
            /// </summary>
            /// <param name="other">The iso to compose the builder optional.</param>
            /// <returns>
            ///     A new builder containing the composed optional.
            /// </returns>
            public OpticsBuilder<Optional<T, V>> Compose(Optional<U, V> other) =>
                new(proxy.Optic.Compose(other));
        }

        extension<T, U, V>(OpticsBuilder<BoundIso<T, U>> proxy)
        {
            /// <summary>
            ///     Composes the bound iso of the builder with another iso.
            /// </summary>
            /// <param name="other">The iso to compose the builder bound iso.</param>
            /// <returns>
            ///     A new builder containing the composed iso.
            /// </returns>
            public OpticsBuilder<BoundIso<T, V>> Compose(Iso<U, V> other) =>
                new(proxy.Optic.Compose(other));

            /// <summary>
            ///     Composes the bound iso of the builder with another lens.
            /// </summary>
            /// <param name="other">The lens to compose the builder bound iso.</param>
            /// <returns>
            ///     A new builder containing the composed bound lens.
            /// </returns>
            public OpticsBuilder<BoundLens<T, V>> Compose(Lens<U, V> other) =>
                new(proxy.Optic.Compose(other));

            /// <summary>
            ///     Composes the bound iso of the builder with another optional.
            /// </summary>
            /// <param name="other">The optional to compose the builder bound iso.</param>
            /// <returns>
            ///     A new builder containing the composed bound optional.
            /// </returns>
            public OpticsBuilder<BoundOptional<T, V>> Compose(Optional<U, V> other) =>
                new(proxy.Optic.Compose(other));
        }

        extension<T, U, V>(OpticsBuilder<Lens<T, U>> proxy)
        {
            /// <summary>
            ///     Composes the lens of the builder with another iso.
            /// </summary>
            /// <param name="other">The iso to compose the builder lens.</param>
            /// <returns>
            ///     A new builder containing the composed lens.
            /// </returns>
            public OpticsBuilder<Lens<T, V>> Compose(Iso<U, V> other) =>
                new(proxy.Optic.Compose(other));

            /// <summary>
            ///     Composes the lens of the builder with another lens.
            /// </summary>
            /// <param name="other">The lens to compose the builder lens.</param>
            /// <returns>
            ///     A new builder containing the composed lens.
            /// </returns>
            public OpticsBuilder<Lens<T, V>> Compose(Lens<U, V> other) =>
                new(proxy.Optic.Compose(other));

            /// <summary>
            ///     Composes the lens of the builder with another optional.
            /// </summary>
            /// <param name="other">The optional to compose the builder lens.</param>
            /// <returns>
            ///     A new builder containing the composed optional.
            /// </returns>
            public OpticsBuilder<Optional<T, V>> Compose(Optional<U, V> other) =>
                new(proxy.Optic.Compose(other));
        }

        extension<T, U, V>(OpticsBuilder<BoundLens<T, U>> proxy)
        {
            /// <summary>
            ///     Composes the bound lens of the builder with another iso.
            /// </summary>
            /// <param name="other">The iso to compose the builder bound lens.</param>
            /// <returns>
            ///     A new builder containing the composed bound lens.
            /// </returns>
            public OpticsBuilder<BoundLens<T, V>> Compose(Iso<U, V> other) =>
                new(proxy.Optic.Compose(other));

            /// <summary>
            ///     Composes the bound lens of the builder with another lens.
            /// </summary>
            /// <param name="other">The lens to compose the builder bound lens.</param>
            /// <returns>
            ///     A new builder containing the composed bound lens.
            /// </returns>
            public OpticsBuilder<BoundLens<T, V>> Compose(Lens<U, V> other) =>
                new(proxy.Optic.Compose(other));

            /// <summary>
            ///     Composes the bound lens of the builder with another optional.
            /// </summary>
            /// <param name="other">The optional to compose the builder bound lens.</param>
            /// <returns>
            ///     A new builder containing the composed bound optional.
            /// </returns>
            public OpticsBuilder<BoundOptional<T, V>> Compose(Optional<U, V> other) =>
                new(proxy.Optic.Compose(other));
        }

        extension<T, U, V>(OpticsBuilder<Optional<T, U>> proxy)
        {
            /// <summary>
            ///     Composes the optional of the builder with another iso.
            /// </summary>
            /// <param name="other">The iso to compose the builder optional.</param>
            /// <returns>
            ///     A new builder containing the composed optional.
            /// </returns>
            public OpticsBuilder<Optional<T, V>> Compose(Iso<U, V> other) =>
                new(proxy.Optic.Compose(other));

            /// <summary>
            ///     Composes the optional of the builder with another lens.
            /// </summary>
            /// <param name="other">The lens to compose the builder optional.</param>
            /// <returns>
            ///     A new builder containing the composed optional.
            /// </returns>
            public OpticsBuilder<Optional<T, V>> Compose(Lens<U, V> other) =>
                new(proxy.Optic.Compose(other));

            /// <summary>
            ///     Composes the optional of the builder with another optional.
            /// </summary>
            /// <param name="other">The optional to compose the builder optional.</param>
            /// <returns>
            ///     A new builder containing the composed optional.
            /// </returns>
            public OpticsBuilder<Optional<T, V>> Compose(Optional<U, V> other) =>
                new(proxy.Optic.Compose(other));
        }

        extension<T, U, V>(OpticsBuilder<BoundOptional<T, U>> proxy)
        {
            /// <summary>
            ///     Composes the bound optional of the builder with another iso.
            /// </summary>
            /// <param name="other">The iso to compose the builder bound optional.</param>
            /// <returns>
            ///     A new builder containing the composed bound optional.
            /// </returns>
            public OpticsBuilder<BoundOptional<T, V>> Compose(Iso<U, V> other) =>
                new(proxy.Optic.Compose(other));

            /// <summary>
            ///     Composes the bound optional of the builder with another lens.
            /// </summary>
            /// <param name="other">The lens to compose the builder bound optional.</param>
            /// <returns>
            ///     A new builder containing the composed bound optional.
            /// </returns>
            public OpticsBuilder<BoundOptional<T, V>> Compose(Lens<U, V> other) =>
                new(proxy.Optic.Compose(other));

            /// <summary>
            ///     Composes the bound optional of the builder with another optional.
            /// </summary>
            /// <param name="other">The optional to compose the builder bound optional.</param>
            /// <returns>
            ///     A new builder containing the composed bound optional.
            /// </returns>
            public OpticsBuilder<BoundOptional<T, V>> Compose(Optional<U, V> other) =>
                new(proxy.Optic.Compose(other));
        }
    }
}
