//HintName: LensGenerator.TestClass.g.cs
namespace Test.Module;

partial record class TestClass
{

        public static global::ThorSoft.Optics.Lens<TestClass, int> PropertyLens { get; } =
            new global::ThorSoft.Optics.Lens<TestClass, int>(
                static (instance) => instance.Property,
                static (value, instance) => instance with { Property = value });

} // TestClass
