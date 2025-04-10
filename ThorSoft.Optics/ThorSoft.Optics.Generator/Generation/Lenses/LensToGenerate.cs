namespace ThorSoft.Optics.Generator.Generation.Lenses
{
    internal sealed record class LensToGenerate
    {
        public required string Name { get; init; }
        public required string Visibility { get; init; }
        public required string Type { get; init; }
    }
}
