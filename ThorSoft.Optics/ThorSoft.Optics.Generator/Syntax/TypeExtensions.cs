namespace ThorSoft.Optics.Generator.Syntax
{
    internal static class TypeExtensions
    {
        /// <summary>
        ///     Returns the full name of the type <paramref name="type"/> without generic type information.
        ///     E.g. <c>IEnumerable</c> would return <c>System.Collections.Generic.IEnumerable</c> instead of
        ///     <c>IEnumerable`1</c>.
        /// </summary>
        public static string GetFullNameWithoutGenericArity(this Type type)
        {
            var fullName = type.FullName;
            var arityIndex = fullName.IndexOf('`');

            return arityIndex < 0
                ? fullName
                : fullName.Substring(0, arityIndex);
        }
    }
}
