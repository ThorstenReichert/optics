using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Runtime.CompilerServices;
using ThorSoft.Optics.Generator.Tests.Generator;

namespace ThorSoft.Optics.Generator.Tests
{
    public sealed class DiagnosticTests
    {
        [Fact]
        public async Task SkipStaticProperty()
        {
            var driver = BuildDriver("""
                [ThorSoft.Optics.GenerateLenses]
                record class TestClass 
                { 
                    public static int Property { get; init; } 
                }
                """,
                ["LENSGEN0001"]);

            await Verify(driver);
        }

        [Fact]
        public async Task SkipPropertyWithoutGet()
        {
            var driver = BuildDriver("""
                [ThorSoft.Optics.GenerateLenses]
                record class TestClass 
                { 
                    public int Property { init; } 
                }
                """,
                ["LENSGEN0002"]);

            await Verify(driver);
        }

        [Fact]
        public async Task SkipPropertyWithoutInitOrSet()
        {
            var driver = BuildDriver("""
                [ThorSoft.Optics.GenerateLenses]
                record class TestClass 
                { 
                    public int Property { get; } 
                }
                """,
                ["LENSGEN0003"]);

            await Verify(driver);
        }

        [Fact]
        public async Task SkipUnsupportedTarget()
        {
            var driver = BuildDriver("""
                [ThorSoft.Optics.GenerateLenses]
                class TestClass 
                { 
                    public int Property { get; init; } 
                }
                """,
                []);

            await Verify(driver);
        }

        [Fact]
        public async Task FocusArgumentMustBeLambdaExpression_BoundFocus()
        {
            var driver = BuildDriver("""
                using ThorSoft.Optics;
                using System;
                using System.Linq.Expressions;

                internal sealed record class A(int Prop);

                public static class TestClass
                {
                    public static void TestMethod()
                    {
                        var instance = new A(5);
                        Expression<Func<A, int>> selector = c => c.Prop;
                        var lens = instance.Focus(selector);
                    }
                }
                """,
                []);

            await Verify(driver);
        }

        [Fact]
        public async Task FocusArgumentMustBeLambdaExpression_StaticFocus()
        {
            var driver = BuildDriver("""
                using ThorSoft.Optics;
                using System;
                using System.Linq.Expressions;

                internal sealed record class A(int Prop);

                public static class TestClass
                {
                    public static void TestMethod()
                    {
                        Expression<Func<A, int>> selector = c => c.Prop;
                        var lens = Lens<A>.Focus(selector);
                    }
                }
                """,
                []);

            await Verify(driver);
        }

        [Fact]
        public async Task UnrecognizedFocusMethod_BoundFocus()
        {
            var driver = BuildDriver("""
                internal sealed record class A(int Prop);

                public static class Extensions
                {
                    public static void Focus(this object obj, int value) { }
                }

                public static class TestClass
                {
                    public static void TestMethod()
                    {
                        var instance = new A(0);
                        instance.Focus(0);
                    }
                }
                """,
                ["LENSGEN1002"]);

            await Verify(driver);
        }

        [Fact]
        public async Task UnrecognizedFocusMethod_StaticFocus()
        {
            var driver = BuildDriver("""
                public static class NoLens
                {
                    public static void Focus(int value) { }
                }

                public static class TestClass
                {
                    public static void TestMethod()
                    {
                        NoLens.Focus(0);
                    }
                }
                """,
                ["LENSGEN1002"]);

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

            var driver = CSharpGeneratorDriver.Create(new LensGenerator(), new FocusGenerator());

            return driver.RunGenerators(compilation);
        }
    }
}
