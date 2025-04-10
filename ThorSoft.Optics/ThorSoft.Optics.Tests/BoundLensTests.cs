namespace ThorSoft.Optics.Tests
{
    public sealed class BoundLensTests
    {
        [Fact]
        public void Lens_Get_PrimitiveType()
        {
            var instance = new A(5);
            var lens = new Lens<A, int>(x => x.Prop, (v, x) => x with { Prop = v });
            var bound = new BoundLens<A, int>(lens, instance);

            Assert.Equal(instance.Prop, bound.Get());
        }

        [Fact]
        public void Lens_Get_ReferenceType()
        {
            var instance = new B(new object());
            var lens = new Lens<B, object>(x => x.Prop, (v, x) => x with { Prop = v });
            var bound = new BoundLens<B, object>(lens, instance);

            Assert.Same(instance.Prop, bound.Get());
        }

        [Fact]
        public void Lens_Set_PrimitiveType()
        {
            const int value = -1;
            var instance = new A(5);
            var lens = new Lens<A, int>(x => x.Prop, (v, x) => x with { Prop = v });
            var bound = new BoundLens<A, int>(lens, instance);

            Assert.Equal(value, bound.Set(value).Prop);
        }

        [Fact]
        public void Lens_Set_ReferenceType()
        {
            var value = new object();
            var instance = new B(new object());
            var lens = new Lens<B, object>(x => x.Prop, (v, x) => x with { Prop = v });
            var bound = new BoundLens<B, object>(lens, instance);

            Assert.Same(value, bound.Set(value).Prop);
        }

        private sealed record class A(int Prop);
        private sealed record class B(object Prop);
    }
}
