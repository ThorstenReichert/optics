//HintName: LensGenerator.TestClass.g.cs
namespace Test.Module;

partial record struct TestClass
{

    public static class Lenses
    {

        public static global::ThorSoft.Optics.Lens<TestClass, int> TestProperty { get; } =
            new global::ThorSoft.Optics.Lens<TestClass, int>(
                static (instance) => instance.TestProperty,
                static (value, instance) => instance with { TestProperty = value });

    } // Lenses

} // TestClass
