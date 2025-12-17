using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ThorSoft.Optics.Generator.Syntax
{
    internal static class SyntaxExtensions
    {
        /// <summary>
        ///     Returns the visibility of the given property.
        /// </summary>
        public static Visibility GetVisibility(this PropertyDeclarationSyntax property)
        {
            var modifiers = property.Modifiers;

            if (TryGetVisibility(modifiers, out var visibility))
            {
                return visibility;
            }
            else
            {
                // Default visibility for properties is internal
                return Visibility.Private;
            }
        }

        /// <summary>
        ///     Returns the visibility of the given record.
        /// </summary>
        public static Visibility GetVisibility(this RecordDeclarationSyntax record)
        {
            var modifiers = record.Modifiers;

            if (TryGetVisibility(modifiers, out var visibility))
            {
                return visibility;
            }
            else
            {
                // Default visibility for records is internal
                return Visibility.Internal;
            }
        }

        /// <summary>
        ///     Checks if the given <see cref="PropertyDeclarationSyntax"/> refers to a static property.
        /// </summary>
        public static bool IsStaticProperty(this PropertyDeclarationSyntax property) =>
            property.HasModifierKind(SyntaxKind.StaticKeyword);

        /// <summary>
        ///     Checks if the given <see cref="PropertyDeclarationSyntax"/> has a get-accessor.
        /// </summary>
        public static bool HasGetter(this PropertyDeclarationSyntax property) =>
            property.HasAccessorKind(SyntaxKind.GetAccessorDeclaration);

        /// <summary>
        ///     Checks if the given <see cref="PropertyDeclarationSyntax"/> has an init-accessor.
        /// </summary>
        public static bool HasInit(this PropertyDeclarationSyntax property) =>
            property.HasAccessorKind(SyntaxKind.InitAccessorDeclaration);

        /// <summary>
        ///     Checks if the given <see cref="PropertyDeclarationSyntax"/> has an set-accessor.
        /// </summary>
        public static bool HasSetter(this PropertyDeclarationSyntax property) =>
            property.HasAccessorKind(SyntaxKind.SetAccessorDeclaration);

        private static bool HasAccessorKind(this PropertyDeclarationSyntax property, SyntaxKind kind) =>
            property.AccessorList?.Accessors.Any(accessor => accessor.IsKind(kind)) == true;

        private static bool HasModifierKind(this MemberDeclarationSyntax declaration, SyntaxKind kind) =>
            declaration.Modifiers.Any(modifier => modifier.IsKind(kind));

        private static bool TryGetVisibility(SyntaxTokenList modifiers, out Visibility visibility)
        {
            if (modifiers.Any(static x => x.IsKind(SyntaxKind.PrivateKeyword))
                && modifiers.Any(static x => x.IsKind(SyntaxKind.ProtectedKeyword)))
            {
                visibility = Visibility.PrivateProtected;
                return true;
            }
            else if (modifiers.Any(static x => x.IsKind(SyntaxKind.PublicKeyword)))
            {
                visibility = Visibility.Public;
                return true;
            }
            else if (modifiers.Any(static x => x.IsKind(SyntaxKind.PrivateKeyword)))
            {
                visibility = Visibility.Private;
                return true;
            }
            else if (modifiers.Any(static x => x.IsKind(SyntaxKind.ProtectedKeyword)))
            {
                visibility = Visibility.Protected;
                return true;
            }
            else if (modifiers.Any(static x => x.IsKind(SyntaxKind.InternalKeyword)))
            {
                visibility = Visibility.Internal;
                return true;
            }
            else
            {
                visibility = default;
                return false;
            }
        }
    }
}
