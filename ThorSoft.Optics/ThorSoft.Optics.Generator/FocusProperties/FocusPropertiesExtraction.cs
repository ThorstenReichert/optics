using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ThorSoft.Optics.Generator.Diagnostics;
using ThorSoft.Optics.Generator.Model;
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

            var typeAccessibility = recordTypeSymbol.GetEffectiveAccessibility();
            if (IsInaccessible(typeAccessibility))
            {
                return DiagnosticsFactory.SkipInaccessibleNestedRecord(recordDeclarationSyntax, typeAccessibility.ToKeywords()).AsOutput();
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
                    diagnostics.Add(DiagnosticsFactory.Unexpected(
                        parameter,
                        "Failed to extract type information from primary constructor parameter"));
                    continue;
                }

                lenses.Add(new Lens
                {
                    Name = parameter.Identifier.Text,
                    Visibility = Accessibility.Public.ToEffectiveAccessibility(typeAccessibility).ToKeywords(),
                    Type = parameterTypeSymbol.ToString()
                });
            }

            var propertySymbols = recordTypeSymbol.GetMembers().OfType<IPropertySymbol>();
            foreach (var propertySymbol in propertySymbols)
            {
                if (propertySymbol.IsImplicitlyDeclared)
                {
                    // Silently skip compiler-generated properties such as EqualityContract.
                    continue;
                }

                if (propertySymbol.IsStatic)
                {
                    diagnostics.AddSkipStaticProperty(propertySymbol);
                    continue;
                }

                if (propertySymbol.GetMethod is null)
                {
                    diagnostics.AddSkipPropertyWithoutGetter(propertySymbol);
                    continue;
                }

                if (propertySymbol.SetMethod is null)
                {
                    // Init-properties also have SetMethod (but with IsInitOnly = true).
                    diagnostics.AddSkipPropertyWithoutInitOrSetter(propertySymbol);
                    continue;
                }

                var propertyAccessibility = propertySymbol.DeclaredAccessibility;
                if (IsInaccessible(propertyAccessibility))
                {
                    diagnostics.AddSkipInaccessibleProperty(propertySymbol, propertyAccessibility.ToKeywords());
                    continue;
                }

                if (propertySymbol.Type is ITypeSymbol propertyTypeSymbol)
                {
                    lenses.Add(new Lens
                    {
                        Name = propertySymbol.Name,
                        Visibility = GetPropertyExtensionAccessibility(propertyAccessibility, typeAccessibility).ToKeywords(),
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

        /// <summary>
        ///     Checks if the given accessibility is suitable for code generation of extensions.
        /// </summary>
        private static bool IsInaccessible(Accessibility accessibility) =>
            accessibility
                is Accessibility.Private
                or Accessibility.ProtectedAndInternal
                or Accessibility.Protected;

        /// <summary>
        ///     Returns accessibility of the extension method to generate for a given property.
        /// </summary>
        private static Accessibility GetPropertyExtensionAccessibility(Accessibility propertyAccessibility, Accessibility typeAccesibility)
        {
            var effectiveAccessibility = propertyAccessibility.ToEffectiveAccessibility(typeAccesibility);

            return effectiveAccessibility switch
            {
                // Protected internal extensions are not possible, downgrade to internal.
                Accessibility.ProtectedOrInternal => Accessibility.Internal,

                _ => effectiveAccessibility
            };
        }
    }
}
