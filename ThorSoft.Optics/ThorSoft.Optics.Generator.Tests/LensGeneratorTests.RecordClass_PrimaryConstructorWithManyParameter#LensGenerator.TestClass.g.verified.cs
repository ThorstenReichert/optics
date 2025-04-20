//HintName: LensGenerator.TestClass.g.cs
namespace Test.Module;

partial record class TestClass
{

        public static global::ThorSoft.Optics.Lens<TestClass, int> Property1Lens { get; } =
            new global::ThorSoft.Optics.Lens<TestClass, int>(
                static (instance) => instance.Property1,
                static (value, instance) => instance with { Property1 = value });

        public static global::ThorSoft.Optics.Lens<TestClass, string> Property2Lens { get; } =
            new global::ThorSoft.Optics.Lens<TestClass, string>(
                static (instance) => instance.Property2,
                static (value, instance) => instance with { Property2 = value });

        public static global::ThorSoft.Optics.Lens<TestClass, object> Propety3Lens { get; } =
            new global::ThorSoft.Optics.Lens<TestClass, object>(
                static (instance) => instance.Propety3,
                static (value, instance) => instance with { Propety3 = value });

} // TestClass
