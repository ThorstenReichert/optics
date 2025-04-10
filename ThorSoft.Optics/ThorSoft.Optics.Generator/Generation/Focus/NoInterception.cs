using Microsoft.CodeAnalysis;

namespace ThorSoft.Optics.Generator.Generation.Focus
{
    internal sealed record class NoInterception : CodeGenerationRequest
    {
        public NoInterception()
        {
        }

        public NoInterception(Diagnostic diagnostic)
        {
            Diagnostics = new([diagnostic]);
        }
    }
}
