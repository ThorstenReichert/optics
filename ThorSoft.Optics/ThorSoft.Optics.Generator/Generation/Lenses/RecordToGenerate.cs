using ThorSoft.Optics.Generator.Util;

namespace ThorSoft.Optics.Generator.Generation.Lenses
{
    internal sealed record class RecordToGenerate : CodeGenerationRequest
    {
        public required string TypeName { get; init; }
        public required string TypeNamespace { get; init; }
        public required string TypeKind { get; init; }
        public required EquatableMemory<LensToGenerate> Properties { get; init; }
    }
}
