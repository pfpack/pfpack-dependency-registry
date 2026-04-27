using System;
using Microsoft.Extensions.DependencyInjection;

namespace PrimeFuncPack;

partial class DependencyRegistrar<T>
{
    public IServiceCollection RegisterSingleton()
        =>
        services.AddSingleton(resolver);

    public IServiceCollection RegisterKeyedSingleton(object serviceKey)
    {
        ArgumentNullException.ThrowIfNull(serviceKey);

        return services.AddKeyedSingleton(serviceKey, InnerResolve);
    }
}
