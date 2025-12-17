//HintName: Test.Namespace.TestClass_Properties.g.cs

namespace ThorSoft.Optics;

public static class Test_Namespace_TestClass_Properties
{

    #region TestProperty

    private static global::ThorSoft.Optics.Lens<global::Test.Namespace.TestClass, int> TestProperty { get; } = new(
        static x => x.TestProperty,
        static (v, x) => x with { TestProperty = v });

    extension<T>(global::ThorSoft.Optics.OpticsBuilder<global::ThorSoft.Optics.Iso<T, global::Test.Namespace.TestClass>> proxy)
    {
        public global::ThorSoft.Optics.OpticsBuilder<global::ThorSoft.Optics.Lens<T, int>> TestProperty =>
            proxy.Compose(TestProperty);
    }

    extension<T>(global::ThorSoft.Optics.OpticsBuilder<global::ThorSoft.Optics.Lens<T, global::Test.Namespace.TestClass>> proxy)
    {
        public global::ThorSoft.Optics.OpticsBuilder<global::ThorSoft.Optics.Lens<T, int>> TestProperty =>
            proxy.Compose(TestProperty);
    }

    extension<T>(global::ThorSoft.Optics.OpticsBuilder<global::ThorSoft.Optics.Optional<T, global::Test.Namespace.TestClass>> proxy)
    {
        public global::ThorSoft.Optics.OpticsBuilder<global::ThorSoft.Optics.Optional<T, int>> TestProperty =>
            proxy.Compose(TestProperty);
    }

    extension<T>(global::ThorSoft.Optics.OpticsBuilder<global::ThorSoft.Optics.BoundIso<T, global::Test.Namespace.TestClass>> proxy)
    {
        public global::ThorSoft.Optics.OpticsBuilder<global::ThorSoft.Optics.BoundLens<T, int>> TestProperty =>
            proxy.Compose(TestProperty);
    }

    extension<T>(global::ThorSoft.Optics.OpticsBuilder<global::ThorSoft.Optics.BoundLens<T, global::Test.Namespace.TestClass>> proxy)
    {
        public global::ThorSoft.Optics.OpticsBuilder<global::ThorSoft.Optics.BoundLens<T, int>> TestProperty =>
            proxy.Compose(TestProperty);
    }

    extension<T>(global::ThorSoft.Optics.OpticsBuilder<global::ThorSoft.Optics.BoundOptional<T, global::Test.Namespace.TestClass>> proxy)
    {
        public global::ThorSoft.Optics.OpticsBuilder<global::ThorSoft.Optics.BoundOptional<T, int>> TestProperty =>
            proxy.Compose(TestProperty);
    }

    #endregion

}
