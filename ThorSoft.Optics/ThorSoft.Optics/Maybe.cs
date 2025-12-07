namespace ThorSoft.Optics
{
    /// <summary>
    ///     Represents an optional value that may or may not be present.
    /// </summary>
    /// <typeparam name="T">The contained type of the maybe instance.</typeparam>
    public readonly struct Maybe<T> : IEquatable<Maybe<T>>
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
        ///     Compares two <see cref="Maybe{T}"/> instances for equality.
        /// </summary>
        /// <remarks>
        ///     Uses the default equality comparer for type <typeparamref name="T"/> for comparisons.
        /// </remarks>
        /// <param name="left">The left instance to compare.</param>
        /// <param name="right">The right instance to compare.</param>
        /// <returns>
        ///     <c>true</c> if both instances have values and those values are equal, or if both instances are empty;
        ///     <c>false</c> otherwise.
        /// </returns>
        public static bool operator ==(Maybe<T> left, Maybe<T> right) =>
            left.Equals(right);

        /// <summary>
        ///     Compares two <see cref="Maybe{T}"/> instances for inequality.
        /// </summary>
        /// <remarks>
        ///     Uses the default equality comparer for type <typeparamref name="T"/> for comparisons.
        /// </remarks>
        /// <param name="left">The left instance to compare.</param>
        /// <param name="right">The right instance to compare.</param>
        /// <returns>
        ///     <c>true</c> if both instances have values and those values are not equal, or if either one or the other is empty;
        ///     <c>false</c> otherwise.
        /// </returns>
        public static bool operator !=(Maybe<T> left, Maybe<T> right) =>
            !left.Equals(right);

        /// <summary>
        ///     Compares this <see cref="Maybe{T}"/> instance to another for equality.
        /// </summary>
        /// <remarks>
        ///     Uses the default equality comparer for type <typeparamref name="T"/> for comparisons.
        /// </remarks>
        /// <param name="other">The maybe instance to compare to.</param>
        /// <returns>
        ///     <c>true</c> if both instances have values and those values are equal, or if both instances are empty;
        ///     <c>false</c> otherwise.
        /// </returns>
        public bool Equals(Maybe<T> other)
        {
            if (_hasValue && other._hasValue)
            {
                return EqualityComparer<T>.Default.Equals(_value, other._value);
            }
            else if (!_hasValue && !other._hasValue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <inheritdoc cref="object.Equals(object)"/>
        public override bool Equals(object obj)
        {
            if (obj is Maybe<T> other)
            {
                return Equals(other);
            }
            else
            {
                return false;
            }
        }

        /// <inheritdoc cref="object.GetHashCode"/>
        public override int GetHashCode()
        {
            return _hasValue.GetHashCode() * 17 + _value?.GetHashCode() ?? 0;
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
