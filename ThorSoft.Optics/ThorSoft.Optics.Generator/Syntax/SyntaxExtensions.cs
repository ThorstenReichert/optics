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
        ///     Returns the generic parameter type of the given <see cref="GenericNameSyntax"/> with index <paramref name="index"/>
        ///     in its type parameter list.
        /// </summary>
        public static INamedTypeSymbol? GetGenericTypeArgument(this GenericNameSyntax name, SemanticModel semanticModel, int index = 0)
        {
            var typeArgumentList = name.TypeArgumentList.Arguments;

            if (index < typeArgumentList.Count && semanticModel.GetSymbolInfo(typeArgumentList[index]).Symbol is INamedTypeSymbol typeSymbol)
            {
                return typeSymbol;
            }

            return null;
        }

        /// <summary>
        ///     Checks if the given <see cref="SyntaxNode"/> refers to a method invocation with method name <paramref name="methodName"/>.
        /// </summary>
        public static bool IsMethodInvocation(this SyntaxNode node, string methodName) =>
            node is InvocationExpressionSyntax
            {
                Expression: MemberAccessExpressionSyntax
                {
                    Name.Identifier.ValueText: string candidateMethodName
                }
            }
            && candidateMethodName == methodName;

        /// <summary>
        ///     Checks if the given <see cref="PropertyDeclarationSyntax"/> refers to a static property.
        /// </summary>
        public static bool IsStaticProperty(this PropertyDeclarationSyntax property) =>
            property.HasModifierKind(SyntaxKind.StaticKeyword);

        /// <summary>
        ///     Checks if the given <see cref="RecordDeclarationSyntax"/> declares a <c>struct</c> record.
        /// </summary>
        public static bool IsStruct(this RecordDeclarationSyntax declaration) =>
            declaration.ClassOrStructKeyword.ValueText switch
            {
                "struct" => true,
                _ => false
            };

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

        private static bool HasModifierKind(this PropertyDeclarationSyntax property, SyntaxKind kind) =>
            property.Modifiers.Any(modifier => modifier.IsKind(kind));
    }
}
