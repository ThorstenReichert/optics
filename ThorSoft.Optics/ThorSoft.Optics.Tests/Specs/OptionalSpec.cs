using FsCheck;
using FsCheck.Fluent;

namespace ThorSoft.Optics.Tests.Specs
{
    internal static class OptionalSpec
    {
        public static Func<T, U, bool> SetGet<T, U>(Optional<T, U> optional, IEqualityComparer<U> comparer)
        {
            return (i, v) => Equals(
                optional.Get(optional.Set(v, i)),
                optional.Get(i).Map(_ => v),
                comparer);
        }

        public static Func<T, U, bool> SetIdempotent<T, U>(Optional<T, U> optional, IEqualityComparer<T> comparer)
        {
            return (i, v) => comparer.Equals(
                optional.Set(v, optional.Set(v, i)),
                optional.Set(v, i));
        }

        public static Func<T, bool> ModifyIdentity<T, U>(Optional<T, U> optional, IEqualityComparer<T> comparer)
        {
            return i => comparer.Equals(
                optional.Modify(u => u, i),
                i);
        }

        public static Func<T, Func<U, U>, Func<U, U>, bool> ModifyComposition<T, U>(Optional<T, U> optional, IEqualityComparer<T> comparer)
        {
            return (i, f, g) => comparer.Equals(
                optional.Modify(f, optional.Modify(g, i)),
                optional.Modify(u => f(g(u)), i));
        }

        public static Func<T, U, bool> ConsistentSetModify<T, U>(Optional<T, U> optional, IEqualityComparer<T> comparer)
        {
            return (i, v) => comparer.Equals(
                optional.Set(v, i),
                optional.Modify(_ => v, i));
        }

        public static void IsSatisfied<T, U>(
            Optional<T, U> optional,
            IEqualityComparer<T>? subjectComparer = null,
            IEqualityComparer<U>? valueComparer = null)
        {
            subjectComparer ??= EqualityComparer<T>.Default;
            valueComparer ??= EqualityComparer<U>.Default;

            Prop.ForAll(SetGet(optional, valueComparer)).QuickCheckThrowOnFailure();
            Prop.ForAll(SetIdempotent(optional, subjectComparer)).QuickCheckThrowOnFailure();
            Prop.ForAll(ModifyIdentity(optional, subjectComparer)).QuickCheckThrowOnFailure();
            Prop.ForAll(ModifyComposition(optional, subjectComparer)).QuickCheckThrowOnFailure();
            Prop.ForAll(ConsistentSetModify(optional, subjectComparer)).QuickCheckThrowOnFailure();
        }

        private static bool Equals<T>(Maybe<T> left, Maybe<T> right, IEqualityComparer<T> comparer)
        {
            var leftHasValue = left.TryGetValue(out var leftValue);
            var rightHasValue = right.TryGetValue(out var rightValue);

            if (!leftHasValue && !rightHasValue)
            {
                return true;
            }
            else if (leftHasValue && rightHasValue)
            {
                return comparer.Equals(leftValue, rightValue);
            }
            else
            {
                return false;
            }
        }
    }
}
