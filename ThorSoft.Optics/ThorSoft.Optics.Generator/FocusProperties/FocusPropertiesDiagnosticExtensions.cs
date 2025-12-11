using Microsoft.CodeAnalysis;
using ThorSoft.Optics;
using ThorSoft.Optics.Generator.Util;

namespace ThorSoft.Optics.Generator.FocusProperties
{
    internal static class FocusPropertiesDiagnosticExtensions
    {
        public static FocusPropertiesOutput AsOutput(this Diagnostic diagnostic) =>
            new()
            {
                Diagnostics = new EquatableMemory<Diagnostic>([diagnostic])
            };
    }
}
