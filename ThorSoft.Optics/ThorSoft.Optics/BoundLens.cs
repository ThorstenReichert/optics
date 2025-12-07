namespace ThorSoft.Optics
{
    /// <summary>
    ///     A lens together with a fixed <typeparamref name="T"/> instance on which lens operations are acting upon.
    /// </summary>
    /// <typeparam name="T">The source type of the lens.</typeparam>
    /// <typeparam name="U">The target type of the lens.</typeparam>
    public readonly struct BoundLens<T, U>
    {
        /// <summary>
        ///     Creates a new bound lens instance from a lens and a <typeparamref name="T"/> instance.
        /// </summary>
        /// <param name="lens">The unbound lens instance.</param>
        /// <param name="instance">The instance on which the lens operations are acting upon.</param>
        public BoundLens(Lens<T, U> lens, T instance)
        {
            Lens = lens;
            Instance = instance;
        }

        /// <summary>
        ///     The <see cref="Lens{T, U}"/> associated to this <see cref="BoundLens{T, U}"/>.
        /// </summary>
        public Lens<T, U> Lens { get; }

        /// <summary>
        ///     The instance associated to this <see cref="BoundLens{T, U}"/>.
        /// </summary>
        public T Instance { get; }
    }
}
