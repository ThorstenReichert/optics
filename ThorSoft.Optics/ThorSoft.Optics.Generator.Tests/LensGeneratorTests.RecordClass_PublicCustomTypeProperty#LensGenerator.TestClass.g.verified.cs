//HintName: LensGenerator.TestClass.g.cs
namespace Test.Module;

partial record class TestClass
{

    public static class Lenses
    {

        public static global::ThorSoft.Optics.Lens<TestClass, Test.OtherModule.CustomType> TestProperty { get; } =
            new global::ThorSoft.Optics.Lens<TestClass, Test.OtherModule.CustomType>(
                static (instance) => instance.TestProperty,
                static (value, instance) => instance with { TestProperty = value });

    } // Lenses

} // TestClass
