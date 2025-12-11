using Microsoft.CodeAnalysis;
using ThorSoft.Optics.Generator.FocusProperties;

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
                    FocusPropertiesAttribute.SourceText);
            });

            IncrementalValuesProvider<FocusPropertiesOutput> focusProperties = context.SyntaxProvider
                .ForAttributeWithMetadataName(
                    "ThorSoft.Optics.FocusPropertiesAttribute",
                    predicate: static (node, _) => true,
                    transform: FocusPropertiesExtraction.Invoke)
                .Where(x => x is not null);

            context.RegisterSourceOutput(
                focusProperties,
                FocusPropertiesGeneration.Invoke);
        }
    }
}
