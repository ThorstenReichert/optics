using System.Diagnostics.CodeAnalysis;

namespace ThorSoft.Optics.Tests.Helper.Comparer
{
    internal sealed class SequenceEqualityComparer<T> : IEqualityComparer<IReadOnlyCollection<T>>
        where T : notnull
    {
        private readonly IEqualityComparer<T> _itemComparer;

        public SequenceEqualityComparer(IEqualityComparer<T> itemComparer)
        {
            _itemComparer = itemComparer;
        }

        public SequenceEqualityComparer() : this(EqualityComparer<T>.Default)
        {
        }

        public bool Equals(IReadOnlyCollection<T>? x, IReadOnlyCollection<T>? y)
        {
            if (x is null && y is null)
            {
                return true;
            }

            if (x is null || y is null)
            {
                return false;
            }

            if (x.Count != y.Count)
            {
                return false;
            }

            return x
                .Zip(y)
                .Select(pair => _itemComparer.Equals(pair.First, pair.Second))
                .All(static x => x);
        }

        public int GetHashCode([DisallowNull] IReadOnlyCollection<T> obj)
        {
            var hash = new HashCode();

            hash.Add(obj?.Count);

            foreach (var item in obj ?? [])
            {
                hash.Add(_itemComparer.GetHashCode(item));
            }

            return hash.ToHashCode();
        }
    }
}
