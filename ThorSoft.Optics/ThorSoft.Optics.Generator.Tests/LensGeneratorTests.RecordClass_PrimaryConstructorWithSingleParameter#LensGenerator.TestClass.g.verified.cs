//HintName: LensGenerator.TestClass.g.cs
namespace Test.Module;

partial record class TestClass
{

    public static class Lenses
    {

        public static global::ThorSoft.Optics.Lens<TestClass, int> Property { get; } =
            new global::ThorSoft.Optics.Lens<TestClass, int>(
                static (instance) => instance.Property,
                static (value, instance) => instance with { Property = value });

    } // Lenses

} // TestClass
