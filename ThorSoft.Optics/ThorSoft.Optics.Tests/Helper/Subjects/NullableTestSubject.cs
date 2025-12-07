using System.Numerics;

namespace ThorSoft.Optics.Tests.Helper.Subjects
{
    internal sealed record class NullableTestSubject<T>(T? Value)
        : IEqualityOperators<NullableTestSubject<T>, NullableTestSubject<T>, bool>
        where T : notnull;
}
