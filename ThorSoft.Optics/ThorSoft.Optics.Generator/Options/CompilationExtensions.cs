using Microsoft.CodeAnalysis;
using ThorSoft.Optics.Generator.Syntax;

namespace ThorSoft.Optics.Generator.Options
{
    internal static class CompilationExtensions
    {
        public static CodeGenerationOptions GetCodeGenerationOptions(this Compilation compilation)
        {
            return new CodeGenerationOptions
            {
                UseStaticLambdas = compilation.CanUseStaticAnonymousFunctions()
            };
        }
    }
}
