using FsCheck;
using FsCheck.Fluent;

namespace ThorSoft.Optics.Tests
{
    public sealed class IsoTests
    {
        [Fact]
        public void Identity_DirectMapIsIdentity()
        {
            Prop
                .ForAll<int>(v =>
                {
                    var identityIso = Iso.Identity<int>();
                    var mapped = identityIso.DirectMap(v);

                    return mapped == v;
                })
                .QuickCheckThrowOnFailure();
        }

        [Fact]
        public void Identity_InverseMapIsIdentity()
        {
            Prop
                .ForAll<int>(v =>
                {
                    var identityIso = Iso.Identity<int>();
                    var mapped = identityIso.InverseMap(v);

                    return mapped == v;
                })
                .QuickCheckThrowOnFailure();
        }
    }
}
