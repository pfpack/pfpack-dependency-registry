using System;
using Microsoft.Extensions.DependencyInjection;

namespace PrimeFuncPack;

partial class DependencyRegistrar<T>
{
    public IServiceCollection RegisterScoped()
        =>
        services.AddScoped(resolver);

    public IServiceCollection RegisterKeyedScoped(object serviceKey)
    {
        ArgumentNullException.ThrowIfNull(serviceKey);
        return services.AddKeyedScoped(serviceKey, InnerResolve);

        T InnerResolve(IServiceProvider serviceProvider, object? _)
            =>
            resolver.Invoke(serviceProvider);
    }
}
