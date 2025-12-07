namespace ThorSoft.Optics
{
    /// <summary>
    ///     An optional together with a fixed <typeparamref name="T"/> instance on which optional operations are acting upon.
    /// </summary>
    /// <typeparam name="T">The base type of the optional.</typeparam>
    /// <typeparam name="U">The property type of the optionsl</typeparam>
    public readonly struct BoundOptional<T, U>
    {
        /// <summary>
        ///     Creates a new <see cref="BoundOptional{T, U}"/> instance from a <see cref="Optional{T, U}"/> and a <typeparamref name="T"/> instance.
        /// </summary>
        /// <param name="optional">The unbound optional instance.</param>
        /// <param name="instance">The instance on which the optional operations are acting upon.</param>
        public BoundOptional(Optional<T, U> optional, T instance)
        {
            Optional = optional;
            Instance = instance;
        }

        /// <summary>
        ///     The <see cref="Optional{T, U}"/> associated to this <see cref="BoundOptional{T, U}"/>.
        /// </summary>
        public Optional<T, U> Optional { get; }

        /// <summary>
        ///     The instance associated to this <see cref="BoundOptional{T, U}"/>.
        /// </summary>
        public T Instance { get; }
    }
}
