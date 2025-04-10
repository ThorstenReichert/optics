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
        [global::System.Runtime.CompilerServices.InterceptsLocation(1, "NqMAw3OU+7Pir8Qvhy4PoA8BAAA=")] // (12,37)
        public static global::ThorSoft.Optics.Lens<global::Test.Module.TestRecord, global::System.Int32> 
            Focus_1(global::System.Linq.Expressions.Expression<global::System.Func<global::Test.Module.TestRecord, global::System.Int32>> _)
            {
                return Lens_1;
            }

    } // FocusInterceptors

} // ThorSoft.LensGenerator
