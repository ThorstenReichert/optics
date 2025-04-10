namespace ThorSoft.Optics.Generator.IntegrationTests
{
    public sealed class StaticFocusTests
    {
        internal record class A(B PropB);
        internal record class B(C PropC);
        internal record class C(int PropInt);

        [Fact]
        public void Focus_TopLevelProperty_Get()
        {
            var instance = new C(4);

            Assert.Equal(instance.PropInt, Lens<C>.Focus(x => x.PropInt).Get(instance));
        }

        [Fact]
        public void Focus_TopLevelProperty_Set()
        {
            var instance = new C(5);

            var result = Lens<C>.Focus(x => x.PropInt).Set(10, instance);

            Assert.Equal(10, result.PropInt);
        }

        [Fact]
        public void Focus_NestedProperty_Get()
        {
            var instance = new A(new B(new C(5)));

            Assert.Equal(instance.PropB.PropC.PropInt, Lens<A>.Focus(x => x.PropB.PropC.PropInt).Get(instance));
        }

        [Fact]
        public void Focus_NestedProperty_Set()
        {
            var instance = new A(new B(new C(5)));

            var result = Lens<A>.Focus(x => x.PropB.PropC.PropInt).Set(10, instance);

            Assert.Equal(10, result.PropB.PropC.PropInt);
        }
    }
}
