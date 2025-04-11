using System.Text.RegularExpressions;

namespace ThorSoft.Optics.Generator.IntegrationTests.Test
{
    internal static partial class VerifyExtensions
    {
        /// <summary>
        ///     Replaces location data in generated intercepts location attributes with a stable value.
        /// </summary>
        /// <remarks>
        ///     Location data is not stable across different machine configuration (e.g. Linux/Windows),
        ///     which might lead to unexpected CI failures.
        /// </remarks>
        public static SettingsTask ScrubInterceptsLocation(this SettingsTask settings)
        {
            return settings.ScrubLinesWithReplace(line =>
            {
                var match = InterceptsLocationRegex().Match(line);
                if (match.Success)
                {
                    return line.Replace(match.Groups[1].Value, "<location>");
                }

                return line;
            });
        }

        [GeneratedRegex(@"\[(?:global::)?System\.Runtime\.CompilerServices\.InterceptsLocation\(\d,\s?""([a-zA-Z=\d\/\+]+)""\)\]")]
        private static partial Regex InterceptsLocationRegex();
    }
}
