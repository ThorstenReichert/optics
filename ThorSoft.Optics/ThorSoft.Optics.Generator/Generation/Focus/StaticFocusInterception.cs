using Microsoft.CodeAnalysis.CSharp;
using ThorSoft.Optics.Generator.Util;

namespace ThorSoft.Optics.Generator.Generation.Focus
{
    internal sealed record class StaticFocusInterception : CodeGenerationRequest
    {
        public required InterceptableLocation Location { get; init; }
        public required string OriginType { get; init; }
        public required EquatableMemory<PropertyAccess> FocusPath { get; init; }
    }
}
