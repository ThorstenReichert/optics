using FsCheck;
using FsCheck.Fluent;

namespace ThorSoft.Optics.Tests
{
    public sealed class OpticsExtensionsReverseTests
    {
        private static Iso<A, int> Iso => new(
            x => x.Value,
            x => new A(x));

        [Fact]
        public void Reverse_DirectMapEqualsInverseMapOfOriginal()
        {
            Prop
                .ForAll<int>(x =>
                {
                    var original = Iso;
                    var reversed = original.Reverse();

                    return reversed.DirectMap(x) == original.InverseMap(x);
                })
                .QuickCheckThrowOnFailure();
        }

        [Fact]
        public void Reverse_InverseMapEqualsDirectMapOfOriginal()
        {
            Prop
                .ForAll<int>(v =>
                {
                    var x = new A(v);
                    var original = Iso;
                    var reversed = original.Reverse();

                    return reversed.InverseMap(x) == original.DirectMap(x);
                })
                .QuickCheckThrowOnFailure();
        }

        private sealed record class A(int Value);
    }
}
