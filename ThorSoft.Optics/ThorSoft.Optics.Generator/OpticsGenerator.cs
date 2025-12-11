using Microsoft.CodeAnalysis;
using ThorSoft.Optics.Generator.FocusProperties;
using ThorSoft.Optics.Generator.Util;

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

    internal sealed record class FocusPropertiesOutput
    {
        public string? TypeName { get; init; }
        public string? TypeKind { get; init; }

        public EquatableMemory<Diagnostic> Diagnostics { get; init; }
        public EquatableMemory<Lens> Lenses { get; init; }
    }

    internal sealed record class Lens
    {
        public required string Name { get; init; }
        public required string Type { get; init; }
        public required string Visibility { get; init; }
    }

    file static class Extensions
    {
        public static FocusPropertiesOutput AsOutput(this Diagnostic diagnostic) =>
            new()
            {
                Diagnostics = new EquatableMemory<Diagnostic>([diagnostic])
            };
    }
}
