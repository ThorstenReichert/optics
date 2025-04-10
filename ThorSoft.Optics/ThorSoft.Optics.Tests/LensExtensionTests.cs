namespace ThorSoft.Optics.Tests
{
    public sealed class LensExtensionTests
    {
        private static Lens<A, B> LensAB() => new Lens<A, B>(x => x.PropB, (v, x) => x with { PropB = v });
        private static Lens<B, int> LensBI() => new Lens<B, int>(x => x.PropInt, (v, x) => x with { PropInt = v });
        private static BoundLens<A, B> BoundAB(A instance) => new BoundLens<A, B>(LensAB(), instance);
        private static BoundLens<B, int> BoundBI(B instance) => new BoundLens<B, int>(LensBI(), instance);

        [Fact]
        public void Compose_LenseWithLens_Get()
        {
            var instance = new A(new B(5));

            Assert.Equal(instance.PropB.PropInt, LensAB().Compose(LensBI()).Get(instance));
        }

        [Fact]
        public void Compose_LensWithLens_Set()
        {
            const int value = -1;
            var instance = new A(new B(5));

            Assert.Equal(value, LensAB().Compose(LensBI()).Set(value, instance).PropB.PropInt);
        }

        [Fact]
        public void Compose_BoundLensWithLens_Get()
        {
            var instance = new A(new B(5));

            Assert.Equal(instance.PropB.PropInt, BoundAB(instance).Compose(LensBI()).Get());
        }

        [Fact]
        public void Compose_BoundLensWithLens_Set()
        {
            const int value = -1;
            var instance = new A(new B(5));

            Assert.Equal(value, BoundAB(instance).Compose(LensBI()).Set(value).PropB.PropInt);
        }

        [Fact]
        public void Compose_BoundLensWithBoundLens_Get()
        {
            var instance = new A(new B(5));

            Assert.Equal(instance.PropB.PropInt, BoundAB(instance).Compose(BoundBI(instance.PropB)).Get());
        }

        [Fact]
        public void Compose_BoundLensWithBoundLens_Set()
        {
            const int value = -1;
            var instance = new A(new B(5));

            Assert.Equal(value, BoundAB(instance).Compose(BoundBI(instance.PropB)).Set(value).PropB.PropInt);
        }

        [Fact]
        public void Compose_LensWithBoundLens_Get()
        {
            var instance = new A(new B(5));

            Assert.Equal(instance.PropB.PropInt, LensAB().Compose(BoundBI(instance.PropB)).Get(instance));
        }

        [Fact]
        public void Compose_LensWithBoundLens_Set()
        {
            const int value = -1;
            var instance = new A(new B(5));

            Assert.Equal(value, LensAB().Compose(BoundBI(instance.PropB)).Set(value, instance).PropB.PropInt);
        }

        [Fact]
        public void Modify_Lens()
        {
            var instance = new B(10);

            Assert.Equal(Increment(instance.PropInt), LensBI().Modify(Increment, instance).PropInt);
        }

        [Fact]
        public void Modify_BoundLens()
        {
            var instance = new B(10);

            Assert.Equal(Increment(instance.PropInt), BoundBI(instance).Modify(Increment).PropInt);
        }

        private static int Increment(int value) => value + 1;

        private sealed record class A(B PropB);
        private sealed record class B(int PropInt);
    }
}
