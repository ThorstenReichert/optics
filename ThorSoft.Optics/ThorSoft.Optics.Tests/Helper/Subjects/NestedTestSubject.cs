using System.Numerics;

namespace ThorSoft.Optics.Tests.Helper.Subjects
{
    internal sealed record class NestedTestSubject<T>(TestSubject<T> Inner)
        : IEqualityOperators<NestedTestSubject<T>, NestedTestSubject<T>, bool>;
}
