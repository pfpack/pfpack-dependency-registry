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
    public static void RegisterKeyedTransient_ServiceKeyIsNull_ExpectArgumentNullException(bool isNotNull)
    {
        var mockServices = MockServiceCollection.CreateMock();
        var sourceServices = mockServices.Object;

        RecordType regService = isNotNull ? PlusFifteenIdSomeStringNameRecord : null!;
        var registrar = InnerCreateRegistrar(sourceServices, _ => regService);

        var ex = Assert.Throws<ArgumentNullException>(InnerTest);
        Assert.Equal("serviceKey", ex.ParamName);

        void InnerTest()
            =>
            registrar.RegisterKeyedTransient(null!);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public static void RegisterKeyedTransient_ServiceKeyIsNotNull_ExpectSourceServices(bool isNotNull)
    {
        var mockServices = MockServiceCollection.CreateMock();
        var sourceServices = mockServices.Object;

        RecordType regService = isNotNull ? PlusFifteenIdSomeStringNameRecord : null!;
        var registrar = InnerCreateRegistrar(sourceServices, _ => regService);

        var actualServices = registrar.RegisterKeyedTransient(new());
        Assert.Same(sourceServices, actualServices);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public static void RegisterKeyedTransient_ServiceKeyIsNotNull_ExpectCallAddTransientOnce(bool isNotNull)
    {
        RefType regService = isNotNull ? MinusFifteenIdRefType : null!;
        var serviceKey = SomeString;

        var mockServices = MockServiceCollection.CreateMock(
            sd =>
            {
                Assert.True(sd.IsKeyedService);
                Assert.Equal(serviceKey, sd.ServiceKey);

                Assert.Equal(typeof(RefType), sd.ServiceType);
                Assert.Equal(ServiceLifetime.Transient, sd.Lifetime);
                Assert.NotNull(sd.KeyedImplementationFactory);

                var actualService = sd.KeyedImplementationFactory.Invoke(Mock.Of<IServiceProvider>(), serviceKey);
                Assert.Equal(regService, actualService);
            });

        var sourceServices = mockServices.Object;
        var registrar = InnerCreateRegistrar(sourceServices, _ => regService);

        _ = registrar.RegisterKeyedTransient(serviceKey);
        mockServices.Verify(static s => s.Add(It.IsAny<ServiceDescriptor>()), Times.Once);
    }
}