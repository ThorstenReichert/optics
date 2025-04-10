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
    public sealed partial record class NestedRecord
    {
        public NestedRecord? Property { get; init; }
    }

    record class MyClass
    {
        public int Prop { get; set; }
    }

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
        public void DeepNesting()
        {
            var instance = new NestedRecord
            {
                Property = new NestedRecord
                {
                    Property = new NestedRecord
                    {
                        Property = new NestedRecord
                        {
                            Property = null,
                        }
                    }
                }
            };

            var property = NestedRecord.Lenses.Property;

            var nextToInnerLens = property.Compose(property).Compose(property);
            Assert.IsType<NestedRecord>(nextToInnerLens.Get(instance));
            Assert.Null(nextToInnerLens.Set(null, instance).Property!.Property!.Property);

            var innerLens = property.Compose(property).Compose(property).Compose(property);
            Assert.Null(innerLens.Get(instance));
            Assert.NotNull(innerLens.Set(new NestedRecord(), instance).Property!.Property!.Property);
        }
    }
}