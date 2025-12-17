namespace ThorSoft.Optics.Generator.FocusProperties
{
    internal static class FocusPropertiesAttribute
    {
        internal const string SourceText = """
            namespace ThorSoft.Optics;
            
            [global::Microsoft.CodeAnalysis.EmbeddedAttribute]
            [global::System.AttributeUsage(global::System.AttributeTargets.Class | global::System.AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
            internal sealed class FocusPropertiesAttribute : global::System.Attribute { }
            """;
    }
}
