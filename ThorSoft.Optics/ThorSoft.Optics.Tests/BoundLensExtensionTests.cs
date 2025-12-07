using FsCheck;
using FsCheck.Fluent;
using NSubstitute;
using static ThorSoft.Optics.Tests.BoundLensExtensionTests;

namespace ThorSoft.Optics.Tests
{
    public sealed class BoundLensExtensionTests
    {
        [Fact]
        public void Get_InvokesGetterOnBoundInstance()
        {
            Prop
                .ForAll<int>(v =>
                {
                    var get = Substitute.For<Func<A, int>>();
                    var instance = new A(v);
                    var boundLens = new BoundLens<A, int>(
                        new(get, (_, _) => throw new NotImplementedException()),
                        instance);

                    _ = boundLens.Get();

                    get.Received(1).Invoke(instance);

                })
                .QuickCheckThrowOnFailure();
        }

        [Fact]
        public void Get_ReturnsGetterValue()
        {
            Prop
                .ForAll<int>(v =>
                {
                    var instance = new A(0);
                    var boundLens = new BoundLens<A, int>(
                        new(_ => v, (_, _) => throw new NotImplementedException()),
                        instance);

                    var result = boundLens.Get();

                    return result == v;
                })
                .QuickCheckThrowOnFailure();
        }

        [Fact]
        public void Set_InvokesSetterOnBoundInstanceAndValue()
        {
            Prop
                .ForAll<int>(v =>
                {
                    var set = Substitute.For<Func<int, A, A>>();
                    var instance = new A(0);
                    var boundLens = new BoundLens<A, int>(
                        new(_ => throw new NotImplementedException(), set),
                        instance);

                    _ = boundLens.Set(v);

                    set.Received(1).Invoke(v, instance);
                })
                .QuickCheckThrowOnFailure();
        }

        [Fact]
        public void Set_ReturnsSetterValue()
        {
            Prop
                .ForAll<int, int>((initialValue, newValue) =>
                {
                    var instance = new A(initialValue);
                    var boundLens = new BoundLens<A, int>(
                        new(_ => throw new NotImplementedException(), (_, _) => new A(newValue)),
                        instance);

                    var result = boundLens.Set(newValue);

                    return result.Value == newValue;
                })
                .QuickCheckThrowOnFailure();
        }

        public sealed record class A(int Value);
    }
}
