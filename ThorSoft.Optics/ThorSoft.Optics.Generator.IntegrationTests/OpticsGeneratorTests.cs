using System.Numerics;
using ThorSoft.Optics.Tests.Specs;

namespace ThorSoft.Optics.Generator.IntegrationTests
{
    public sealed class OpticsGeneratorTests
    {
        [Fact]
        public void Focus_Unbound_IntegerProperty_UpdatesProperty()
        {
            const int value = 42;
            var instance = new TestRecord { IntProperty = -1, StringProperty = "abcd" };
            var lens = Optics<TestRecord>.Focus(r => r.IntProperty);

            Assert.Equal(instance with { IntProperty = value }, lens.Set(value, instance));
        }

        [Fact]
        public void Focus_Bound_IntegerProperty_UpdatesProperty()
        {
            const int value = 42;
            var instance = new TestRecord { IntProperty = -1, StringProperty = "abcd" };
            var lens = Optics<TestRecord>.Focus(instance, r => r.IntProperty);

            Assert.Equal(instance with { IntProperty = value }, lens.Set(value));
        }

        [Fact]
        public void Focus_Unbound_StringProperty_UpdatesProperty()
        {
            const string value = "xyz";
            var instance = new TestRecord { IntProperty = -1, StringProperty = "abcd" };
            var lens = Optics<TestRecord>.Focus(r => r.StringProperty);

            Assert.Equal(instance with { StringProperty = value }, lens.Set(value, instance));
        }

        [Fact]
        public void Focus_Bound_StringProperty_UpdatesProperty()
        {
            const string value = "xyz";
            var instance = new TestRecord { IntProperty = -1, StringProperty = "abcd" };
            var lens = Optics<TestRecord>.Focus(instance, r => r.StringProperty);

            Assert.Equal(instance with { StringProperty = value }, lens.Set(value));
        }

        [FocusProperties]
        public sealed record class TestRecord
        {
            public int IntProperty { get; init; }
            public int OtherProperty { get; init; }
            public required string StringProperty { get; init; }
        }

        [Fact]
        public void Focus_PublicProperty_SatisfiesLensSpec()
        {
            Lens<TestRecord_PublicProperty, int> lens = Optics<TestRecord_PublicProperty>.Focus(r => r.Property);

            LensSpec.IsSatisfied(lens);
        }

        [FocusProperties]
        public record class TestRecord_PublicProperty : IEqualityOperators<TestRecord_PublicProperty, TestRecord_PublicProperty, bool>
        {
            public int Property { get; init; }
        }

        [Fact]
        public void Focus_InternalProperty_SatisfiesLensSpec()
        {
            Lens<TestRecord_InternalProperty, int> lens = Optics<TestRecord_InternalProperty>.Focus(r => r.Property);

            LensSpec.IsSatisfied(lens);
        }

        [FocusProperties]
        public record class TestRecord_InternalProperty : IEqualityOperators<TestRecord_InternalProperty, TestRecord_InternalProperty, bool>
        {
            internal int Property { get; init; }
        }

        [Fact]
        public void Focus_ProtectedInternalProperty_SatisfiesLensSpec()
        {
            Lens<TestRecord_ProtectedInternalProperty, int> lens = Optics<TestRecord_ProtectedInternalProperty>.Focus(r => r.Property);

            LensSpec.IsSatisfied(lens);
        }

        [FocusProperties]
        public record class TestRecord_ProtectedInternalProperty : IEqualityOperators<TestRecord_ProtectedInternalProperty, TestRecord_ProtectedInternalProperty, bool>
        {
            protected internal int Property { get; init; }
        }

        [Fact]
        public void Focus_PropertyWithSetter_SatisfiesLensSpec()
        {
            Lens<TestRecord_PropertyWithSetter, int> lens = Optics<TestRecord_PropertyWithSetter>.Focus(r => r.Property);

            LensSpec.IsSatisfied(lens);
        }

        [FocusProperties]
        public record class TestRecord_PropertyWithSetter : IEqualityOperators<TestRecord_PropertyWithSetter, TestRecord_PropertyWithSetter, bool>
        {
            public int Property { get; set; }
        }

        [Fact]
        public void Focus_InternalRecordType_SatisfiesLensSpec()
        {
            Lens<TestRecord_InternalRecordType, int> lens = Optics<TestRecord_InternalRecordType>.Focus(r => r.Property);

            LensSpec.IsSatisfied(lens);
        }

        [FocusProperties]
        public record class TestRecord_InternalRecordType : IEqualityOperators<TestRecord_InternalRecordType, TestRecord_InternalRecordType, bool>
        {
            public int Property { get; set; }
        }

        [Fact]
        public void Focus_RequiredProperty_SatisfiesLensSpec()
        {
            Lens<TestRecord_RequiredProperty, int> lens = Optics<TestRecord_RequiredProperty>.Focus(r => r.Property);

            LensSpec.IsSatisfied(lens);
        }

        [FocusProperties]
        public record class TestRecord_RequiredProperty : IEqualityOperators<TestRecord_RequiredProperty, TestRecord_RequiredProperty, bool>
        {
            public required int Property { get; set; }
        }

        [Fact]
        public void Focus_ReferenceTypeProperty_SatisfiesLensSpec()
        {
            Lens<TestRecord_ReferenceTypeProperty, ReferenceType> lens = Optics<TestRecord_ReferenceTypeProperty>.Focus(r => r.Property);

            LensSpec.IsSatisfied(lens);
        }

        [FocusProperties]
        public record class TestRecord_ReferenceTypeProperty : IEqualityOperators<TestRecord_ReferenceTypeProperty, TestRecord_ReferenceTypeProperty, bool>
        {
            public required ReferenceType Property { get; set; }
        }

        [Fact]
        public void Focus_NestedType_SatisfiesLensSpec()
        {
            Lens<TestRecord_NestedType_OuterType.TestRecord_NestedType, ReferenceType> lens = Optics<TestRecord_NestedType_OuterType.TestRecord_NestedType>.Focus(r => r.Property);

            LensSpec.IsSatisfied(lens);
        }

        public sealed class TestRecord_NestedType_OuterType
        {
            [FocusProperties]
            public record class TestRecord_NestedType : IEqualityOperators<TestRecord_NestedType, TestRecord_NestedType, bool>
            {
                public required ReferenceType Property { get; set; }
            }
        }

        public sealed class ReferenceType : IEqualityOperators<ReferenceType, ReferenceType, bool>
        {
            public int Value { get; set; }
            public static bool operator ==(ReferenceType? left, ReferenceType? right) =>
                EqualityComparer<ReferenceType>.Default.Equals(left, right);
            public static bool operator !=(ReferenceType? left, ReferenceType? right) =>
                !(left == right);
            public override bool Equals(object? obj) =>
                obj is ReferenceType other && Value == other.Value;
            public override int GetHashCode() =>
                Value.GetHashCode();
        }
    }
}
