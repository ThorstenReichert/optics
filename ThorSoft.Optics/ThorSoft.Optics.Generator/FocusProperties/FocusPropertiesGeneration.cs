using Microsoft.CodeAnalysis;
using System.Text;

namespace ThorSoft.Optics.Generator.FocusProperties
{
    internal static class FocusPropertiesGeneration
    {
        const string OpticsNamespace = "ThorSoft.Optics";
        const string BuilderName = $"global::{OpticsNamespace}.OpticsBuilder";
        const string IsoName = $"global::{OpticsNamespace}.Iso";
        const string LensName = $"global::{OpticsNamespace}.Lens";
        const string OptionalName = $"global::{OpticsNamespace}.Optional";
        const string BoundIsoName = $"global::{OpticsNamespace}.BoundIso";
        const string BoundLensName = $"global::{OpticsNamespace}.BoundLens";
        const string BoundOptionalName = $"global::{OpticsNamespace}.BoundOptional";

        public static void Invoke(SourceProductionContext context, FocusPropertiesRequest request)
        {
            foreach (var diagnostic in request.Diagnostics)
            {
                context.ReportDiagnostic(diagnostic);
            }

            if (request.TypeName is not { } typeName
                || request.Lenses.Count == 0)
            {
                return;
            }

            var builder = new StringBuilder();
            var content = CreateContent(builder, request);
            var filename = CreateFilename(builder, typeName);

            context.AddSource(filename, content);
        }

        private static string CreateFilename(this StringBuilder builder, string typeName)
        {
            builder.Clear();
            builder
                .Append(typeName)
                .Replace("global::", "")
                .Append("_Properties.g.cs")
                .Replace('<', '_')
                .Replace('>', '_')
                .Replace(',', '.')
                .Replace(' ', '_');

            return builder.ToString();
        }

        private static string CreateContent(this StringBuilder builder, FocusPropertiesRequest request)
        {
            // Construct class name for extensions on OpticsProxy.
            builder.Clear();

            builder
                .AppendFormat("{0}_Properties", request.TypeName)
                .Replace("global::", "")
                .Replace('<', '_')
                .Replace('>', '_')
                .Replace(',', '_')
                .Replace('.', '_')
                .Replace(' ', '_');

            var extensionClassName = builder.ToString();

            // Construct extensions class.
            builder.Clear();

            builder.Append($"""

                namespace {OpticsNamespace};

                """);

            builder.AppendFormat("""

                public static class {0}
                {{

                """, extensionClassName);

            foreach (var lens in request.Lenses)
            {
                builder.AppendFormat("""

                        #region {0}

                    """, lens.Name);

                builder.AppendFormat($$$"""

                        private static {{{LensName}}}<{0}, {1}> {2} {{ get; }} = new(
                            static x => x.{2},
                            static (v, x) => x with {{ {2} = v }});

                    """, request.TypeName, lens.Type, lens.Name);

                // Iso.
                builder.AppendFormat($$$"""

                        extension<T>({{{BuilderName}}}<{{{IsoName}}}<T, {0}>> proxy)
                        {{
                            {1} {{{BuilderName}}}<{{{LensName}}}<T, {2}>> {3} =>
                                proxy.Compose({3});
                        }}

                    """, request.TypeName, lens.Accessibility, lens.Type, lens.Name);

                // Lens.
                builder.AppendFormat($$$"""

                        extension<T>({{{BuilderName}}}<{{{LensName}}}<T, {0}>> proxy)
                        {{
                            {1} {{{BuilderName}}}<{{{LensName}}}<T, {2}>> {3} =>
                                proxy.Compose({3});
                        }}

                    """, request.TypeName, lens.Accessibility, lens.Type, lens.Name);

                // Optional.
                builder.AppendFormat($$$"""

                        extension<T>({{{BuilderName}}}<{{{OptionalName}}}<T, {0}>> proxy)
                        {{
                            {1} {{{BuilderName}}}<{{{OptionalName}}}<T, {2}>> {3} =>
                                proxy.Compose({3});
                        }}

                    """, request.TypeName, lens.Accessibility, lens.Type, lens.Name);

                // BoundIso.
                builder.AppendFormat($$$"""

                        extension<T>({{{BuilderName}}}<{{{BoundIsoName}}}<T, {0}>> proxy)
                        {{
                            {1} {{{BuilderName}}}<{{{BoundLensName}}}<T, {2}>> {3} =>
                                proxy.Compose({3});
                        }}

                    """, request.TypeName, lens.Accessibility, lens.Type, lens.Name);

                // BoundLens.
                builder.AppendFormat($$$"""

                        extension<T>({{{BuilderName}}}<{{{BoundLensName}}}<T, {0}>> proxy)
                        {{
                            {1} {{{BuilderName}}}<{{{BoundLensName}}}<T, {2}>> {3} =>
                                proxy.Compose({3});
                        }}

                    """, request.TypeName, lens.Accessibility, lens.Type, lens.Name);

                // BoundOptional.
                builder.AppendFormat($$$"""

                        extension<T>({{{BuilderName}}}<{{{BoundOptionalName}}}<T, {0}>> proxy)
                        {{
                            {1} {{{BuilderName}}}<{{{BoundOptionalName}}}<T, {2}>> {3} =>
                                proxy.Compose({3});
                        }}

                    """, request.TypeName, lens.Accessibility, lens.Type, lens.Name);

                builder.Append("""

                        #endregion

                    """);
            }

            builder.Append("""

                }

                """);


            return builder.ToString();
        }
    }
}
