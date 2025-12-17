using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Frozen;

namespace ThorSoft.Optics.Generator.Syntax
{
    internal static class SyntaxExtensions
    {
        private static readonly FrozenSet<SyntaxKind> AccessModifierKinds =
            new HashSet<SyntaxKind>{
                SyntaxKind.PublicKeyword,
                SyntaxKind.ProtectedKeyword,
                SyntaxKind.InternalKeyword,
                SyntaxKind.PrivateKeyword
            }
            .ToFrozenSet();

        /// <summary>
        ///     Returns the list of all access modifies on the given <see cref="PropertyDeclarationSyntax"/> 
        ///     (i.e. <c>private</c>, <c>protected</c>, <c>internal</c>, or <c>public</c>)
        /// </summary>
        public static IEnumerable<string> GetAccessModifiers(this PropertyDeclarationSyntax property)
        {
            foreach (var modifier in property.Modifiers)
            {
                if (AccessModifierKinds.Contains(modifier.Kind()))
                {
                    yield return modifier.ToString();
                }
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
    }
}
