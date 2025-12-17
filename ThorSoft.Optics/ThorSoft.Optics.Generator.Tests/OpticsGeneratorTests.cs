using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Runtime.CompilerServices;
using ThorSoft.Optics.Generator.Tests.Generator;

namespace ThorSoft.Optics.Generator.Tests
{
    public sealed class OpticsGeneratorTests
    {
        [Fact]
        public async Task RecordClass_PublicIntegerProperty()
        {
            var driver = BuildDriver("""
                using ThorSoft.Optics;

                namespace Test.Namespace;

                [FocusProperties]
                public sealed partial record class TestClass
                {
                    public required int TestProperty { get; init; }
                }
                """);

            await Verify(driver);
        }

        private static GeneratorDriver BuildDriver(string? sourceText = null, [CallerMemberName] string caller = null!)
        {
            List<SyntaxTree> syntaxTrees = [];
            if (sourceText is string text)
            {
                syntaxTrees.Add(CSharpSyntaxTree.ParseText(text));
            }

            var compilation = CSharpCompilation.Create(
                $"{caller}.Test",
                syntaxTrees,
                CompilationHelper.References);

            var generator = new OpticsGenerator();
            var driver = CSharpGeneratorDriver.Create(generator);

            return driver.RunGenerators(compilation);
        }
    }
}
