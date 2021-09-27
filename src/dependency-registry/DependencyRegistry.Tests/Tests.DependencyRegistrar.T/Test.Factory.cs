using System;
using PrimeFuncPack.UnitTest;
using Xunit;
using static PrimeFuncPack.UnitTest.TestData;

namespace PrimeFuncPack.DependencyRegistry.Tests;

partial class DependencyRegistrarTypedTest
{
    [Fact]
    public void Create_ServicesAreNull_ExpectArgumentNullException()
    {
        var ex = Assert.Throws<ArgumentNullException>(
            () => _ = DependencyRegistrar<RefType>.Create(null!, _ => PlusFifteenIdRefType));

        Assert.Equal("services", ex.ParamName);
    }

    [Fact]
    public void Create_ResolverIsNull_ExpectArgumentNullException()
    {
        var mockServices = MockServiceCollection.CreateMock();
        var sourceServices = mockServices.Object;

        var ex = Assert.Throws<ArgumentNullException>(
            () => _ = DependencyRegistrar<RecordType>.Create(sourceServices, null!));

        Assert.Equal("resolver", ex.ParamName);
    }
}
