namespace ThorSoft.Optics
{
    /// <summary>
    ///     A lens together with a fixed <typeparamref name="T"/> instance on which lens operations are acting upon.
    /// </summary>
    /// <typeparam name="T">The base type of the lens.</typeparam>
    /// <typeparam name="U">The property type of the lens.</typeparam>
    public readonly struct BoundLens<T, U>
    {
        /// <summary>
        ///     Creates a new <see cref="BoundLens{T, U}"/> instance from a <see cref="Lens{T, U}"/> and a <typeparamref name="T"/> instance.
        /// </summary>
        /// <param name="lens">The general lens.</param>
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

        /// <summary>
        ///     Returns the value of the property focused by the lens.
        /// </summary>
        public U Get() => Lens.Get(Instance);

        /// <summary>
        ///     Creates a copy with the value of the focused property updated.
        /// </summary>
        public T Set(U value) => Lens.Set(value, Instance);
    }
}
