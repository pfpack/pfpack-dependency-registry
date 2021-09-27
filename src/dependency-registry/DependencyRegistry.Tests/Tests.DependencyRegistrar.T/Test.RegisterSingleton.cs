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
    public void RegisterSingleton_ExpectSourceServices(bool isNotNull)
    {
        var mockServices = MockServiceCollection.CreateMock();
        var sourceServices = mockServices.Object;

        object regService = isNotNull ? new object() : null!;
        var registrar = DependencyRegistrar<object>.Create(sourceServices, _ => regService);

        var actualServices = registrar.RegisterSingleton();
        Assert.Same(sourceServices, actualServices);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void RegisterSingleton_ExpectCallAddSingletonOnce(bool isNotNull)
    {
        RecordType regService = isNotNull ? PlusFifteenIdLowerSomeStringNameRecord : null!;
        var mockServices = MockServiceCollection.CreateMock(
            sd =>
            {
                Assert.Equal(typeof(RecordType), sd.ServiceType);
                Assert.Equal(ServiceLifetime.Singleton, sd.Lifetime);
                Assert.NotNull(sd.ImplementationFactory);

                var actualService = sd.ImplementationFactory!.Invoke(Mock.Of<IServiceProvider>());
                Assert.Equal(regService, actualService);
            });

        var sourceServices = mockServices.Object;
        var registrar = DependencyRegistrar<RecordType>.Create(sourceServices, _ => regService);

        _ = registrar.RegisterSingleton();
        mockServices.Verify(s => s.Add(It.IsAny<ServiceDescriptor>()), Times.Once);
    }
}
