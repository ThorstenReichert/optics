using Microsoft.CodeAnalysis;
using ThorSoft.Optics.Generator.Util;

namespace ThorSoft.Optics.Generator.FocusProperties
{
    internal sealed record class FocusPropertiesRequest
    {
        public string? TypeName { get; init; }
        public string? TypeKind { get; init; }

        public EquatableMemory<Diagnostic> Diagnostics { get; init; }
        public EquatableMemory<Lens> Lenses { get; init; }
    }
}
