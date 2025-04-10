//HintName: LensGenerator.BoundFocusInterceptors.g.cs
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

namespace ThorSoft.LensGenerator
{
    
    using ThorSoft.Optics;

    static file class FocusInterceptors
    {

        private static global::ThorSoft.Optics.Lens<global::Test.Module.TestRecord, global::System.Int32> 
            Lens_1 { get; } =
                new global::ThorSoft.Optics.Lens<global::Test.Module.TestRecord, global::System.Int32>(
                    static x => x.Prop,
                    static (v,x) => x with { Prop = v }
                )
                ;
        [global::System.Runtime.CompilerServices.InterceptsLocation(1, "fyzgGxWiLe9/seFyaw4kBAcBAAA=")] // (12,29)
        public static global::ThorSoft.Optics.BoundLens<global::Test.Module.TestRecord, global::System.Int32> 
            Focus_1(this global::Test.Module.TestRecord source, global::System.Linq.Expressions.Expression<global::System.Func<global::Test.Module.TestRecord, global::System.Int32>> _)
            {
                return new global::ThorSoft.Optics.BoundLens<global::Test.Module.TestRecord, global::System.Int32>(
                    Lens_1,
                    source
                );
            }

    } // FocusInterceptors

} // ThorSoft.LensGenerator
