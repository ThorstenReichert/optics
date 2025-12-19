using Microsoft.CodeAnalysis;

namespace ThorSoft.Optics.Generator.Model
{
    internal static class AccessibilityExtensions
    {
        /// <summary>
        ///     Returns the stricter of the two accessibilities.
        /// </summary>
        public static Accessibility ToEffectiveAccessibility(this Accessibility accessibility, Accessibility parentAccessibility) =>
            (accessibility, parentAccessibility) switch
            {
                (Accessibility.NotApplicable, _) => parentAccessibility,
                (_, Accessibility.NotApplicable) => accessibility,

                _ => (Accessibility) Math.Min((int) accessibility, (int) parentAccessibility)
            };

        /// <summary>
        ///     Converte the accessibility into the equivalent keywords in source-code.
        /// </summary>
        public static string ToKeywords(this Accessibility accessibility) => accessibility switch
        {
            Accessibility.NotApplicable => "",
            Accessibility.Private => "private",
            Accessibility.ProtectedAndInternal => "private protected",
            Accessibility.Protected => "protected",
            Accessibility.Internal => "internal",
            Accessibility.ProtectedOrInternal => "protected internal",
            Accessibility.Public => "public",

            _ => throw new NotSupportedException(accessibility.ToString())
        };
    }
}
