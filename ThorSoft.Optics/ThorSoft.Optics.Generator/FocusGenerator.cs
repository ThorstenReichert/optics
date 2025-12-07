using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Immutable;
using ThorSoft.Optics.Generator.Diagnostics;
using ThorSoft.Optics.Generator.Generation;
using ThorSoft.Optics.Generator.Generation.Focus;
using ThorSoft.Optics.Generator.Options;
using ThorSoft.Optics.Generator.Syntax;
using ThorSoft.Optics.Generator.Util;

namespace ThorSoft.Optics.Generator
{
    [Generator]
    public sealed class FocusGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            IncrementalValueProvider<CodeGenerationOptions> options = context
                .CompilationProvider
                .Select(static (c, _) => c.GetCodeGenerationOptions());

            IncrementalValueProvider<(ImmutableArray<CodeGenerationRequest> Locations, CodeGenerationOptions Options)> focusToIntercept = context
                .SyntaxProvider
                .CreateSyntaxProvider(
                    predicate: static (node, _) => IsFocusCallCandidate(node),
                    transform: static (context, token) => GetFocusCallsToIntercept(context, token))
                .Where(static m => m is not null)!
                .Collect()
                .Combine(options)!;

            context.RegisterSourceOutput(focusToIntercept, static (spc, args) =>
                Execute(spc, args.Locations, args.Options));
        }

        private static bool IsFocusCallCandidate(SyntaxNode node)
        {
            return node.IsMethodInvocation(nameof(OpticsExtensions.Focus))
                || node.IsMethodInvocation(nameof(Lens<object>.Focus));
        }

        private static CodeGenerationRequest? GetFocusCallsToIntercept(GeneratorSyntaxContext context, CancellationToken token)
        {
            if (context.Node is not InvocationExpressionSyntax { Expression: MemberAccessExpressionSyntax { Name: { } nameSyntax } invocationMemberSyntax } invocationSyntax)
            {
                // Not interested in method invocations other than through member access, skip.
                return null;
            }

            if (context.SemanticModel.GetOperation(context.Node, token) is not IInvocationOperation targetOperation)
            {
                // Not sure if/how this could happen, skip.
                return null;
            }

#pragma warning disable RSEXPERIMENTAL002 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            if (context.SemanticModel.GetInterceptableLocation(invocationSyntax) is not { } location)
#pragma warning restore RSEXPERIMENTAL002 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            {
                // Do not even try to intercept anything we cannot get the location of, skip.
                return null;
            }

            if (IsBoundFocusInvocation(targetOperation))
            {
                // Extract the source type parameter for the generated lens from the first argument.
                var invocationInstanceArgumentType = targetOperation.Arguments[0].Value.Type;
                if (invocationInstanceArgumentType is not INamedTypeSymbol instanceType)
                {
                    return new NoInterception(DiagnosticsHelper.CreateUnexpectedDiagnostic(
                        targetOperation.Arguments[0].Value.Syntax,
                        $"Argument is not a '{nameof(INamedTypeSymbol)}' instance"));
                }

                // Extract the lambda expression defining the nested property path from the second argument.
                var invocationPathArgumentSyntax = targetOperation.Arguments[1].Value.Syntax;
                if (invocationPathArgumentSyntax is not LambdaExpressionSyntax lambdaExpressionSyntax)
                {
                    return new NoInterception(DiagnosticsHelper.CreateFocusArgumentMustBeLambdaExpression(
                        invocationPathArgumentSyntax));
                }

                var propertyPath = AnalyzeLensComposition(context.SemanticModel, lambdaExpressionSyntax);

                return new BoundFocusInterception
                {
                    Diagnostics = [],
                    Location = location,
                    OriginType = instanceType.GetFullName(),
                    FocusPath = new([.. propertyPath])
                };
            }

            else if (IsStaticFocusInvocation(targetOperation))
            {
                // Extract the lambda expression defining the nested property path from the first argument.
                var invocationPathArgumentSyntax = targetOperation.Arguments[0].Value.Syntax;
                if (invocationPathArgumentSyntax is not LambdaExpressionSyntax lambdaExpressionSyntax)
                {
                    return new NoInterception(DiagnosticsHelper.CreateFocusArgumentMustBeLambdaExpression(
                        invocationPathArgumentSyntax));
                }

                // Extract the source type parameter for the generated lens from the type argument of the
                // static class declaring the invoked method.
                if (invocationMemberSyntax.Expression is not GenericNameSyntax declaringTypeSyntax)
                {
                    return new NoInterception(DiagnosticsHelper.CreateUnexpectedDiagnostic(
                        invocationMemberSyntax,
                        $"Expected syntax type '{nameof(GenericNameSyntax)}'"));
                }

                if (declaringTypeSyntax.GetGenericTypeArgument(context.SemanticModel) is not INamedTypeSymbol genericTypeSymbol)
                {
                    return new NoInterception(DiagnosticsHelper.CreateUnexpectedDiagnostic(
                        invocationMemberSyntax,
                        $"Unable to resolve generic type for '{typeof(Lens<>).FullName}'"));
                }

                var propertyPath = AnalyzeLensComposition(context.SemanticModel, lambdaExpressionSyntax);

                return new StaticFocusInterception
                {
                    Diagnostics = [],
                    Location = location,
                    OriginType = genericTypeSymbol.GetFullName(),
                    FocusPath = new([.. propertyPath])
                };
            }
            else
            {
                return new NoInterception(DiagnosticsHelper.CreateSkipUnrecognizedFocusMethod(
                    context.Node));
            }
        }

        /// <summary>
        ///     Returns <c>true</c> if <paramref name="targetOperation"/> is an invocation of 
        ///     <see cref="LensExtensions.Focus{T, U}(T, System.Linq.Expressions.Expression{Func{T, U}})"/>,
        ///     <c>false</c> otherwise.
        /// </summary>
        private static bool IsBoundFocusInvocation(IInvocationOperation targetOperation)
        {
            return targetOperation.TargetMethod.IsMethod(nameof(OpticsExtensions.Focus), typeof(OpticsExtensions))
                && targetOperation.Instance is null
                && targetOperation.Arguments is { Length: 2 };
        }

        /// <summary>
        ///     Returns <c>true</c> if <paramref name="targetOperation"/> is an invocation of
        ///     <see cref="Lens{T}.Focus{U}(System.Linq.Expressions.Expression{Func{T, U}})"/>,
        ///     <c>false</c> otherwise.
        /// </summary>
        private static bool IsStaticFocusInvocation(IInvocationOperation targetOperation)
        {
            return targetOperation.TargetMethod.IsMethod(nameof(Lens<object>.Focus), typeof(Lens<>))
                && targetOperation.Instance is null
                && targetOperation.Arguments is { Length: 1 };
        }

        /// <summary>
        ///     Walk the lambda expression <paramref name="lambdaExpressionSyntax"/> and extract the 
        ///     intermediate lens types and property names necessary to construct the requested composite lens.
        /// </summary>
        /// <param name="semanticModel">The semantic model for type queries.</param>
        /// <param name="lambdaExpressionSyntax">The lambda expression to analyze</param>
        /// <returns>
        ///     A collection of <see cref="PropertyAccess"/> instances representing the composite lens.
        /// </returns>
        private static EquatableMemory<PropertyAccess> AnalyzeLensComposition(SemanticModel semanticModel, LambdaExpressionSyntax lambdaExpressionSyntax)
        {
            var propertyPath = new Stack<PropertyAccess>();
            var currentExpression = lambdaExpressionSyntax.ExpressionBody;

            while (currentExpression is MemberAccessExpressionSyntax memberAccessSyntax
                && semanticModel.GetSymbolInfo(memberAccessSyntax).Symbol is IPropertySymbol propertySymbol
                && propertySymbol.Type is INamedTypeSymbol propertyTypeSymbol)
            {
                propertyPath.Push(new PropertyAccess
                {
                    DeclaringTypeName = propertySymbol.ContainingType.GetFullName(),
                    PropertyType = propertyTypeSymbol.GetFullName(),
                    PropertyName = propertySymbol.Name,
                });

                currentExpression = memberAccessSyntax.Expression;
            }

            // TODO: check if no unexpected syntax token is left.

            return new([.. propertyPath]);
        }

        private static void Execute(SourceProductionContext context, ImmutableArray<CodeGenerationRequest> locations, CodeGenerationOptions options)
        {
            foreach (var diagnostic in locations.SelectMany(static x => x.Diagnostics))
            {
                context.ReportDiagnostic(diagnostic);
            }

            if (locations.Length != 0)
            {
                var sourceText = SourceGenerationHelper.GenerateFocusInterceptions(locations, options);
                if (sourceText is null)
                {
                    return;
                }

                context.AddSource(
                    $"LensGenerator.BoundFocusInterceptors.g.cs",
                    SourceText.From(sourceText, GlobalOptions.SourceTextEncoding));
            }
        }
    }
}
