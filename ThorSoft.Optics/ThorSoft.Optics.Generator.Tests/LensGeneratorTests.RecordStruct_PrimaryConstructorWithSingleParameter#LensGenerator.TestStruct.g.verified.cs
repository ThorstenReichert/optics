//HintName: LensGenerator.TestStruct.g.cs
namespace Test.Module;

partial record struct TestStruct
{

    public static class Lenses
    {

        public static global::ThorSoft.Optics.Lens<TestStruct, int> Property { get; } =
            new global::ThorSoft.Optics.Lens<TestStruct, int>(
                static (instance) => instance.Property,
                static (value, instance) => instance with { Property = value });

    } // Lenses

} // TestStruct
