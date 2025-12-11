using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace ThorSoft.Optics.Generator.FocusProperties
{
    internal static class FocusPropertiesExtraction
    {
        public static FocusPropertiesOutput Invoke(GeneratorAttributeSyntaxContext context, CancellationToken cancellationToken)
        {
            if (context.TargetNode is not RecordDeclarationSyntax recordDeclarationSyntax
                || context.TargetSymbol is not INamedTypeSymbol recordTypeSymbol)
            {
                return FocusPropertiesDiagnostics.MustBeRecordType(context.TargetNode).AsOutput();
            }

            var typeName = recordTypeSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
            var typeKind = recordDeclarationSyntax.ClassOrStructKeyword.ValueText;

            ResizeArray<Diagnostic> diagnostics = new();
            ResizeArray<Lens> lenses = new();

            // Generate a lens for all properties from the primary constructor.
            var primaryConstructorParameters = recordDeclarationSyntax.ParameterList;
            foreach (var parameter in primaryConstructorParameters?.Parameters ?? [])
            {
                if (context.SemanticModel.GetDeclaredSymbol(parameter) is not IParameterSymbol parameterSymbol
                    || parameterSymbol.Type is not ITypeSymbol parameterTypeSymbol)
                {
                    diagnostics.Add(FocusPropertiesDiagnostics.Unexpected(
                        parameter,
                        "Failed to extract type information from primary constructor parameter"));
                    continue;
                }

                lenses.Add(new Lens
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
                    diagnostics.Add(FocusPropertiesDiagnostics.SkipStaticProperty(property));
                    continue;
                }

                if (!property.HasGetter())
                {
                    diagnostics.Add(FocusPropertiesDiagnostics.SkipPropertyWithoutGetter(property));
                    continue;
                }

                if (!property.HasSetter() && !property.HasInit())
                {
                    diagnostics.Add(FocusPropertiesDiagnostics.SkipPropertyWithoutInitOrSetter(property));
                    continue;
                }

                if (context.SemanticModel.GetDeclaredSymbol(property) is IPropertySymbol propertySymbol
                    && propertySymbol.Type is ITypeSymbol propertyTypeSymbol)
                {
                    lenses.Add(new Lens
                    {
                        Name = property.Identifier.Text,
                        Visibility = string.Join(" ", property.GetAccessModifiers()).Trim(),
                        Type = propertyTypeSymbol.ToString()
                    });
                }
            }

            if (lenses.Count == 0)
            {
                diagnostics.Add(FocusPropertiesDiagnostics.NoLensesToGenerate(recordDeclarationSyntax));
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
