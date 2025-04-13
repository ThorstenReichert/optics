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
    }
}