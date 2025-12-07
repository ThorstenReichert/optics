namespace ThorSoft.Optics
{
    /// <summary>
    ///     An optic that focuses on a specific property of target type <typeparamref name="T"/> on the source type <typeparamref name="T"/>,
    ///     that may or may not be present.
    /// </summary>
    /// <typeparam name="T">The source type of the optional.</typeparam>
    /// <typeparam name="U">The target type of the optional.</typeparam>
    public readonly struct Optional<T, U>
    {
        /// <summary>
        ///     Creates a new optional instance with specified getter and setter.
        /// </summary>
        /// <param name="get">The getter of the optional.</param>
        /// <param name="set">The setter of the optional.</param>
        public Optional(Func<T, Maybe<U>> get, Func<U, T, T> set)
        {
            Get = get;
            Set = set;
        }

        /// <summary>
        ///     Returns the value of the focused property, or <see cref="Maybe.None{T}"/> if no such value exists.
        /// </summary>
        /// <returns>
        ///     A <see cref="Maybe{T}"/> instance wrapping the focus of this optional,
        ///     if it exists, or <see cref="Maybe.None{T}"/> if it does not."/>.
        /// </returns>
        public Func<T, Maybe<U>> Get { get; }

        /// <summary>
        ///     Creates a copy with the value of the focused property updated, if it exists.
        /// </summary>
        /// <returns>
        ///     A new instance of <typeparamref name="T"/> with the focus of this optional set to a new <typeparamref name="U"/> value, 
        ///     if it exists, or the original <typeparamref name="T"/> if it does not.
        /// </returns>
        public Func<U, T, T> Set { get; }
    }
}
