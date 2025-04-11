using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Runtime.CompilerServices;
using ThorSoft.Optics.Generator.IntegrationTests.Test;
using ThorSoft.Optics.Generator.Tests.Generator;

namespace ThorSoft.Optics.Generator.Tests
{
    public sealed class BoundFocusTests
    {
        [Fact]
        public async Task Focus_InstanceMethod_TopLevelProperty()
        {
            var driver = BuildDriver("""
                using ThorSoft.Optics;

                namespace Test.Module;

                internal sealed record class TestRecord(int Prop);

                internal static class TestClass
                {
                    public static TestRecord TestMethod()
                    {
                        var instance = new TestRecord(5);
                        var lens = instance.Focus(x => x.Prop);

                        return lens.Set(10);
                    }
                }
                """);

            await Verify(driver).ScrubInterceptsLocation();
        }

        [Fact]
        public async Task Focus_InstanceMethod_NestedProperty()
        {
            var driver = BuildDriver("""
                using ThorSoft.Optics;

                namespace Test.Module;

                internal sealed record class A(B PropB);
                internal sealed record class B(C PropC);
                internal sealed record class C(int PropInt);

                internal static class TestClass
                {
                    public static A TestMethod()
                    {
                        var instance = new A(new B(new C(5)));
                        var lens = instance.Focus(x => x.PropB.PropC.PropInt);

                        return lens.Set(10);
                    }
                }
                """);

            await Verify(driver).ScrubInterceptsLocation();
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

            var generator = new FocusGenerator();
            var driver = CSharpGeneratorDriver.Create(generator);

            return driver.RunGenerators(compilation);
        }
    }
}
