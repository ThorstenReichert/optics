using System.Linq.Expressions;
using System.Text;
using ThorSoft.Optics.Generator.Generation.Focus;
using ThorSoft.Optics.Generator.Generation.Lenses;
using ThorSoft.Optics.Generator.Options;
using ThorSoft.Optics.Generator.Syntax;
using ThorSoft.Optics.Generator.Util;

namespace ThorSoft.Optics.Generator.Generation
{
    internal static class SourceGenerationHelper
    {
        internal static readonly string OpticsNamespace = typeof(LensExtensions).Namespace;
        internal static readonly string LensTypeName = typeof(Lens<,>).GetFullNameWithoutGenericArity();
        internal static readonly string BoundLensTypeName = typeof(BoundLens<,>).GetFullNameWithoutGenericArity();
        internal static readonly string ExpressionTypeName = typeof(Expression<>).GetFullNameWithoutGenericArity();
        internal static readonly string FuncTypeName = typeof(Func<,>).GetFullNameWithoutGenericArity();

        internal static string InterceptsLocationAttribute { get; } =
            """
            namespace System.Runtime.CompilerServices
            {
                [global::System.Diagnostics.Conditional("DEBUG")]
                [global::System.AttributeUsage(global::System.AttributeTargets.Method, AllowMultiple = true)]
                sealed file class InterceptsLocationAttribute : global::System.Attribute
                {
                    public InterceptsLocationAttribute(int version, string data)
                    {
                        _ = version;
                        _ = data;
                    }
                }
            }
            """;

        internal static string? GenerateFocusInterceptions(IEnumerable<CodeGenerationRequest> interceptions, CodeGenerationOptions options)
        {
            const int Version = 1;
            const string LensGeneratorNamespace = "ThorSoft.LensGenerator";

            static string GenerateLensPropertyName(ulong id) =>
                    $"Lens_{id}";

            if (!interceptions.Any(x => x is BoundFocusInterception or StaticFocusInterception))
            {
                return null;
            }



            var sourceText = new StringBuilder(InterceptsLocationAttribute.Length);

            sourceText.AppendLine(InterceptsLocationAttribute);
            sourceText.AppendLine();

            sourceText.AppendLine(
                $$"""
                namespace {{LensGeneratorNamespace}}
                {
                    
                    using {{OpticsNamespace}};

                    static file class FocusInterceptors
                    {
                """);

            static void AppendLensProperty(StringBuilder sourceText, ulong id, EquatableMemory<PropertyAccess> focusPath, CodeGenerationOptions options)
            {
                var staticPrefix = options.UseStaticLambdas ? "static " : "";

                var firstPropertyAccess = focusPath.First();
                var lastPropertyAccess = focusPath.Last();

                var totalLensSourceType = firstPropertyAccess.DeclaringTypeName;
                var totalLensTargetType = lastPropertyAccess.PropertyType;

                var baseLensSource = firstPropertyAccess.DeclaringTypeName;
                var baseLensTarget = firstPropertyAccess.PropertyType;
                var baseLensProperty = firstPropertyAccess.PropertyName;

                sourceText.AppendLine(
                $$"""

                        private static global::{{LensTypeName}}<global::{{totalLensSourceType}}, global::{{totalLensTargetType}}> 
                            {{GenerateLensPropertyName(id)}} { get; } =
                                new global::{{LensTypeName}}<global::{{baseLensSource}}, global::{{baseLensTarget}}>(
                                    {{staticPrefix}}x => x.{{baseLensProperty}},
                                    {{staticPrefix}}(v,x) => x with { {{baseLensProperty}} = v }
                                )
                """);

                foreach (var propertyAccess in focusPath.Skip(1))
                {
                    var nextLensSource = propertyAccess.DeclaringTypeName;
                    var nextLensTarget = propertyAccess.PropertyType;
                    var nextLensProperty = propertyAccess.PropertyName;

                    sourceText.AppendLine(
                $$"""
                                .Compose(
                                    new global::{{LensTypeName}}<global::{{nextLensSource}}, global::{{nextLensTarget}}>(
                                        {{staticPrefix}}x => x.{{nextLensProperty}},
                                        {{staticPrefix}}(v,x) => x with { {{nextLensProperty}} = v }
                                    )
                                )
                """);
                }

                sourceText.AppendLine(
                """
                                ;
                """);
            }

            // Multiple Focus() calls with same type signatures will generate multiple interceptors,
            // hence we need a schema to distinguish between the generated methods.
            // The simplest option for now is to append a counter to each generated method,
            // making all generated method names unique.
            ulong interceptionCounter = 0L;

            foreach (var interception in interceptions)
            {
                var interceptionNonce = ++interceptionCounter;

                if (interception is BoundFocusInterception boundFocusInterception)
                {
                    var interceptionLocation = boundFocusInterception.Location.Data;
                    var interceptionDisplay = boundFocusInterception.Location.GetDisplayLocation();
                    var lensTargetType = boundFocusInterception.FocusPath.Last().PropertyType;
                    var lensSourceType = boundFocusInterception.OriginType;

                    // Generate a static property caching the required Lens instance.
                    AppendLensProperty(sourceText, interceptionNonce, boundFocusInterception.FocusPath, options);

                    // Generate the method call intercepting the requested location and returing a 
                    // bound lens instance from the cached lens.
                    sourceText.AppendLine(
                $$"""
                        [global::System.Runtime.CompilerServices.InterceptsLocation({{Version}}, "{{interceptionLocation}}")] // {{interceptionDisplay}}
                        public static global::{{BoundLensTypeName}}<global::{{lensSourceType}}, global::{{lensTargetType}}> 
                            Focus_{{interceptionNonce}}(this global::{{lensSourceType}} source, global::{{ExpressionTypeName}}<global::{{FuncTypeName}}<global::{{lensSourceType}}, global::{{lensTargetType}}>> _)
                            {
                                return new global::{{BoundLensTypeName}}<global::{{lensSourceType}}, global::{{lensTargetType}}>(
                                    {{GenerateLensPropertyName(interceptionNonce)}},
                                    source
                                );
                            }
                """);
                }
                else if (interception is StaticFocusInterception staticFocusInterception)
                {
                    var interceptionLocation = staticFocusInterception.Location.Data;
                    var interceptionDisplay = staticFocusInterception.Location.GetDisplayLocation();
                    var lensTargetType = staticFocusInterception.FocusPath.Last().PropertyType;
                    var lensSourceType = staticFocusInterception.OriginType;

                    // Generate a static property caching the required Lens instance.
                    AppendLensProperty(sourceText, interceptionNonce, staticFocusInterception.FocusPath, options);

                    // Generate the method call intercepting the requested location and returning the cached lens.
                    sourceText.AppendLine(
                $$"""
                        [global::System.Runtime.CompilerServices.InterceptsLocation({{Version}}, "{{interceptionLocation}}")] // {{interceptionDisplay}}
                        public static global::{{LensTypeName}}<global::{{lensSourceType}}, global::{{lensTargetType}}> 
                            Focus_{{interceptionNonce}}(global::{{ExpressionTypeName}}<global::{{FuncTypeName}}<global::{{lensSourceType}}, global::{{lensTargetType}}>> _)
                            {
                                return {{GenerateLensPropertyName(interceptionNonce)}};
                            }
                """);
                }
                else
                {
                    continue;
                }
            }

            sourceText.AppendLine(
                $$"""

                    } // FocusInterceptors

                } // {{LensGeneratorNamespace}}
                """);

            return sourceText.ToString();
        }

        internal static string? GenerateLenses(RecordToGenerate type, CodeGenerationOptions options)
        {
            if (type.Properties.Count == 0)
            {
                return null;
            }

            var source = new StringBuilder();
            var staticPrefix = options.UseStaticLambdas ? "static " : "";

            source.AppendLine(
                $$"""
                namespace {{type.TypeNamespace}};

                partial record {{type.TypeKind}} {{type.TypeName}}
                {

                    public static class Lenses
                    {
                """);

            foreach (var property in type.Properties)
            {
                var lensType = $"global::{LensTypeName}<{type.TypeName}, {property.Type}>";

                source.AppendLine(
                $$"""

                        {{property.Visibility}} static {{lensType}} {{property.Name}} { get; } =
                            new {{lensType}}(
                                {{staticPrefix}}(instance) => instance.{{property.Name}},
                                {{staticPrefix}}(value, instance) => instance with { {{property.Name}} = value });
                """);
            }

            source.AppendLine(
                $$"""

                    } // Lenses

                } // {{type.TypeName}}
                """);

            return source.ToString();
        }
    }
}
