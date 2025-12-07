namespace ThorSoft.Optics
{
    /// <summary>
    ///     Represents an optional value that may or may not be present.
    /// </summary>
    /// <typeparam name="T">The contained type of the maybe instance.</typeparam>
    public readonly struct Maybe<T>
    {
        private readonly T _value;
        private readonly bool _hasValue;

        /// <summary>
        ///     Creates a new <see cref="Maybe{T}"/> instance with no value.
        /// </summary>
        public Maybe()
        {
            _hasValue = false;
            _value = default!;
        }

        /// <summary>
        ///     Creates a new <see cref="Maybe{T}"/> instance that contains the specified value.
        /// </summary>
        /// <param name="value">The value to be wrapped in this <see cref="Maybe{T}"/> instance.</param>
        public Maybe(T value)
        {
            _hasValue = true;
            _value = value;
        }

        /// <summary>
        ///     Attempts to retrieve the stored value if one is available.
        /// </summary>
        /// <param name="value">The wrapped value, if available.</param>
        /// <returns>
        ///     <c>true</c> if a value is available and was returned in <paramref name="value"/>, <c>false</c> otherwise.
        /// </returns>
        public bool TryGetValue(out T value)
        {
            if (_hasValue)
            {
                value = _value;
                return true;
            }
            else
            {
                value = default!;
                return false;
            }
        }
    }

    /// <summary>
    ///     Factory methods for the <see cref="Maybe{T}"/> type.
    /// </summary>
    public static class Maybe
    {
        /// <summary>
        ///     Creates a <see cref="Maybe{T}"/> instance with no value.
        /// </summary>
        /// <typeparam name="T">The contained type of the maybe instance.</typeparam>
        /// <returns>
        ///     A <see cref="Maybe{T}"/> instance with no value.
        /// </returns>
        public static Maybe<T> None<T>() => new();

        /// <summary>
        ///     Creates a new <see cref="Maybe{T}"/> instance that contains the specified value.
        /// </summary>
        /// <typeparam name="T">The contained type of the maybe instance.</typeparam>
        /// <param name="value">The value to be wrapped in the <see cref="Maybe{T}"/>.</param>
        /// <returns>
        ///     A <see cref="Maybe{T}"/> representing a value that is present.
        /// </returns>
        public static Maybe<T> Some<T>(T value) => new(value);
    }
}
