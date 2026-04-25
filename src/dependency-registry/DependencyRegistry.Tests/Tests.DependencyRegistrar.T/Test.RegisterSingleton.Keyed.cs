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
    [InlineData(true)]
    [InlineData(false)]
    public static void RegisterKeyedSingleton_ServiceKeyIsNull_ExpectArgumentNullException(bool isNotNull)
    {
        var mockServices = MockServiceCollection.CreateMock();
        var sourceServices = mockServices.Object;

        object regService = isNotNull ? new object() : null!;
        var registrar = InnerCreateRegistrar(sourceServices, _ => regService);

        var ex = Assert.Throws<ArgumentNullException>(InnerTest);
        Assert.Equal("serviceKey", ex.ParamName);

        void InnerTest()
            =>
            registrar.RegisterKeyedSingleton(null!);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public static void RegisterKeyedSingleton_ServiceKeyIsNotNull_ExpectSourceServices(bool isNotNull)
    {
        var mockServices = MockServiceCollection.CreateMock();
        var sourceServices = mockServices.Object;

        object regService = isNotNull ? new object() : null!;
        var registrar = InnerCreateRegistrar(sourceServices, _ => regService);

        var actualServices = registrar.RegisterKeyedSingleton(new());
        Assert.Same(sourceServices, actualServices);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public static void RegisterKeyedSingleton_ServiceKeyIsNotNull_ExpectCallAddSingletonOnce(bool isNotNull)
    {
        RecordType regService = isNotNull ? PlusFifteenIdLowerSomeStringNameRecord : null!;
        var serviceKey = SomeString;

        var mockServices = MockServiceCollection.CreateMock(
            sd =>
            {
                Assert.True(sd.IsKeyedService);
                Assert.Equal(serviceKey, sd.ServiceKey);

                Assert.Equal(typeof(RecordType), sd.ServiceType);
                Assert.Equal(ServiceLifetime.Singleton, sd.Lifetime);
                Assert.NotNull(sd.KeyedImplementationFactory);

                var actualService = sd.KeyedImplementationFactory.Invoke(Mock.Of<IServiceProvider>(), serviceKey);
                Assert.Equal(regService, actualService);
            });

        var sourceServices = mockServices.Object;
        var registrar = InnerCreateRegistrar(sourceServices, _ => regService);

        _ = registrar.RegisterKeyedSingleton(serviceKey);
        mockServices.Verify(static s => s.Add(It.IsAny<ServiceDescriptor>()), Times.Once);
    }
}