using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Runtime.CompilerServices;
using ThorSoft.Optics.Generator.Tests.Generator;

namespace ThorSoft.Optics.Generator.Tests
{
    public sealed class LensGeneratorTests
    {
        [Fact]
        public async Task RecordClass_PublicIntegerProperty()
        {
            var driver = BuildDriver("""
                using ThorSoft.Optics;

                namespace Test.Module;

                [GenerateLenses]
                public sealed partial record class TestClass
                {
                    public required int TestProperty { get; init; }
                }
                """);

            await Verify(driver);
        }

        [Fact]
        public async Task RecordStruct_PublicIntegerProperty()
        {
            var driver = BuildDriver("""
                using ThorSoft.Optics;

                namespace Test.Module;

                [GenerateLenses]
                public sealed partial record struct TestClass
                {
                    public required int TestProperty { get; init; }
                }
                """);

            await Verify(driver);
        }

        [Fact]
        public async Task RecordClass_PublicCustomTypeProperty()
        {
            var driver = BuildDriver("""

                namespace Test.OtherModule
                {
                    public class CustomType
                }

                namespace Test.Module
                {
                    using ThorSoft.Optics;
                    using Test.OtherModule;

                    [GenerateLenses]
                    public sealed partial record class TestClass
                    {
                        public required CustomType TestProperty { get; init; }
                    }
                }
                """);

            await Verify(driver);
        }

        [Fact]
        public async Task RecordStruct_PublicCustomTypeProperty()
        {
            var driver = BuildDriver("""

                namespace Test.OtherModule
                {
                    public class CustomType
                }

                namespace Test.Module
                {
                    using ThorSoft.Optics;
                    using Test.OtherModule;

                    [GenerateLenses]
                    public sealed partial record struct TestClass
                    {
                        public required CustomType TestProperty { get; init; }
                    }
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

            var generator = new LensGenerator();
            var driver = CSharpGeneratorDriver.Create(generator);

            return driver.RunGenerators(compilation);
        }
    }
}