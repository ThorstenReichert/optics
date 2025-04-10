using Microsoft.CodeAnalysis;
using ThorSoft.Optics.Generator.Util;

namespace ThorSoft.Optics.Generator.Generation
{
    internal record class CodeGenerationRequest
    {
        public EquatableMemory<Diagnostic> Diagnostics { get; init; } = [];
    }
}
