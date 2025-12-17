using System.Numerics;
using ThorSoft.Optics.Tests.Specs;

namespace ThorSoft.Optics.Generator.IntegrationTests
{
    public sealed class OpticsGeneratorTests
    {
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
            public int Property { get; init; }
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
    }
}
