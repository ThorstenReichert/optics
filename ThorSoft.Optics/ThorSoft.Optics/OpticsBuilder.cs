namespace ThorSoft.Optics
{
    /// <summary>
    ///     Builder for composite optics used within the <see cref="Optics{TSource}" /> factory methods.
    /// </summary>
    /// <remarks>
    ///     Should only be used by generated code.
    /// </remarks>
    /// <typeparam name="TOptic">The type of the current optic being built so far.</typeparam>
    /// <param name="optic">The optic being built so far.</param>
    public readonly struct OpticsBuilder<TOptic>(TOptic optic)
    {
        /// <summary>
        ///     The optic being built so far.
        /// </summary>
        public TOptic Optic { get; } = optic;
    }
}
