using System.Collections.Immutable;
using ThorSoft.Optics.Tests.Helper.Comparer;
using ThorSoft.Optics.Tests.Helper.Subjects;
using ThorSoft.Optics.Tests.Specs;

namespace ThorSoft.Optics.Tests
{
    public sealed class OpticsExtensionComposeTests
    {
        [Fact]
        public void Compose_Iso_Iso_SatisfiesIsoSpec()
        {
            var iso1 = new Iso<TestSubject<int>, int>(
                x => x.Value,
                x => new TestSubject<int>(x));
            var iso2 = new Iso<int, int>(
                x => x + 1,
                x => x - 1);

            IsoSpec.IsSatisfied(iso1.Compose(iso2));
        }

        [Fact]
        public void Compose_Iso_Lens_SatisfiesLensSpec()
        {
            var iso = new Iso<NestedTestSubject<int>, TestSubject<int>>(
                x => x.Inner,
                x => new NestedTestSubject<int>(x));
            var lens = new Lens<TestSubject<int>, int>(
                i => i.Value,
                (v, i) => i with { Value = v });

            LensSpec.IsSatisfied(iso.Compose(lens));
        }

        [Fact]
        public void Compose_Iso_Optional_SatisfiesOptionalSpec()
        {
            var comparer = new SequenceEqualityComparer<int>();
            var iso = new Iso<ImmutableList<int>, ImmutableList<int>>(
                x => x.Reverse(),
                x => x.Reverse());
            var optional = new Optional<ImmutableList<int>, int>(
                i => i is [var x, ..] ? Maybe.Some(x) : Maybe.None<int>(),
                (v, i) => i.Count > 0 ? i.SetItem(0, v) : i);

            OptionalSpec.IsSatisfied(iso.Compose(optional), comparer);
        }

        [Fact]
        public void Compose_Lens_Iso_SatisfiesLensSpec()
        {
            var lens = new Lens<TestSubject<int>, int>(
                i => i.Value,
                (v, x) => x with { Value = v });
            var iso = new Iso<int, int>(
                x => x + 1,
                x => x - 1);

            LensSpec.IsSatisfied(lens.Compose(iso));
        }

        [Fact]
        public void Compose_Lens_Lens_SatisfiesLensSpec()
        {
            var lens1 = new Lens<NestedTestSubject<int>, TestSubject<int>>(
                i => i.Inner,
                (v, i) => i with { Inner = v });
            var lens2 = new Lens<TestSubject<int>, int>(
                i => i.Value,
                (v, i) => i with { Value = v });

            LensSpec.IsSatisfied(lens1.Compose(lens2));
        }

        [Fact]
        public void Compose_Lens_Optional_SatisfiesOptionalSpec()
        {
            var comparer = new TestSubjectEqualityComparer<ImmutableList<int>>(
                new SequenceEqualityComparer<int>());
            var lens = new Lens<TestSubject<ImmutableList<int>>, ImmutableList<int>>(
                i => i.Value,
                (v, i) => i with { Value = v });
            var optional = new Optional<ImmutableList<int>, int>(
                i => i is [var x, ..] ? Maybe.Some(x) : Maybe.None<int>(),
                (v, i) => i.Count > 0 ? i.SetItem(0, v) : i);

            OptionalSpec.IsSatisfied(lens.Compose(optional), comparer);
        }

        [Fact]
        public void Compose_Optional_Iso_SatisfiesOptionalSpec()
        {
            var comparer = new SequenceEqualityComparer<int>();
            var optional = new Optional<ImmutableList<int>, int>(
                i => i is [var x, ..] ? Maybe.Some(x) : Maybe.None<int>(),
                (v, i) => i.Count > 0 ? i.SetItem(0, v) : i);
            var iso = new Iso<int, int>(
                x => x + 1,
                x => x - 1);

            OptionalSpec.IsSatisfied(optional.Compose(iso), comparer);
        }

        [Fact]
        public void Compose_Optional_Lens_SatisfiesOptionalSpec()
        {
            var comparer = new SequenceEqualityComparer<TestSubject<int>>();
            var optional = new Optional<ImmutableList<TestSubject<int>>, TestSubject<int>>(
                i => i is [var x, ..] ? Maybe.Some(x) : Maybe.None<TestSubject<int>>(),
                (v, i) => i.Count > 0 ? i.SetItem(0, v) : i);
            var lens = new Lens<TestSubject<int>, int>(
                i => i.Value,
                (v, i) => i with { Value = v });

            OptionalSpec.IsSatisfied(optional.Compose(lens), comparer);
        }

        [Fact]
        public void Compose_Optional_Optional_SatisfiesOptionalSpec()
        {
            var comparer = new SequenceEqualityComparer<ImmutableList<int>>(
                new SequenceEqualityComparer<int>());
            var optional1 = new Optional<ImmutableList<ImmutableList<int>>, ImmutableList<int>>(
                i => i is [var x, ..] ? Maybe.Some(x) : Maybe.None<ImmutableList<int>>(),
                (v, i) => i.Count > 0 ? i.SetItem(0, v) : i);
            var optional2 = new Optional<ImmutableList<int>, int>(
                i => i is [var x, ..] ? Maybe.Some(x) : Maybe.None<int>(),
                (v, i) => i.Count > 0 ? i.SetItem(0, v) : i);

            OptionalSpec.IsSatisfied(optional1.Compose(optional2), comparer);
        }
    }
}
