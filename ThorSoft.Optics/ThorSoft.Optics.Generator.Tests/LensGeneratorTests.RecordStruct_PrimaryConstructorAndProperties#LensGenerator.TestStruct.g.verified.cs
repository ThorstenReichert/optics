//HintName: LensGenerator.TestStruct.g.cs
namespace Test.Module;

partial record struct TestStruct
{

        public static global::ThorSoft.Optics.Lens<TestStruct, int> Property1Lens =>
            new global::ThorSoft.Optics.Lens<TestStruct, int>(
                static (instance) => instance.Property1,
                static (value, instance) => instance with { Property1 = value });

        public static global::ThorSoft.Optics.Lens<TestStruct, string> Property2Lens =>
            new global::ThorSoft.Optics.Lens<TestStruct, string>(
                static (instance) => instance.Property2,
                static (value, instance) => instance with { Property2 = value });

        public static global::ThorSoft.Optics.Lens<TestStruct, object> Propety3Lens =>
            new global::ThorSoft.Optics.Lens<TestStruct, object>(
                static (instance) => instance.Propety3,
                static (value, instance) => instance with { Propety3 = value });

        public static global::ThorSoft.Optics.Lens<TestStruct, int> DeclaredProperty1Lens =>
            new global::ThorSoft.Optics.Lens<TestStruct, int>(
                static (instance) => instance.DeclaredProperty1,
                static (value, instance) => instance with { DeclaredProperty1 = value });

} // TestStruct
