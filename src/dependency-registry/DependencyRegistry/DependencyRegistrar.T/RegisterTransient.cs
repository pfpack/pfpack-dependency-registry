using System;
using Microsoft.Extensions.DependencyInjection;

namespace PrimeFuncPack;

partial class DependencyRegistrar<T>
{
    public IServiceCollection RegisterTransient()
        =>
        services.AddTransient(resolver);

    public IServiceCollection RegisterKeyedTransient(object serviceKey)
    {
        ArgumentNullException.ThrowIfNull(serviceKey);
        return services.AddKeyedTransient(serviceKey, InnerResolve);

        T InnerResolve(IServiceProvider serviceProvider, object? _)
            =>
            resolver.Invoke(serviceProvider);
    }
}
