using System;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace PrimeFuncPack.DependencyRegistry.Tests;

internal static class MockServiceCollection
{
    public static Mock<IServiceCollection> CreateMock(Action<ServiceDescriptor>? callback = null)
    {
        var mock = new Mock<IServiceCollection>();

        var m = mock.Setup(s => s.Add(It.IsAny<ServiceDescriptor>()));
        if (callback is object)
        {
            _ = m.Callback(callback);
        }

        return mock;
    }
}
