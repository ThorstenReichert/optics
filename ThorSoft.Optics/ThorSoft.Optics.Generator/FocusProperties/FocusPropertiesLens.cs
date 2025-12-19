namespace ThorSoft.Optics.Generator.FocusProperties
{
    internal sealed record class Lens
    {
        public required string Name { get; init; }
        public required string Type { get; init; }
        public required string Accessibility { get; init; }
    }
}
