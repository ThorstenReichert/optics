using FsCheck;
using FsCheck.Fluent;

namespace ThorSoft.Optics.Tests
{
    public sealed class OpticsExtensionModifyTests
    {
        private static Lens<A, int> Lens => new(
            x => x.Prop,
            (v, x) => x with { Prop = v });

        private static Optional<A, int> OptionalPositive => new(
            x => Maybe.Some(x.Prop),
            (v, x) => x with { Prop = v });

        private static Optional<A, int> OptionalNegative => new(
            x => Maybe.None<int>(),
            (v, x) => x);

        private static Func<int, int> Times2 =>
            x => x * 2;

        [Fact]
        public void Modify_Lens_ModifiesFocusedProperty()
        {
            Prop
                .ForAll<int>(v =>
                {
                    var instance = new A { Prop = v };
                    var modified = Lens.Modify(Times2, instance);

                    return modified.Prop == Times2(v);
                })
                .QuickCheckThrowOnFailure();
        }

        [Fact]
        public void Modify_BoundLens_ModifiesFocusedProperty()
        {
            Prop
                .ForAll<int>(v =>
                {
                    var boundLens = new BoundLens<A, int>(Lens, new A { Prop = v });
                    var modified = boundLens.Modify(Times2);

                    return modified.Prop == Times2(v);
                })
                .QuickCheckThrowOnFailure();
        }

        [Fact]
        public void Modify_Optional_PropertyExists_ModifiesFocusedProperty()
        {
            Prop
                .ForAll<int>(v =>
                {
                    var instance = new A { Prop = v };
                    var modified = OptionalPositive.Modify(Times2, instance);

                    return modified.Prop == Times2(v);
                })
                .QuickCheckThrowOnFailure();
        }

        [Fact]
        public void Modify_Optional_PropertyDoesNotExist_ReturnsOriginal()
        {
            Prop
                .ForAll<int>(v =>
                {
                    var instance = new A { Prop = v };
                    var modified = OptionalNegative.Modify(Times2, instance);

                    return modified == instance;
                })
                .QuickCheckThrowOnFailure();
        }

        [Fact]
        public void Modify_BoundOptional_PropertyExists_ModifiesFocusedProperty()
        {
            Prop
                .ForAll<int>(v =>
                {
                    var boundOptional = new BoundOptional<A, int>(OptionalPositive, new A { Prop = v });
                    var modified = boundOptional.Modify(Times2);

                    return modified.Prop == Times2(v);
                })
                .QuickCheckThrowOnFailure();
        }

        [Fact]
        public void Modify_BoundOptional_PropertyDoesNotExist_ReturnsOriginal()
        {
            Prop
                .ForAll<int>(v =>
                {
                    var boundOptional = new BoundOptional<A, int>(OptionalNegative, new A { Prop = v });
                    var modified = boundOptional.Modify(Times2);

                    return modified == boundOptional.Instance;
                })
                .QuickCheckThrowOnFailure();
        }

        private sealed record class A
        {
            public int Prop { get; init; }
        }
    }
}
