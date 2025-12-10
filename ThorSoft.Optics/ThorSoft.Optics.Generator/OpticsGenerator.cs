using Microsoft.CodeAnalysis;

namespace ThorSoft.Optics.Generator
{
    [Generator]
    public class OpticsGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            context.RegisterPostInitializationOutput(i =>
            {
                i.AddEmbeddedAttributeDefinition();
                i.AddSource(
                    "FocusPropertiesAttribute.g.cs",
                    """
                    namespace ThorSoft.Optics;

                    [global::Microsoft.CodeAnalysis.EmbeddedAttribute]
                    [global::System.AttributeUsage(global::System.AttributeTargets.Class | global::System.AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
                    internal sealed class FocusPropertiesAttribute : global::System.Attribute { }
                    """);
            });
        }
    }
}
