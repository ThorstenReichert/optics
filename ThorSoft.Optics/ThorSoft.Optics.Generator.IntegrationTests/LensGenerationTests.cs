namespace ThorSoft.Optics.Generator.IntegrationTests
{
    public readonly struct OtherRecord;

    [GenerateLenses]
    public sealed partial record class MyRecordClass
    {
        public required int RequiredIntProperty { get; init; }

        public int IntProperty { get; init; }

        public OtherRecord RecordProperty { get; init; }

        public int ReadonlyIntProperty { get; }
    }

    [GenerateLenses]
    public sealed partial record class MyOtherRecordClass
    {
        public required MyRecordClass MyRecordClassProperty { get; init; }
    }

    [GenerateLenses]
    public sealed partial record class A { public required B PropB { get; init; } }

    [GenerateLenses]
    public sealed partial record class B { public required C PropC {get; init;} }

    [GenerateLenses]
    public sealed partial record class C { public required D PropD {get; init;} }

    [GenerateLenses]
    public sealed partial record class D { public required string PropString {get; init;} }

    [GenerateLenses]
    public sealed partial record class PrimaryCtor(int PropertyInt, string PropertyString);

    [GenerateLenses]
    public readonly partial record struct PrimaryCtorStruct(int PropertyInt, string PropertyString);

    public class LensGenerationTests
    {
        [Fact]
        public void RecordClass_RequiredIntProperty_Getter()
        {
            var instance = new MyRecordClass { RequiredIntProperty = 1 };

            Assert.Equal(instance.RequiredIntProperty, MyRecordClass.Lenses.RequiredIntProperty.Get(instance));
        }

        [Fact]
        public void RecordClass_RequiredIntProperty_Setter()
        {
            var instance = new MyRecordClass { RequiredIntProperty = 1 };

            var newInstance = MyRecordClass.Lenses.RequiredIntProperty.Set(5, instance);

            Assert.Equal(5, newInstance.RequiredIntProperty);
        }

        [Fact]
        public void RecordClass_IntProperty_Getter()
        {
            var instance = new MyRecordClass { IntProperty = 5, RequiredIntProperty = 1 };

            Assert.Equal(instance.IntProperty, MyRecordClass.Lenses.IntProperty.Get(instance));
        }

        [Fact]
        public void RecordClass_IntProperty_Setter()
        {
            var instance = new MyRecordClass { IntProperty = 1, RequiredIntProperty = -1 };

            var newInstance = MyRecordClass.Lenses.IntProperty.Set(5, instance);

            Assert.Equal(5, newInstance.IntProperty);
        }

        [Fact]
        public void RecordClass_Set_KeepsOtherPropertyValues()
        {
            const int newValue = int.MaxValue; 
            var instance = new MyRecordClass { IntProperty = 1, RequiredIntProperty = -5 };

            var newInstance = MyRecordClass.Lenses.IntProperty.Set(newValue, instance);

            Assert.Equal(instance with { IntProperty = newValue }, newInstance);
        }

        [Fact]
        public void DeepNesting()
        {
            var instance = new A { PropB = new B { PropC = new C { PropD = new D { PropString = "originalValue" } } } };

            var originalValue = A.Lenses.PropB
                .Compose(B.Lenses.PropC)
                .Compose(C.Lenses.PropD)
                .Compose(D.Lenses.PropString);

            Assert.Equal(instance.PropB.PropC.PropD.PropString, originalValue.Get(instance));
            Assert.Equal("new value", originalValue.Set("new value", instance).PropB.PropC.PropD.PropString);
        }

        [Fact]
        public void PrimaryConstructor_IntProperty_Get()
        {
            var instance = new PrimaryCtor(1, "some value");

            Assert.Equal(instance.PropertyInt, PrimaryCtor.Lenses.PropertyInt.Get(instance));
        }

        [Fact]
        public void PrimaryConstructor_IntProperty_Set()
        {
            var instance = new PrimaryCtor(1, "some value");

            var newInstance = PrimaryCtor.Lenses.PropertyInt.Set(-3, instance);

            Assert.Equal(-3, newInstance.PropertyInt);
            Assert.Equal(instance.PropertyString, newInstance.PropertyString);
        }

        [Fact]
        public void PrimaryConstructor_StringProperty_Get()
        {
            var instance = new PrimaryCtor(1, "some value");

            Assert.Equal(instance.PropertyString, PrimaryCtor.Lenses.PropertyString.Get(instance));
        }

        [Fact]
        public void PrimaryConstructor_StringProperty_Set()
        {
            var instance = new PrimaryCtor(1, "some value");

            var newInstance = PrimaryCtor.Lenses.PropertyString.Set("new value", instance);

            Assert.Equal("new value", newInstance.PropertyString);
            Assert.Equal(instance.PropertyInt, newInstance.PropertyInt);
        }

        [Fact]
        public void PrimaryConstructorStruct_IntProperty_Get()
        {
            var instance = new PrimaryCtorStruct(1, "some value");

            Assert.Equal(instance.PropertyInt, PrimaryCtorStruct.Lenses.PropertyInt.Get(instance));
        }

        [Fact]
        public void PrimaryConstructorStruct_IntProperty_Set()
        {
            var instance = new PrimaryCtorStruct(1, "some value");

            var newInstance = PrimaryCtorStruct.Lenses.PropertyInt.Set(-3, instance);

            Assert.Equal(-3, newInstance.PropertyInt);
            Assert.Equal(instance.PropertyString, newInstance.PropertyString);
        }

        [Fact]
        public void PrimaryConstructorStruct_StringProperty_Get()
        {
            var instance = new PrimaryCtorStruct(1, "some value");

            Assert.Equal(instance.PropertyString, PrimaryCtorStruct.Lenses.PropertyString.Get(instance));
        }

        [Fact]
        public void PrimaryConstructorStruct_StringProperty_Set()
        {
            var instance = new PrimaryCtorStruct(1, "some value");

            var newInstance = PrimaryCtorStruct.Lenses.PropertyString.Set("new value", instance);

            Assert.Equal("new value", newInstance.PropertyString);
            Assert.Equal(instance.PropertyInt, newInstance.PropertyInt);
        }
    }
}