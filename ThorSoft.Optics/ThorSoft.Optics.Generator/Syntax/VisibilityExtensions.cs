using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Frozen;

namespace ThorSoft.Optics.Generator.Syntax
{
    internal static class VisibilityExtensions
    {
        private static readonly FrozenDictionary<Visibility, string> VisibilityText =
            new Dictionary<Visibility, string>
            {
                [Visibility.Public] = "public",
                [Visibility.Internal] = "internal",
                [Visibility.Protected] = "protected",
                [Visibility.PrivateProtected] = "private protected",
                [Visibility.Private] = "private"
            }
            .ToFrozenDictionary();

        /// <summary>
        ///     Returns the more restrictive visibility between the two.
        /// </summary>
        public static Visibility Merge(this Visibility first, Visibility second) =>
            (first, second) switch
            {
                (Visibility.Private, _) or (_, Visibility.Private) =>
                    Visibility.Private,

                (Visibility.PrivateProtected, _) or (_, Visibility.PrivateProtected) =>
                    Visibility.PrivateProtected,

                (Visibility.Protected, _) or (_, Visibility.Protected) =>
                    Visibility.Protected,

                (Visibility.Internal, _) or (_, Visibility.Internal) =>
                    Visibility.Internal,

                _ =>
                    Visibility.Public
            };

        /// <summary>
        ///     Converts the visibility to its declaration string.
        /// </summary>
        public static string ToDeclarationString(this Visibility visibility) =>
            VisibilityText[visibility];
    }
}
