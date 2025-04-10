using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace ThorSoft.Optics.Generator.Syntax
{
    internal static class CompilationExtensions
    {
        /// <summary>
        ///     Checks if LangVer of <paramref name="compilation"/> is high enough to support static lambda expressions.
        /// </summary>
        public static bool CanUseStaticAnonymousFunctions(this Compilation compilation) =>
            compilation is CSharpCompilation csharp
            && csharp.LanguageVersion >= LanguageVersion.CSharp8;
    }
}
