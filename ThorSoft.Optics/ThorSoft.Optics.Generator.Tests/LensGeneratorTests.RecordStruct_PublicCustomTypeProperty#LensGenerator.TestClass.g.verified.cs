//HintName: LensGenerator.TestClass.g.cs
namespace Test.Module;

partial record struct TestClass
{

        public static global::ThorSoft.Optics.Lens<TestClass, Test.OtherModule.CustomType> TestPropertyLens =>
            new global::ThorSoft.Optics.Lens<TestClass, Test.OtherModule.CustomType>(
                static (instance) => instance.TestProperty,
                static (value, instance) => instance with { TestProperty = value });

} // TestClass
