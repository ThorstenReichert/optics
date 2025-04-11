using Microsoft.CodeAnalysis.CSharp;
using ThorSoft.Optics.Generator.Util;

namespace ThorSoft.Optics.Generator.Generation.Focus
{
    internal sealed record class StaticFocusInterception : CodeGenerationRequest
    {
#pragma warning disable RSEXPERIMENTAL002 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        public required InterceptableLocation Location { get; init; }
#pragma warning restore RSEXPERIMENTAL002 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        public required string OriginType { get; init; }
        public required EquatableMemory<PropertyAccess> FocusPath { get; init; }
    }
}
