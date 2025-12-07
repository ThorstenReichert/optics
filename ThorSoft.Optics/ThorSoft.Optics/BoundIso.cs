namespace ThorSoft.Optics
{
    /// <summary>
    ///     An iso together with a fixed <typeparamref name="T"/> instance on which iso operations are acting upon.
    /// </summary>
    /// <typeparam name="T">The source type of the iso.</typeparam>
    /// <typeparam name="U">The target type of the iso.</typeparam>
    public readonly struct BoundIso<T, U>
    {
        /// <summary>
        ///     Creates a new bound iso instance from an iso and a <typeparamref name="T"/> instance.
        /// </summary>
        /// <param name="iso">The unbound iso instance.</param>
        /// <param name="instance">The instance on which the iso operations are acting upon.</param>
        public BoundIso(Iso<T, U> iso, T instance)
        {
            Iso = iso;
            Instance = instance;
        }

        /// <summary>
        ///     The <see cref="Iso{T, U}"/> associated to this <see cref="BoundIso{T, U}"/>."/>
        /// </summary>
        public Iso<T, U> Iso { get; }

        /// <summary>
        ///     The instance associated to this <see cref="BoundIso{T, U}"/>.
        /// </summary>
        public T Instance { get; }
    }
}
