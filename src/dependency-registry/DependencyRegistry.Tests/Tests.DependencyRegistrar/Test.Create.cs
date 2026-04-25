using System;
using PrimeFuncPack.UnitTest;
using Xunit;
using static PrimeFuncPack.UnitTest.TestData;

namespace PrimeFuncPack.DependencyRegistry.Tests;

partial class DependencyRegistrarTest
{
    [Fact]
    public static void Create_ServicesAreNull_ExpectArgumentNullException()
    {
        var ex = Assert.Throws<ArgumentNullException>(
            static () => _ = DependencyRegistrar.Create(null!, _ => PlusFifteenIdRefType));

        Assert.Equal("services", ex.ParamName);
    }

    [Fact]
    public static void Create_ResolverIsNull_ExpectArgumentNullException()
    {
        var mockServices = MockServiceCollection.CreateMock();
        var sourceServices = mockServices.Object;

        var ex = Assert.Throws<ArgumentNullException>(
            () => _ = DependencyRegistrar.Create<RecordType>(sourceServices, null!));

        Assert.Equal("resolver", ex.ParamName);
    }

    [Fact]
    public static void Create_ArgsAreNotNull_ExpectSourceArgs()
    {
        var mockServices = MockServiceCollection.CreateMock();
        var sourceServices = mockServices.Object;
        Func<IServiceProvider, RefType> sourceResolver = static _ => PlusFifteenIdRefType;

        var actual = DependencyRegistrar.Create(sourceServices, sourceResolver);

        var actualServices = actual.GetInnerServices();
        Assert.Same(sourceServices, actualServices);

        var actualResolver = actual.GetInnerResolver();
        Assert.Same(sourceResolver, actualResolver);
    }
}
