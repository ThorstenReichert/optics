using System.Numerics;

namespace ThorSoft.Optics.Tests.Helper.Subjects
{
    internal sealed record class TestSubject<T>(T Value)
        : IEqualityOperators<TestSubject<T>, TestSubject<T>, bool>;
}
