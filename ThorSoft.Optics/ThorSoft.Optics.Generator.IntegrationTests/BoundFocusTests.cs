namespace ThorSoft.Optics.Generator.IntegrationTests
{
    public sealed class BoundFocusTests
    {
        internal record class A(B PropB);
        internal record class B(C PropC);
        internal record class C(int PropInt);

        [Fact]
        public void Focus_TopLevelProperty_Get()
        {
            var instance = new C(4);

            Assert.Equal(instance.PropInt, instance.Focus(x => x.PropInt).Get());
        }

        [Fact]
        public void Focus_TopLevelProperty_Set()
        {
            var instance = new C(5);

            var result = instance.Focus(x => x.PropInt).Set(10);

            Assert.Equal(10, result.PropInt);
        }

        [Fact]
        public void Focus_NestedProperty_Get()
        {
            var instance = new A(new B(new C(5)));

            Assert.Equal(instance.PropB.PropC.PropInt, instance.Focus(x => x.PropB.PropC.PropInt).Get());
        }

        [Fact]
        public void Focus_NestedProperty_Set()
        {
            var instance = new A(new B(new C(5)));

            var result = instance.Focus(x => x.PropB.PropC.PropInt).Set(10);

            Assert.Equal(10, result.PropB.PropC.PropInt);
        }
    }
}
