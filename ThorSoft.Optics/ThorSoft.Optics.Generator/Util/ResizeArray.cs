using System.Diagnostics;

namespace ThorSoft.Optics.Generator.Util
{
    /// <summary>
    ///     Array that grows when adding items and whose backing storage can be extracted.
    /// </summary>
    /// <typeparam name="T">The type of items to store.</typeparam>
    internal sealed class ResizeArray<T>
    {
        private T[]? _array;
        private int _count;

        /// <summary>
        ///     Creates a new, empty <see cref="ResizeArray{T}"/> instance.
        /// </summary>
        public ResizeArray()
        {
            _array = [];
            _count = 0;
        }

        public int Count => _count;

        /// <summary>
        ///     Add <paramref name="item"/> to the backing array. Resizes backing array if necessary.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void Add(T item)
        {
            CheckExtracted();

            Debug.Assert(_array is not null);

            if (_count == _array!.Length)
            {
                Resize();
            }

            _array[_count] = item;
            _count += 1;
        }

        /// <summary>
        ///     Extracts the backing storage from this <see cref="ResizeArray{T}"/> instance. No further operations on this instance will be allowed.
        /// </summary>
        /// <returns>A <see cref="ReadOnlyMemory{T}"/> containing all items added so far.</returns>
        public ReadOnlyMemory<T> Extract()
        {
            var array = _array;
            _array = null;

            return array.AsMemory(0, _count);
        }

        private void CheckExtracted()
        {
            if (_array is null)
            {
                throw new InvalidOperationException("Backing array was already extracted.");
            }
        }

        private void Resize()
        {
            CheckExtracted();

            var currentArray = _array;
            var currentCount = _count;

            Debug.Assert(currentArray is not null);
            Debug.Assert(currentCount == currentArray!.Length);

            const int initialCapacity = 4;
            const int growthFactor = 2;

            var currentCapacity = currentArray.Length;
            var newSize = currentCapacity == 0
                ? initialCapacity
                : checked(currentCapacity * growthFactor);

            var newArray = new T[newSize];

            _array.AsSpan(0, currentCount).CopyTo(newArray);
            _array = newArray;

            Debug.Assert(_array.Length > currentArray.Length);
        }
    }
}
