using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using ThorSoft.Optics.Generator.Diagnostics;
using ThorSoft.Optics.Generator.Generation;
using ThorSoft.Optics.Generator.Generation.Lenses;
using ThorSoft.Optics.Generator.Options;
using ThorSoft.Optics.Generator.Syntax;
using ThorSoft.Optics.Generator.Util;

namespace ThorSoft.Optics.Generator
{
    [Generator]
    public sealed class LensGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            IncrementalValueProvider<CodeGenerationOptions> options = context
                .CompilationProvider
                .Select(static (c, _) => c.GetCodeGenerationOptions());

            IncrementalValuesProvider<(CodeGenerationRequest Request, CodeGenerationOptions Options)> recordsToGenerate = context
                .SyntaxProvider
                .ForAttributeWithMetadataName(
                    typeof(GenerateLensesAttribute).FullName,
                    predicate: static (s, _) => true,
                    transform: static (ctx, _) => GetClassToGenerate(ctx.SemanticModel, ctx.TargetNode))
                .Where(static m => m is not null)
                .Combine(options);

            context.RegisterSourceOutput(recordsToGenerate, static (spc, source) =>
                Execute(source.Request, source.Options, spc));
        }

        private static CodeGenerationRequest GetClassToGenerate(SemanticModel semanticModel, SyntaxNode targetNode)
        {
            // Generate new partial class definition for all classes with GenerateLenses attribute.
            if (targetNode is not RecordDeclarationSyntax recordDeclarationSyntax
                || semanticModel.GetDeclaredSymbol(recordDeclarationSyntax) is not INamedTypeSymbol recordTypeSymbol)
            {
                return NoGeneratedLenses(DiagnosticsHelper.CreateUnsupportedGenerateLensesTarget(targetNode));
            }

            if (!recordDeclarationSyntax.IsPartialDeclaration())
            {
                return NoGeneratedLenses(DiagnosticsHelper.CreateMissingPartialKeyword(recordDeclarationSyntax));
            }

            ResizeArray<LensToGenerate> properties = new();
            ResizeArray<Diagnostic> diagnostics = new();

            // Generate a lens for all properties from the primary constructor.
            var primaryConstructorParameters = recordDeclarationSyntax.ParameterList;
            foreach (var parameter in primaryConstructorParameters?.Parameters ?? [])
            {
                if (semanticModel.GetDeclaredSymbol(parameter) is not IParameterSymbol parameterSymbol
                    || parameterSymbol.Type is not ITypeSymbol parameterTypeSymbol)
                {
                    diagnostics.Add(DiagnosticsHelper.CreateUnexpectedDiagnostic(
                        parameter,
                        "Failed to extract type information from primary constructor parameter"));
                    continue;
                }

                properties.Add(new LensToGenerate
                {
                    Name = parameter.Identifier.Text,
                    Visibility = "public",
                    Type = parameterTypeSymbol.ToString()
                });
            }

            // Generate a lens for all properties defined on the class.
            var propertyDeclarations = recordDeclarationSyntax.Members.OfType<PropertyDeclarationSyntax>();
            foreach (var property in propertyDeclarations)
            {
                if (property.IsStaticProperty())
                {
                    diagnostics.Add(DiagnosticsHelper.CreateSkipStaticProperty(property));
                    continue;
                }

                if (!property.HasGetter())
                {
                    diagnostics.Add(DiagnosticsHelper.CreateSkipPropertyWithoutGetter(property));
                    continue;
                }

                if (!property.HasSetter() && !property.HasInit())
                {
                    diagnostics.Add(DiagnosticsHelper.CreateSkipPropertyWithoutInitOrSetter(property));
                    continue;
                }

                if (semanticModel.GetDeclaredSymbol(property) is IPropertySymbol propertySymbol
                    && propertySymbol.Type is ITypeSymbol propertyTypeSymbol)
                {
                    properties.Add(new LensToGenerate
                    {
                        Name = property.Identifier.Text,
                        Visibility = string.Join(" ", property.GetAccessModifiers()).Trim(),
                        Type = propertyTypeSymbol.ToString()
                    });
                }
            }

            if (properties.Count == 0)
            {
                diagnostics.Add(DiagnosticsHelper.CreateNoLensesToGenerate(recordDeclarationSyntax));
            }

            return new RecordToGenerate
            {
                IsStructType = recordDeclarationSyntax.IsStruct(),
                TypeName = recordDeclarationSyntax.Identifier.Text,
                TypeNamespace = recordTypeSymbol.ContainingNamespace.ToString(),
                TypeKind = recordDeclarationSyntax.ClassOrStructKeyword.ValueText,
                Properties = new EquatableMemory<LensToGenerate>(properties.Extract()),
                Diagnostics = new EquatableMemory<Diagnostic>(diagnostics.Extract())
            };
        }

        private static void Execute(CodeGenerationRequest request, CodeGenerationOptions options, SourceProductionContext spc)
        {
            foreach (var diagnostic in request.Diagnostics)
            {
                spc.ReportDiagnostic(diagnostic);
            }

            if (request is RecordToGenerate recordToGenerate)
            {
                var sourceText = SourceGenerationHelper.GenerateLenses(recordToGenerate, options);
                if (sourceText is null)
                {
                    return;
                }

                spc.AddSource(
                    $"LensGenerator.{recordToGenerate.TypeName}.g.cs",
                    SourceText.From(sourceText, GlobalOptions.SourceTextEncoding));
            }
        }

        private static CodeGenerationRequest NoGeneratedLenses(params Diagnostic[] diagnostics)
        {
            return new CodeGenerationRequest
            {
                Diagnostics = new EquatableMemory<Diagnostic>(diagnostics)
            };
        }
    }
}
