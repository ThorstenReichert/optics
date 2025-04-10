using Microsoft.CodeAnalysis;

namespace ThorSoft.Optics.Generator.Syntax
{
    internal static class SymbolExtensions
    {
        private static readonly SymbolDisplayFormat FullNameFormat = new(typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces);

        /// <summary>
        ///     
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public static string GetFullName(this INamedTypeSymbol symbol)
        {
            return symbol.ToDisplayString(FullNameFormat);
        }

        /// <summary>
        ///     Checks if the given <see cref="IMethodSymbol"/> refers to the method <paramref name="methodName"/> on type <paramref name="containingType"/>.
        /// </summary>
        public static bool IsMethod(this IMethodSymbol symbol, string methodName, Type containingType)
        {
            return symbol.Name == methodName
                && symbol.ContainingType.IsType(containingType);
        }

        /// <summary>
        ///     Checks if the given <see cref="INamedTypeSymbol"/> refers to the type <paramref name="type"/>.
        /// </summary>
        public static bool IsType(this INamedTypeSymbol symbol, Type type)
        {
            return symbol.MetadataName == type.Name
                && symbol.HasAbsoluteNamespace(type.Namespace);
        }

        /// <summary>
        ///     Checks if the given <see cref="INamedTypeSymbol"/> has the exact namespace <paramref name="namespace"/>.
        /// </summary>
        /// <returns></returns>
        public static bool HasAbsoluteNamespace(this INamedTypeSymbol symbol, string @namespace)
        {
            var parts = @namespace.Split('.');
            var current = symbol.ContainingNamespace;

            foreach (var part in parts.Reverse())
            {
                if (current.Name != part)
                {
                    return false;
                }

                current = current.ContainingNamespace;
            }

            return current.IsGlobalNamespace;
        }
    }
}
