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

        [Fact]
        public async Task RecordClass_PrimaryConstructorWithSingleParameter()
        {
            var driver = BuildDriver("""
                namespace Test.Module
                {
                    using ThorSoft.Optics;

                    [GenerateLenses]
                    public sealed partial record class TestClass(int Property);
                }
                """);

            await Verify(driver);
        }

        [Fact]
        public async Task RecordClass_PrimaryConstructorWithManyParameter()
        {
            var driver = BuildDriver("""
                namespace Test.Module
                {
                    using ThorSoft.Optics;

                    [GenerateLenses]
                    public sealed partial record class TestClass(
                        int Property1, 
                        string Property2,
                        object Propety3);
                }
                """);

            await Verify(driver);
        }

        [Fact]
        public async Task RecordClass_PrimaryConstructorAndProperties()
        {
            var driver = BuildDriver("""
                namespace Test.Module
                {
                    using ThorSoft.Optics;

                    [GenerateLenses]
                    public sealed partial record class TestClass(
                        int Property1, 
                        string Property2,
                        object Propety3)
                    {
                        public int DeclaredProperty1 { get; init; }
                    }
                }
                """);

            await Verify(driver);
        }

        [Fact]
        public async Task RecordStruct_PrimaryConstructorWithSingleParameter()
        {
            var driver = BuildDriver("""
                namespace Test.Module
                {
                    using ThorSoft.Optics;

                    [GenerateLenses]
                    public readonly partial record struct TestStruct(int Property);
                }
                """);

            await Verify(driver);
        }

        [Fact]
        public async Task RecordStruct_PrimaryConstructorWithManyParameter()
        {
            var driver = BuildDriver("""
                namespace Test.Module
                {
                    using ThorSoft.Optics;

                    [GenerateLenses]
                    public readonly partial record struct TestStruct(
                        int Property1, 
                        string Property2,
                        object Propety3);
                }
                """);

            await Verify(driver);
        }

        [Fact]
        public async Task RecordStruct_PrimaryConstructorAndProperties()
        {
            var driver = BuildDriver("""
                namespace Test.Module
                {
                    using ThorSoft.Optics;

                    [GenerateLenses]
                    public readonly partial record struct TestStruct(
                        int Property1, 
                        string Property2,
                        object Propety3)
                    {
                        public int DeclaredProperty1 { get; init; }
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