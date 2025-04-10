
// Source: https://github.com/andrewlock/StronglyTypedId/blob/6bd17db4a4b700eaad9e209baf41478cc3f0bbe9/src/StronglyTypedIds/EquatableArray.cs

using System.Collections;
using System.Runtime.InteropServices;

namespace ThorSoft.Optics.Generator.Util
{
    /// <summary>
    ///     An immutable, equatable collection. This is equivalent to <see cref="ReadOnlyMemory{T}"/> but with value equality support.
    /// </summary>
    /// <typeparam name="T">The type of values in the collection.</typeparam>
    internal readonly struct EquatableMemory<T> : IEquatable<EquatableMemory<T>>, IEnumerable<T>
        where T : IEquatable<T>
    {
        public static readonly EquatableMemory<T> Empty = new([]);

        /// <summary>
        ///     The underlying <typeparamref name="T"/> collection.
        /// </summary>
        private readonly ReadOnlyMemory<T> _memory;

        /// <summary>
        ///     Creates a new <see cref="EquatableMemory{T}"/> instance.
        /// </summary>
        /// <param name="memory">The input to wrap.</param>
        public EquatableMemory(ReadOnlyMemory<T> memory)
        {
            _memory = memory;
        }

        /// <summary>
        ///     Creates a new <see cref="EquatableMemory{T}"/> instance.
        /// </summary>
        /// <param name="array">The items to wrap.</param>
        public EquatableMemory(params T[] array)
        {
            _memory = array;
        }

        /// <sinheritdoc/>
        public bool Equals(EquatableMemory<T> memory)
        {
            return AsSpan().SequenceEqual(memory.AsSpan());
        }

        /// <sinheritdoc/>
        public override bool Equals(object? obj)
        {
            return obj is EquatableMemory<T> memory && Equals(memory);
        }

        /// <sinheritdoc/>
        public override int GetHashCode()
        {
            HashCode hashCode = default;
            ReadOnlySpan<T> span = _memory.Span;

            for (var i = 0; i < span.Length; i++)
            {
                hashCode.Add(span[i]);
            }

            return hashCode.ToHashCode();
        }

        /// <summary>
        ///     Returns a <see cref="ReadOnlySpan{T}"/> wrapping the current items.
        /// </summary>
        /// <returns>A <see cref="ReadOnlySpan{T}"/> wrapping the current items.</returns>
        public ReadOnlySpan<T> AsSpan()
        {
            return _memory.Span;
        }

        /// <summary>
        ///     Gets the underlying memory if there is one
        /// </summary>
        public ReadOnlyMemory<T>? AsMemory() => _memory;

        /// <sinheritdoc/>
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return MemoryMarshal.ToEnumerable(_memory).GetEnumerator();
        }

        /// <sinheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return MemoryMarshal.ToEnumerable(_memory).GetEnumerator();
        }

        public int Count => _memory.Length;

        /// <summary>
        ///     Checks whether two <see cref="EquatableMemory{T}"/> values are the same.
        /// </summary>
        /// <param name="left">The first <see cref="EquatableMemory{T}"/> value.</param>
        /// <param name="right">The second <see cref="EquatableMemory{T}"/> value.</param>
        /// <returns>Whether <paramref name="left"/> and <paramref name="right"/> are equal.</returns>
        public static bool operator ==(EquatableMemory<T> left, EquatableMemory<T> right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Checks whether two <see cref="EquatableMemory{T}"/> values are not the same.
        /// </summary>
        /// <param name="left">The first <see cref="EquatableMemory{T}"/> value.</param>
        /// <param name="right">The second <see cref="EquatableMemory{T}"/> value.</param>
        /// <returns>Whether <paramref name="left"/> and <paramref name="right"/> are not equal.</returns>
        public static bool operator !=(EquatableMemory<T> left, EquatableMemory<T> right)
        {
            return !left.Equals(right);
        }
    }
}
