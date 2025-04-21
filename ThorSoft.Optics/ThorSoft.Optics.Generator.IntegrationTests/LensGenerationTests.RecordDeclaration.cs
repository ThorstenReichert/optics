namespace ThorSoft.Optics.Generator.IntegrationTests
{
    [GenerateLenses]
    partial record class RecordClass { public int Property { get; init; } }

    [GenerateLenses]
    sealed partial record class SealedRecordClass { public int Property { get; init; } }

    [GenerateLenses]
    abstract partial record class AbstractRecordClass { public int Property { get; init; } }

    record class AbstractRecordClassDerived : AbstractRecordClass;

    [GenerateLenses]
    public partial record class PublicRecordClass { public int Property { get; init; } }

    [GenerateLenses]
    public sealed partial record class PublicSealedRecordClass { public int Property { get; init; } }

    [GenerateLenses]
    public abstract partial record class PublicAbstractRecordClass { public int Property { get; init; } }

    public record class PublicAbstractRecordClassDerived : PublicAbstractRecordClass;

    [GenerateLenses]
    internal partial record class InternalRecordClass { public int Property { get; init; } }

    [GenerateLenses]
    internal sealed partial record class InternalSealedRecordClass { public int Property { get; init; } }

    [GenerateLenses]
    internal abstract partial record class InternalAbstractRecordClass { public int Property { get; init; } }

    internal record class InternalAbstractRecordClassDerived : InternalAbstractRecordClass;

    [GenerateLenses]
    partial record struct RecordStruct { public int Property { get; init; } }

    [GenerateLenses]
    readonly partial record struct ReadonlyRecordStruct { public int Property { get; init; } }

    [GenerateLenses]
    public partial record struct PublicRecordStruct { public int Property { get; init; } }

    [GenerateLenses]
    public readonly partial record struct PublicReadonlyRecordStruct { public int Property { get; init; } }

    [GenerateLenses]
    internal partial record struct InternalRecordStruct { public int Property { get; init; } }

    [GenerateLenses]
    internal readonly partial record struct InternalReadonlyRecordStruct { public int Property { get; init; } }

    public sealed partial class LensGenerationTests
    {
        [Fact]
        public void RecordDeclaration_RecordClass()
        {
            var instance = new RecordClass { Property = 1 };

            Assert.Equal(instance.Property, RecordClass.PropertyLens.Get(instance));
        }

        [Fact]
        public void RecordDeclaration_SealedRecordClass()
        {
            var instance = new SealedRecordClass { Property = 1 };

            Assert.Equal(instance.Property, SealedRecordClass.PropertyLens.Get(instance));
        }

        [Fact]
        public void RecordDeclaration_AbstractRecordClass()
        {
            var instance = new AbstractRecordClassDerived { Property = 1 };

            Assert.Equal(instance.Property, AbstractRecordClassDerived.PropertyLens.Get(instance));
        }

        [Fact]
        public void RecordDeclaration_PublicRecordClass()
        {
            var instance = new PublicRecordClass { Property = 1 };

            Assert.Equal(instance.Property, PublicRecordClass.PropertyLens.Get(instance));
        }

        [Fact]
        public void RecordDeclaration_PublicSealedRecordClass()
        {
            var instance = new PublicSealedRecordClass { Property = 1 };

            Assert.Equal(instance.Property, PublicSealedRecordClass.PropertyLens.Get(instance));
        }

        [Fact]
        public void RecordDeclaration_PublicAbstractRecordClass()
        {
            var instance = new PublicAbstractRecordClassDerived { Property = 1 };

            Assert.Equal(instance.Property, PublicAbstractRecordClassDerived.PropertyLens.Get(instance));
        }

        [Fact]
        public void RecordDeclaration_InternalRecordClass()
        {
            var instance = new InternalRecordClass { Property = 1 };

            Assert.Equal(instance.Property, InternalRecordClass.PropertyLens.Get(instance));
        }

        [Fact]
        public void RecordDeclaration_InternalSealedRecordClass()
        {
            var instance = new InternalSealedRecordClass { Property = 1 };

            Assert.Equal(instance.Property, InternalSealedRecordClass.PropertyLens.Get(instance));
        }

        [Fact]
        public void RecordDeclaration_InternalAbstractRecordClass()
        {
            var instance = new InternalAbstractRecordClassDerived { Property = 1 };

            Assert.Equal(instance.Property, InternalAbstractRecordClassDerived.PropertyLens.Get(instance));
        }

        [Fact]
        public void RecordDeclaration_RecordStruct()
        {
            var instance = new RecordStruct { Property = 1 };

            Assert.Equal(instance.Property, RecordStruct.PropertyLens.Get(instance));
        }

        [Fact]
        public void RecordDeclaration_ReadonlyRecordStruct()
        {
            var instance = new ReadonlyRecordStruct { Property = 1 };

            Assert.Equal(instance.Property, ReadonlyRecordStruct.PropertyLens.Get(instance));
        }

        [Fact]
        public void RecordDeclaration_PublicRecordStruct()
        {
            var instance = new PublicRecordStruct { Property = 1 };

            Assert.Equal(instance.Property, PublicRecordStruct.PropertyLens.Get(instance));
        }

        [Fact]
        public void RecordDeclaration_PublicReadonlyRecordStruct()
        {
            var instance = new PublicReadonlyRecordStruct { Property = 1 };

            Assert.Equal(instance.Property, PublicReadonlyRecordStruct.PropertyLens.Get(instance));
        }

        [Fact]
        public void RecordDeclaration_InternalRecordStruct()
        {
            var instance = new InternalRecordStruct { Property = 1 };

            Assert.Equal(instance.Property, InternalRecordStruct.PropertyLens.Get(instance));
        }

        [Fact]
        public void RecordDeclaration_InternalReadonlyRecordStruct()
        {
            var instance = new InternalReadonlyRecordStruct { Property = 1 };

            Assert.Equal(instance.Property, InternalReadonlyRecordStruct.PropertyLens.Get(instance));
        }
    }
}
