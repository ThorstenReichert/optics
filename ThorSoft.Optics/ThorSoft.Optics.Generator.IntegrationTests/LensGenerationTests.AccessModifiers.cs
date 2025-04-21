namespace ThorSoft.Optics.Generator.IntegrationTests
{
    [GenerateLenses]
    public sealed partial record class AccessModifierPublic
    {
        public int Property { get; init; }
    }

    [GenerateLenses]
    internal sealed partial record class AccessModifierInternal
    {
        internal int Property { get; init; }
    }

    [GenerateLenses]
    public partial record class AccessModifierProtected
    {
        protected int Property { get; init; }
    }

    public record class AccessModifierProtectedTest : AccessModifierProtected
    {
        public static void Test()
        {
            var instance = new AccessModifierProtectedTest { Property = 1 };

            Assert.Equal(5, PropertyLens.Get(PropertyLens.Set(5, instance)));
        }
    }

    [GenerateLenses]
    public partial record class AccessModifierPrivateProtected
    {
        private protected int Property { get; init; }
    }

    public record class AccessModifierPrivateProtectedTest : AccessModifierPrivateProtected
    {
        public static void Test()
        {
            var instance = new AccessModifierPrivateProtectedTest { Property = 1 };

            Assert.Equal(5, PropertyLens.Get(PropertyLens.Set(5, instance)));
        }
    }

    [GenerateLenses]
    public partial record class AccessModifierPrivate
    {
        private int Property { get; init; }

        public static void Test()
        {
            var instance = new AccessModifierPrivate { Property = 1 };

            Assert.Equal(5, PropertyLens.Get(PropertyLens.Set(5, instance)));
        }
    }

    public sealed partial class LensGenerationTests
    {

        [Fact]
        public void AccessModifier_Public()
        {
            var instance = new AccessModifierPublic { Property = 1 };

            Assert.Equal(5, AccessModifierPublic.PropertyLens.Get(AccessModifierPublic.PropertyLens.Set(5, instance)));
        }

        [Fact]
        public void AccessModifier_Internal()
        {
            var instance = new AccessModifierInternal { Property = 1 };

            Assert.Equal(5, AccessModifierInternal.PropertyLens.Get(AccessModifierInternal.PropertyLens.Set(5, instance)));
        }

        [Fact]
        public void AccessModifier_Protected()
        {
            AccessModifierProtectedTest.Test();
        }

        [Fact]
        public void AccessModifier_PrivateProtected()
        {
            AccessModifierPrivateProtectedTest.Test();
        }

        [Fact]
        public void AccessModifier_Private()
        {
            AccessModifierPrivate.Test();
        }
    }
}
