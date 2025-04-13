namespace ThorSoft.Optics
{
    /// <summary>
    ///     Attribute marking the target for generation of all property lenses.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public sealed class GenerateLensesAttribute : Attribute
    {
    }
}
