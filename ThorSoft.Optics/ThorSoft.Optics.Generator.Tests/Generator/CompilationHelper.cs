using Microsoft.CodeAnalysis;

namespace ThorSoft.Optics.Generator.Tests.Generator
{
    internal static class CompilationHelper
    {
        //public static PortableExecutableReference[] References =>
        //[
        //    MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
        //    MetadataReference.CreateFromFile(typeof(GenerateLensesAttribute).Assembly.Location),
        //    MetadataReference.CreateFromFile(typeof(ValueType).Assembly.Location)
        //];

        public static PortableExecutableReference[] References =>
        [
            .. AppDomain.CurrentDomain.GetAssemblies()
                .Where(assembly => !assembly.IsDynamic && !string.IsNullOrWhiteSpace(assembly.Location))
                .Select(assembly => MetadataReference.CreateFromFile(assembly.Location))
,
            MetadataReference.CreateFromFile(typeof(LensGenerator).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(GenerateLensesAttribute).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(Func<,>).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(System.ComponentModel.DataAnnotations.DisplayAttribute).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(System.CodeDom.Compiler.GeneratedCodeAttribute).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(System.Linq.Expressions.Expression).Assembly.Location),
        ];
    }
}
