using Microsoft.CodeAnalysis;

namespace ThorSoft.Optics.Generator.Model
{
    internal static class TypeSymbolExtensions
    {
        /// <summary>
        ///     Returns the effective accessibility of the type and all types it is nested within.
        /// </summary>
        public static Accessibility GetEffectiveAccessibility(this ITypeSymbol typeSymbol)
        {
            var accessibility = typeSymbol.DeclaredAccessibility;
            var currentSymbol = typeSymbol;

            while (currentSymbol?.ContainingType is not null)
            {
                currentSymbol = currentSymbol.ContainingType;
                accessibility = accessibility.ToEffectiveAccessibility(currentSymbol.DeclaredAccessibility);
            }

            return accessibility;
        }
    }
}