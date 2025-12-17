using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Runtime.CompilerServices;
using ThorSoft.Optics.Generator.Tests.Generator;

namespace ThorSoft.Optics.Generator.Tests
{
    public sealed class DiagnosticTests
    {
        [Fact]
        public async Task MustBeRecordType()
        {
            var driver = BuildDriver("""
                [ThorSoft.Optics.FocusProperties]
                partial class TestClass 
                { 
                    public int Property { get; init; } 
                }
                """,
                ["OPTICS1001"]);

            await Verify(driver);
        }

        [Fact]
        public async Task NoLensesToGenerate()
        {
            var driver = BuildDriver("""
                [ThorSoft.Optics.FocusProperties]
                partial record class TestClass 
                { 
                    public static int Property { get; init; } 
                }
                """,
                ["OPTICS1002"]);

            await Verify(driver);
        }

        private static GeneratorDriver BuildDriver(string sourceText, IEnumerable<string> enabledDiagnostics, [CallerMemberName] string caller = null!)
        {
            var options = new CSharpCompilationOptions(
                OutputKind.ConsoleApplication,
                specificDiagnosticOptions: enabledDiagnostics
                    .Select(id => new KeyValuePair<string, ReportDiagnostic>(id, ReportDiagnostic.Default)));

            var compilation = CSharpCompilation.Create(
                $"{caller}.Test",
                [CSharpSyntaxTree.ParseText(sourceText)],
                CompilationHelper.References,
                options);

            var driver = CSharpGeneratorDriver.Create(new OpticsGenerator());

            return driver.RunGenerators(compilation);
        }
    }
}
