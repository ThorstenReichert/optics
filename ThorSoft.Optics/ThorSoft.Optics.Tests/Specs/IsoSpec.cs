using FsCheck;
using FsCheck.Fluent;
using System.Numerics;

namespace ThorSoft.Optics.Tests.Specs
{
    public static class IsoSpec
    {
        public static Func<T, bool> DirectRoundtrip<T, U>(Iso<T, U> iso, IEqualityComparer<T> comparer)
        {
            return i => comparer.Equals(
                iso.InverseMap(iso.DirectMap(i)),
                i);
        }

        public static Func<U, bool> InverseRoundtrip<T, U>(Iso<T, U> iso, IEqualityComparer<U> comparer)
        {
            return u => comparer.Equals(
                iso.DirectMap(iso.InverseMap(u)),
                u);
        }

        public static void IsSatisfied<T, U>(
            this Iso<T, U> iso,
            IEqualityComparer<T>? subjectComparer = null,
            IEqualityComparer<U>? valueComparer = null)

            where T : IEqualityOperators<T, T, bool>
            where U : IEqualityOperators<U, U, bool>
        {
            subjectComparer ??= EqualityComparer<T>.Default;
            valueComparer ??= EqualityComparer<U>.Default;

            Prop.ForAll(DirectRoundtrip(iso, subjectComparer)).QuickCheckThrowOnFailure();
            Prop.ForAll(InverseRoundtrip(iso, valueComparer)).QuickCheckThrowOnFailure();
        }
    }
}
