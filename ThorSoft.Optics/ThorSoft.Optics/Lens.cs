namespace ThorSoft.Optics
{
    /// <summary>
    ///     An optic that focuses a specific property of target type <typeparamref name="T"/> on the source type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The source type of the lens.</typeparam>
    /// <typeparam name="U">The target type of the lens.</typeparam>
    public readonly struct Lens<T, U>
    {
        /// <summary>
        ///     Creates a new lens instance with specified getter and setter.
        /// </summary>
        /// <param name="get">The getter of the lens.</param>
        /// <param name="set">The setter of the lens.</param>
        public Lens(Func<T, U> get, Func<U, T, T> set)
        {
            Get = get;
            Set = set;
        }

        /// <summary>
        ///     Returns the value of the property focused by the lens.
        /// </summary>
        public Func<T, U> Get { get; }

        /// <summary>
        ///     Creates a copy with the value of the focused property updated.
        /// </summary>
        public Func<U, T, T> Set { get; }
    }
}
