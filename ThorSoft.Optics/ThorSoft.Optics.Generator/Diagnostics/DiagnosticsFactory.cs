using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ThorSoft.Optics.Generator.Util;

namespace ThorSoft.Optics.Generator.Diagnostics
{
    internal static class DiagnosticsFactory
    {
        #region 1000 - 1999 FocusProperties Diagnostics

        private static readonly DiagnosticDescriptor MustBeRecordTypeTemplate = new(
            id: "OPTICS1001",
            title: "Must be record type",
            messageFormat: "FocusProperties can only be used with record types",
            category: Category.CodeGeneration,
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);
        public static Diagnostic MustBeRecordType(SyntaxNode node)
        {
            return Diagnostic.Create(
                MustBeRecordTypeTemplate,
                node.GetLocation());
        }


        private static readonly DiagnosticDescriptor NoLensesToGenerateTemplate = new(
            id: "OPTICS1002",
            title: "No lenses found to generate",
            messageFormat: "Record is registered for lens generation, but no suitable properties were found",
            category: Category.CodeGeneration,
            defaultSeverity: DiagnosticSeverity.Info,
            isEnabledByDefault: true);
        public static Diagnostic NoLensesToGenerate(RecordDeclarationSyntax record)
        {
            return Diagnostic.Create(
                NoLensesToGenerateTemplate,
                record.GetLocation());
        }

        private static readonly DiagnosticDescriptor SkipStaticPropertyTemplate = new(
            id: "OPTICS1003",
            title: "Skip static property",
            messageFormat: "No lens generated for static property",
            category: Category.CodeGeneration,
            defaultSeverity: DiagnosticSeverity.Info,
            isEnabledByDefault: false);
        public static void AddSkipStaticProperty(this ResizeArray<Diagnostic> diagnostics, IPropertySymbol property)
        {
            foreach (var syntaxReference in property.DeclaringSyntaxReferences)
            {
                diagnostics.Add(Diagnostic.Create(
                    SkipStaticPropertyTemplate,
                    syntaxReference.GetSyntax().GetLocation()));
            }
        }

        private static readonly DiagnosticDescriptor SkipPropertyWithoutGetterTemplate = new(
            id: "OPTICS1004",
            title: "Skip property without getter",
            messageFormat: "No lens generated for property without getter",
            category: Category.CodeGeneration,
            defaultSeverity: DiagnosticSeverity.Info,
            isEnabledByDefault: false);
        public static void AddSkipPropertyWithoutGetter(this ResizeArray<Diagnostic> diagnostics, IPropertySymbol property)
        {
            foreach (var syntaxReference in property.DeclaringSyntaxReferences)
            {
                diagnostics.Add(Diagnostic.Create(
                    SkipPropertyWithoutGetterTemplate,
                    syntaxReference.GetSyntax().GetLocation()));
            }
        }

        private static readonly DiagnosticDescriptor SkipPropertyWithoutInitOrSetterTemplate = new(
            id: "OPTICS1005",
            title: "Skip property without init or setter",
            messageFormat: "No lens generated for property with neither init not setter",
            category: Category.CodeGeneration,
            defaultSeverity: DiagnosticSeverity.Info,
            isEnabledByDefault: false);
        public static void AddSkipPropertyWithoutInitOrSetter(this ResizeArray<Diagnostic> diagnostics, IPropertySymbol property)
        {
            foreach (var syntaxReference in property.DeclaringSyntaxReferences)
            {
                diagnostics.Add(Diagnostic.Create(
                    SkipPropertyWithoutInitOrSetterTemplate,
                    syntaxReference.GetSyntax().GetLocation()));
            }
        }

        private static readonly DiagnosticDescriptor SkipInaccessiblePropertyTemplate = new(
            id: "OPTICS1006",
            title: "Skip inaccessible property",
            messageFormat: "No lens generated for property with visibility '{0}'",
            category: Category.CodeGeneration,
            defaultSeverity: DiagnosticSeverity.Info,
            isEnabledByDefault: false);
        public static void AddSkipInaccessibleProperty(this ResizeArray<Diagnostic> diagnostics, IPropertySymbol property, string visibility)
        {
            foreach (var syntaxReference in property.DeclaringSyntaxReferences)
            {
                diagnostics.Add(Diagnostic.Create(
                    SkipInaccessiblePropertyTemplate,
                    syntaxReference.GetSyntax().GetLocation(),
                    visibility));
            }
        }

        private static readonly DiagnosticDescriptor SkipInaccessibleNestedRecordTemplate = new(
            id: "OPTICS1007",
            title: "Skip inaccessible nested record",
            messageFormat: "No lens generated for nested record type with visibility '{0}'",
            category: Category.CodeGeneration,
            defaultSeverity: DiagnosticSeverity.Warning,
            isEnabledByDefault: true);
        public static Diagnostic SkipInaccessibleNestedRecord(RecordDeclarationSyntax record, string accessibility)
        {
            return Diagnostic.Create(
                SkipInaccessibleNestedRecordTemplate,
                record.GetLocation(),
                accessibility);
        }

        #endregion

        #region 9999 - 9999 Unexpected Diagnostics

        private static readonly DiagnosticDescriptor UnexpectedTemplate = new(
            id: "OPTICS9999",
            title: "Unexpected Issue",
            messageFormat: "Encountered unexpected issue: {0}",
            category: Category.Unexpected,
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);
        public static Diagnostic Unexpected(SyntaxNode node, string message)
        {
            return Diagnostic.Create(
                UnexpectedTemplate,
                node.GetLocation(),
                message);
        }

        #endregion
    }
}