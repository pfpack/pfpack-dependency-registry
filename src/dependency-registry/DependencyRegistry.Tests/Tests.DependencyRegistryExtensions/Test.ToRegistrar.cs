using System;
using Moq;
using PrimeFuncPack.UnitTest;
using Xunit;
using static PrimeFuncPack.UnitTest.TestData;

namespace PrimeFuncPack.DependencyRegistry.Tests;

partial class DependencyRegistryExtensionsTest
{
    [Fact]
    public static void ToRegistrar_DependencyIsNull_ExpectArgumentNullException()
    {
        var mockServices = MockServiceCollection.CreateMock();
        var sourceServices = mockServices.Object;

        Dependency<RefType> dependency = null!;

        var ex = Assert.Throws<ArgumentNullException>(
            () => _ = dependency.ToRegistrar(sourceServices));

        Assert.Equal("dependency", ex.ParamName);
    }

    [Fact]
    public static void ToRegistrar_ServicesAreNull_ExpectArgumentNullException()
    {
        var dependency = Dependency.Of(MinusFifteenIdSomeStringNameRecord);

        var ex = Assert.Throws<ArgumentNullException>(
            () => _ = dependency.ToRegistrar(null!));

        Assert.Equal("services", ex.ParamName);
    }

    [Fact]
    public static void ToRegistrar_ArgsAreNotNull_ExpectSourceArgs()
    {
        var mockServices = MockServiceCollection.CreateMock();
        var sourceServices = mockServices.Object;

        var sourceService = MinusFifteenIdNullNameRecord;
        var dependency = Dependency.Of(sourceService);

        var actual = dependency.ToRegistrar(sourceServices);

        var actualServices = actual.GetInnerServices();
        Assert.Same(sourceServices, actualServices);

        var actualService = actual.GetInnerResolver()?.Invoke(Mock.Of<IServiceProvider>());
        Assert.Same(sourceService, actualService);
    }
}