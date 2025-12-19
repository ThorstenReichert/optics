using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ThorSoft.Optics.Generator.Diagnostics;
using ThorSoft.Optics.Generator.Syntax;
using ThorSoft.Optics.Generator.Util;

namespace ThorSoft.Optics.Generator.FocusProperties
{
    internal static class FocusPropertiesExtraction
    {
        public static FocusPropertiesRequest Invoke(GeneratorAttributeSyntaxContext context, CancellationToken _)
        {
            if (context.TargetNode is not RecordDeclarationSyntax recordDeclarationSyntax
                || context.TargetSymbol is not INamedTypeSymbol recordTypeSymbol)
            {
                return DiagnosticsFactory.MustBeRecordType(context.TargetNode).AsOutput();
            }

            var typeName = recordTypeSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
            var typeKind = recordDeclarationSyntax.ClassOrStructKeyword.ValueText;
            var typeVisibility = recordDeclarationSyntax.GetVisibility();

            ResizeArray<Diagnostic> diagnostics = new();
            ResizeArray<Lens> lenses = new();

            // Generate a lens for all properties from the primary constructor.
            var primaryConstructorParameters = recordDeclarationSyntax.ParameterList;
            foreach (var parameter in primaryConstructorParameters?.Parameters ?? [])
            {
                if (context.SemanticModel.GetDeclaredSymbol(parameter) is not IParameterSymbol parameterSymbol
                    || parameterSymbol.Type is not ITypeSymbol parameterTypeSymbol)
                {
                    diagnostics.Add(DiagnosticsFactory.Unexpected(
                        parameter,
                        "Failed to extract type information from primary constructor parameter"));
                    continue;
                }

                lenses.Add(new Lens
                {
                    Name = parameter.Identifier.Text,
                    Visibility = typeVisibility.Merge(Visibility.Public).ToDeclarationString(),
                    Type = parameterTypeSymbol.ToString()
                });
            }

            // Generate a lens for all properties defined on the class.
            var propertyDeclarations = recordDeclarationSyntax.Members.OfType<PropertyDeclarationSyntax>();
            foreach (var property in propertyDeclarations)
            {
                if (property.IsStaticProperty())
                {
                    diagnostics.Add(DiagnosticsFactory.SkipStaticProperty(property));
                    continue;
                }

                if (!property.HasGetter())
                {
                    diagnostics.Add(DiagnosticsFactory.SkipPropertyWithoutGetter(property));
                    continue;
                }

                if (!property.HasSetter() && !property.HasInit())
                {
                    diagnostics.Add(DiagnosticsFactory.SkipPropertyWithoutInitOrSetter(property));
                    continue;
                }

                var propertyVisibility = property.GetVisibility();
                if (propertyVisibility is Visibility.Private or Visibility.PrivateProtected or Visibility.Protected)
                {
                    diagnostics.Add(DiagnosticsFactory.SkipInaccessibleProperty(property, propertyVisibility));
                    continue;
                }

                if (context.SemanticModel.GetDeclaredSymbol(property) is IPropertySymbol propertySymbol
                    && propertySymbol.Type is ITypeSymbol propertyTypeSymbol)
                {
                    lenses.Add(new Lens
                    {
                        Name = property.Identifier.Text,
                        Visibility = property.GetVisibility().Merge(typeVisibility).ToDeclarationString(),
                        Type = propertyTypeSymbol.ToString()
                    });
                }
            }

            if (lenses.Count == 0)
            {
                diagnostics.Add(DiagnosticsFactory.NoLensesToGenerate(recordDeclarationSyntax));
            }

            return new()
            {
                TypeKind = typeKind,
                TypeName = typeName,
                Diagnostics = new(diagnostics.Extract()),
                Lenses = new(lenses.Extract())
            };
        }
    }
}
