using System;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using PrimeFuncPack.UnitTest;
using Xunit;
using static PrimeFuncPack.UnitTest.TestData;

namespace PrimeFuncPack.DependencyRegistry.Tests;

partial class DependencyRegistrarTypedTest
{
    [Theory]
    [InlineData(null)]
    [InlineData(SomeString)]
    public static void RegisterKeyedScoped_ServiceKeyIsNull_ExpectArgumentNullException(string? regService)
    {
        var mockServices = MockServiceCollection.CreateMock();
        var sourceServices = mockServices.Object;

        var registrar = InnerCreateRegistrar(sourceServices, _ => regService!);

        var ex = Assert.Throws<ArgumentNullException>(InnerTest);
        Assert.Equal("serviceKey", ex.ParamName);

        void InnerTest()
            =>
            registrar.RegisterKeyedScoped(null!);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(SomeString)]
    public static void RegisterKeyedScoped_ServiceKeyIsNotNull_ExpectSourceServices(string? regService)
    {
        var mockServices = MockServiceCollection.CreateMock();
        var sourceServices = mockServices.Object;

        var registrar = InnerCreateRegistrar(sourceServices, _ => regService!);

        var actualServices = registrar.RegisterKeyedScoped(new());
        Assert.Same(sourceServices, actualServices);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public static void RegisterKeyedScoped_ServiceKeyIsNotNull_ExpectCallAddScopedOnce(bool isNotNull)
    {
        RefType regService = isNotNull ? ZeroIdRefType : null!;
        var serviceKey = SomeString;

        var mockServices = MockServiceCollection.CreateMock(
            sd =>
            {
                Assert.True(sd.IsKeyedService);
                Assert.Equal(serviceKey, sd.ServiceKey);

                Assert.Equal(typeof(RefType), sd.ServiceType);
                Assert.Equal(ServiceLifetime.Scoped, sd.Lifetime);
                Assert.NotNull(sd.KeyedImplementationFactory);

                var actualService = sd.KeyedImplementationFactory.Invoke(Mock.Of<IServiceProvider>(), serviceKey);
                Assert.Equal(regService, actualService);
            });

        var sourceServices = mockServices.Object;
        var registrar = InnerCreateRegistrar(sourceServices, _ => regService);

        _ = registrar.RegisterKeyedScoped(serviceKey);
        mockServices.Verify(static s => s.Add(It.IsAny<ServiceDescriptor>()), Times.Once);
    }
}