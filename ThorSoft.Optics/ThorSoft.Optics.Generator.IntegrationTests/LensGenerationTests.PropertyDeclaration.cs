namespace ThorSoft.Optics.Generator.IntegrationTests
{
    [GenerateLenses]
    partial record class PublicProperty { public int Property { get; init; } }

    [GenerateLenses]
    partial record class PublicRequiredProperty { public required int Property { get; init; } }

    [GenerateLenses]
    partial record class InternalProperty { internal int Property { get; init; } }

    [GenerateLenses]
    partial record class InternalRequiredProperty { internal required int Property { get; init; } }

    public sealed partial class LensGenerationTests
    {
        [Fact]
        public void PropertyDeclaration_PublicProperty()
        {
            var instance = new PublicProperty { Property = 1 };

            Assert.Equal(instance.Property, PublicProperty.PropertyLens.Get(instance));
        }

        [Fact]
        public void PropertyDeclaration_PublicRequiredProperty()
        {
            var instance = new PublicRequiredProperty { Property = 1 };

            Assert.Equal(instance.Property, PublicRequiredProperty.PropertyLens.Get(instance));
        }

        [Fact]
        public void PropertyDeclaration_InternalProperty()
        {
            var instance = new InternalProperty { Property = 1 };

            Assert.Equal(instance.Property, InternalProperty.PropertyLens.Get(instance));
        }

        [Fact]
        public void PropertyDeclaration_InternalRequiredProperty()
        {
            var instance = new InternalRequiredProperty { Property = 1 };

            Assert.Equal(instance.Property, InternalRequiredProperty.PropertyLens.Get(instance));
        }
    }
}
