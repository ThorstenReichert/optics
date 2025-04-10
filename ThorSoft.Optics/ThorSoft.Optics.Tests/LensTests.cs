namespace ThorSoft.Optics.Tests
{
    public class LensTests
    {
        [Fact]
        public void Lens_Get_PrimitiveType()
        {
            var instance = new A(5);
            var lens = new Lens<A, int>(x => x.Prop, (v, x) => x with { Prop = v });

            Assert.Equal(instance.Prop, lens.Get(instance));
        }

        [Fact]
        public void Lens_Get_ReferenceType()
        {
            var instance = new B(new object());
            var lens = new Lens<B, object>(x => x.Prop, (v, x) => x with { Prop = v });

            Assert.Same(instance.Prop, lens.Get(instance));
        }

        [Fact]
        public void Lens_Set_PrimitiveType()
        {
            const int value = -1;
            var instance = new A(5);
            var lens = new Lens<A, int>(x => x.Prop, (v, x) => x with { Prop = v });

            Assert.Equal(value, lens.Set(value, instance).Prop);
        }

        [Fact]
        public void Lens_Set_ReferenceType()
        {
            var value = new object();
            var instance = new B(new object());
            var lens = new Lens<B, object>(x => x.Prop, (v, x) => x with { Prop = v });

            Assert.Same(value, lens.Set(value, instance).Prop);
        }

        private sealed record class A(int Prop);
        private sealed record class B(object Prop);
    }
}