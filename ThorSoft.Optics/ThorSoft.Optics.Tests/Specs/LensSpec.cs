using FsCheck;
using FsCheck.Fluent;
using System.Numerics;

namespace ThorSoft.Optics.Tests.Specs
{
    public static class LensSpec
    {
        public static Func<T, bool> GetSet<T, U>(Lens<T, U> lens, IEqualityComparer<T> comparer)
        {
            return i => comparer.Equals(
                lens.Set(lens.Get(i), i),
                i);
        }

        public static Func<T, U, bool> SetGet<T, U>(Lens<T, U> lens, IEqualityComparer<U> comparer)
        {
            return (i, v) => comparer.Equals(
                lens.Get(lens.Set(v, i)),
                v);
        }

        public static Func<T, U, bool> SetIdempotent<T, U>(Lens<T, U> lens, IEqualityComparer<T> comparer)
        {
            return (i, v) => comparer.Equals(
                lens.Set(v, lens.Set(v, i)),
                lens.Set(v, i));
        }

        public static Func<T, bool> ModifyIdentity<T, U>(Lens<T, U> lens, IEqualityComparer<T> comparer)
        {
            return i => comparer.Equals(
                lens.Modify(u => u, i),
                i);
        }

        public static Func<T, Func<U, U>, Func<U, U>, bool> ModifyComposition<T, U>(Lens<T, U> lens, IEqualityComparer<T> comparer)
        {
            return (i, f, g) => comparer.Equals(
                lens.Modify(f, lens.Modify(g, i)),
                lens.Modify(u => f(g(u)), i));
        }

        public static Func<T, U, bool> ConsistentReplaceModify<T, U>(Lens<T, U> lens, IEqualityComparer<T> comparer)
        {
            return (i, v) => comparer.Equals(
                lens.Set(v, i),
                lens.Modify(_ => v, i));
        }

        public static void IsSatisfied<TSubject, T>(
            this Lens<TSubject, T> lens,
            IEqualityComparer<TSubject>? subjectComparer = null,
            IEqualityComparer<T>? valueComparer = null)

            where T : IEqualityOperators<T, T, bool>
            where TSubject : IEqualityOperators<TSubject, TSubject, bool>
        {
            subjectComparer ??= EqualityComparer<TSubject>.Default;
            valueComparer ??= EqualityComparer<T>.Default;

            Prop.ForAll(GetSet(lens, subjectComparer)).QuickCheckThrowOnFailure();
            Prop.ForAll(SetGet(lens, valueComparer)).QuickCheckThrowOnFailure();
            Prop.ForAll(SetIdempotent(lens, subjectComparer)).QuickCheckThrowOnFailure();
            Prop.ForAll(ModifyIdentity(lens, subjectComparer)).QuickCheckThrowOnFailure();
            Prop.ForAll(ModifyComposition(lens, subjectComparer)).QuickCheckThrowOnFailure();
            Prop.ForAll(ConsistentReplaceModify(lens, subjectComparer)).QuickCheckThrowOnFailure();
        }
    }
}
