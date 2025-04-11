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

        private static global::ThorSoft.Optics.Lens<global::Test.Module.A, global::System.Int32> 
            Lens_1 { get; } =
                new global::ThorSoft.Optics.Lens<global::Test.Module.A, global::Test.Module.B>(
                    static x => x.PropB,
                    static (v,x) => x with { PropB = v }
                )
                .Compose(
                    new global::ThorSoft.Optics.Lens<global::Test.Module.B, global::Test.Module.C>(
                        static x => x.PropC,
                        static (v,x) => x with { PropC = v }
                    )
                )
                .Compose(
                    new global::ThorSoft.Optics.Lens<global::Test.Module.C, global::System.Int32>(
                        static x => x.PropInt,
                        static (v,x) => x with { PropInt = v }
                    )
                )
                ;
        [global::System.Runtime.CompilerServices.InterceptsLocation(1, "<location>")] // (14,28)
        public static global::ThorSoft.Optics.Lens<global::Test.Module.A, global::System.Int32> 
            Focus_1(global::System.Linq.Expressions.Expression<global::System.Func<global::Test.Module.A, global::System.Int32>> _)
            {
                return Lens_1;
            }

    } // FocusInterceptors

} // ThorSoft.LensGenerator
