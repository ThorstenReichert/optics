using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ThorSoft.Optics.Generator.Diagnostics
{
    internal static class DiagnosticsHelper
    {

        private static readonly DiagnosticDescriptor SkipStaticProperty = new(
            id: "LENSGEN0001",
            title: "Skip static property",
            messageFormat: "No lens generated for static property",
            category: Category.CodeGeneration,
            defaultSeverity: DiagnosticSeverity.Info,
            isEnabledByDefault: false);
        public static Diagnostic CreateSkipStaticProperty(PropertyDeclarationSyntax property)
        {
            return Diagnostic.Create(
                SkipStaticProperty,
                property.GetLocation());
        }

        private static readonly DiagnosticDescriptor SkipPropertyWithoutGetter = new(
            id: "LENSGEN0002",
            title: "Skip property without getter",
            messageFormat: "No lens generated for property without getter",
            category: Category.CodeGeneration,
            defaultSeverity: DiagnosticSeverity.Info,
            isEnabledByDefault: false);
        public static Diagnostic CreateSkipPropertyWithoutGetter(PropertyDeclarationSyntax property)
        {
            return Diagnostic.Create(
                SkipPropertyWithoutGetter,
                property.GetLocation());
        }

        private static readonly DiagnosticDescriptor SkipPropertyWithoutInitOrSetter = new(
            id: "LENSGEN0003",
            title: "Skip property without init or setter",
            messageFormat: "No lens generated for property with neither init not setter",
            category: Category.CodeGeneration,
            defaultSeverity: DiagnosticSeverity.Info,
            isEnabledByDefault: false);
        public static Diagnostic CreateSkipPropertyWithoutInitOrSetter(PropertyDeclarationSyntax property)
        {
            return Diagnostic.Create(
                SkipPropertyWithoutInitOrSetter,
                property.GetLocation());
        }

        private static readonly DiagnosticDescriptor UnsupportedGenerateLensesTarget = new(
            id: "LENSGEN0004",
            title: "Unsupported syntax node for lens generation",
            messageFormat: "Syntax nodes of kind '{0}' is not supported",
            category: Category.CodeGeneration,
            defaultSeverity: DiagnosticSeverity.Warning,
            isEnabledByDefault: true);
        public static Diagnostic CreateUnsupportedGenerateLensesTarget(SyntaxNode targetNode)
        {

            return Diagnostic.Create(
                UnsupportedGenerateLensesTarget,
                targetNode.GetLocation(),
                targetNode.Kind());
        }

        private static readonly DiagnosticDescriptor NoLensesToGenerate = new(
            id: "LENSGEN0005",
            title: "No lenses found to generate",
            messageFormat: "Record is registered for lens generation, but no suitable properties were found",
            category: Category.CodeGeneration,
            defaultSeverity: DiagnosticSeverity.Info,
            isEnabledByDefault: true);
        public static Diagnostic CreateNoLensesToGenerate(RecordDeclarationSyntax record)
        {
            return Diagnostic.Create(
                NoLensesToGenerate,
                record.GetLocation());
        }

        private static readonly DiagnosticDescriptor MissingPartialKeyword = new(
            id: "LENSGEN0006",
            title: "Cannot generate lenses for non-partial type declaration",
            messageFormat: "Record is registered for lens generation, but is missing the partial keyword",
            category: Category.CodeGeneration,
            defaultSeverity: DiagnosticSeverity.Warning,
            isEnabledByDefault: true);
        public static Diagnostic CreateMissingPartialKeyword(RecordDeclarationSyntax record)
        {
            return Diagnostic.Create(
                MissingPartialKeyword,
                record.GetLocation());
        }

        private static readonly DiagnosticDescriptor FocusArgumentMustBeLambdaExpression = new(

            id: "LENSGEN1001",
            title: "Unsupported Focus() argument",
            messageFormat: "Focus argument must be lambda expression, but found '{0}'",
            category: Category.Interception,
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);
        public static Diagnostic CreateFocusArgumentMustBeLambdaExpression(SyntaxNode node)
        {
            return Diagnostic.Create(
                FocusArgumentMustBeLambdaExpression,
                node.GetLocation(),
                node.Kind());
        }

        private static readonly DiagnosticDescriptor SkipUnrecognizedFocusMethod = new(

            id: "LENSGEN1002",
            title: "Skip unrecognized Focus() invocation",
            messageFormat: "Focus method invocation does not match any known signatures",
            category: Category.Interception,
            defaultSeverity: DiagnosticSeverity.Info,
            isEnabledByDefault: false);
        public static Diagnostic CreateSkipUnrecognizedFocusMethod(SyntaxNode node)
        {
            return Diagnostic.Create(
                SkipUnrecognizedFocusMethod,
                node.GetLocation());
        }

        private static readonly DiagnosticDescriptor Unexpected = new(
            id: "LENSGEN9999",
            title: "Unexpected Issue",
            messageFormat: "Encountered unexpected issue: {0}",
            category: Category.Unexpected,
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);
        public static Diagnostic CreateUnexpectedDiagnostic(SyntaxNode node, string message)
        {
            return Diagnostic.Create(
                Unexpected,
                node.GetLocation(),
                message);
        }
    }
}