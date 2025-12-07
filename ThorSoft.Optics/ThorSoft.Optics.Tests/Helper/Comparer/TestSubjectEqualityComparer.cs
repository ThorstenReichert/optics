using System.Diagnostics.CodeAnalysis;
using ThorSoft.Optics.Tests.Helper.Subjects;

namespace ThorSoft.Optics.Tests.Helper.Comparer
{
    internal sealed class TestSubjectEqualityComparer<T> : IEqualityComparer<TestSubject<T>>
        where T : notnull
    {
        private readonly IEqualityComparer<T> _valueComparer;
        public TestSubjectEqualityComparer(IEqualityComparer<T> _comparer)
        {
            _valueComparer = _comparer;
        }

        public TestSubjectEqualityComparer() : this(EqualityComparer<T>.Default)
        {
        }

        public bool Equals(TestSubject<T>? x, TestSubject<T>? y)
        {
            if (x is null && y is null)
            {
                return true;
            }

            if (x is null || y is null)
            {
                return false;
            }

            return _valueComparer.Equals(x.Value, y.Value);
        }

        public int GetHashCode([DisallowNull] TestSubject<T> obj)
        {
            return _valueComparer.GetHashCode(obj.Value);
        }
    }
}
