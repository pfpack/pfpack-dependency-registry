#nullable enable

using System;
using PrimeFuncPack.UnitTest;
using Xunit;
using static PrimeFuncPack.UnitTest.TestData;

namespace PrimeFuncPack.Tests
{
    partial class EightDependencyTest
    {
        [Theory]
        [MemberData(nameof(TestEntitySource.RecordTypes), MemberType = typeof(TestEntitySource))]
        public void ToRest_ExpectResolvedValueIsEqualToRestSource(
            RecordType restSource)
        {
            var source = Dependency.Create(
                _ => decimal.MaxValue,
                _ => new Tuple<int?, string?, string>(null, EmptyString, UpperSomeString),
                _ => Array.Empty<byte>(),
                _ => PlusFifteenIdSomeStringNameRecord,
                _ => new object(),
                _ => true,
                _ => SomeTextStructType,
                _ => restSource);

            var actual = source.ToRest();
            var actualValue = actual.Resolve();

            Assert.Equal(restSource, actualValue);
        }
    }
}