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
            public OpticsBuilder<Iso<T, V>> Compose(Iso<U, V> other) =>
                new(proxy.Optic.Compose(other));

            public OpticsBuilder<Lens<T, V>> Compose(Lens<U, V> other) =>
                new(proxy.Optic.Compose(other));

            public OpticsBuilder<Optional<T, V>> Compose(Optional<U, V> other) =>
                new(proxy.Optic.Compose(other));
        }

        extension<T, U, V>(OpticsBuilder<BoundIso<T, U>> proxy)
        {
            public OpticsBuilder<BoundIso<T, V>> Compose(Iso<U, V> other) =>
                new(proxy.Optic.Compose(other));

            public OpticsBuilder<BoundLens<T, V>> Compose(Lens<U, V> other) =>
                new(proxy.Optic.Compose(other));

            public OpticsBuilder<BoundOptional<T, V>> Compose(Optional<U, V> other) =>
                new(proxy.Optic.Compose(other));
        }

        extension<T, U, V>(OpticsBuilder<Lens<T, U>> proxy)
        {
            public OpticsBuilder<Lens<T, V>> Compose(Iso<U, V> other) =>
            new(proxy.Optic.Compose(other));

            public OpticsBuilder<Lens<T, V>> Compose(Lens<U, V> other) =>
                new(proxy.Optic.Compose(other));

            public OpticsBuilder<Optional<T, V>> Compose(Optional<U, V> other) =>
                new(proxy.Optic.Compose(other));
        }

        extension<T, U, V>(OpticsBuilder<BoundLens<T, U>> proxy)
        {
            public OpticsBuilder<BoundLens<T, V>> Compose(Iso<U, V> other) =>
            new(proxy.Optic.Compose(other));

            public OpticsBuilder<BoundLens<T, V>> Compose(Lens<U, V> other) =>
                new(proxy.Optic.Compose(other));

            public OpticsBuilder<BoundOptional<T, V>> Compose(Optional<U, V> other) =>
                new(proxy.Optic.Compose(other));
        }

        extension<T, U, V>(OpticsBuilder<Optional<T, U>> proxy)
        {
            public OpticsBuilder<Optional<T, V>> Compose(Iso<U, V> other) =>
                new(proxy.Optic.Compose(other));

            public OpticsBuilder<Optional<T, V>> Compose(Lens<U, V> other) =>
                new(proxy.Optic.Compose(other));

            public OpticsBuilder<Optional<T, V>> Compose(Optional<U, V> other) =>
                new(proxy.Optic.Compose(other));
        }

        extension<T, U, V>(OpticsBuilder<BoundOptional<T, U>> proxy)
        {
            public OpticsBuilder<BoundOptional<T, V>> Compose(Iso<U, V> other) =>
                new(proxy.Optic.Compose(other));

            public OpticsBuilder<BoundOptional<T, V>> Compose(Lens<U, V> other) =>
                new(proxy.Optic.Compose(other));

            public OpticsBuilder<BoundOptional<T, V>> Compose(Optional<U, V> other) =>
                new(proxy.Optic.Compose(other));
        }
    }
}
