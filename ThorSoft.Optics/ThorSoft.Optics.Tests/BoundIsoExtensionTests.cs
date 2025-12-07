using FsCheck;
using FsCheck.Fluent;
using NSubstitute;

namespace ThorSoft.Optics.Tests
{
    public sealed class BoundIsoExtensionTests
    {
        [Fact]
        public void DirectMap_MapsBoundInstance()
        {
            var map = Substitute.For<Func<A, int>>();
            var instance = new A(0);
            var boundIso = new BoundIso<A, int>(
                new(map, _ => throw new NotImplementedException()),
                instance);

            _ = boundIso.DirectMap();

            map.Received(1).Invoke(instance);
        }

        [Fact]
        public void DirectMap_ReturnsMappedValue()
        {
            Prop
                .ForAll<int>(v =>
                {
                    var instance = new A(0);
                    var boundIso = new BoundIso<A, int>(
                        new(_ => v, _ => throw new NotImplementedException()),
                        instance);

                    var result = boundIso.DirectMap();

                    return result == v;
                })
                .QuickCheckThrowOnFailure();
        }

        private sealed record class A(int Value);
    }
}
