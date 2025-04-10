namespace ThorSoft.Optics.Generator.Generation.Focus
{
    internal sealed record class PropertyAccess
    {
        /// <summary>
        ///     Name of the accessed property.
        /// </summary>
        public required string PropertyName { get; init; }

        /// <summary>
        ///     Type of the accessed property.
        /// </summary>
        public required string PropertyType { get; init; }

        /// <summary>
        ///     Type on which the property is defined.
        /// </summary>
        public required string DeclaringTypeName { get; init; }
    }
}
