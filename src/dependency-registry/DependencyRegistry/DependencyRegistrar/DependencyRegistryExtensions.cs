using Microsoft.Extensions.DependencyInjection;
using System;

namespace PrimeFuncPack;

public static class DependencyRegistryExtensions
{
    public static DependencyRegistrar<T> ToRegistrar<T>(this Dependency<T> dependency, IServiceCollection services)
        where T : class
    {
        ArgumentNullException.ThrowIfNull(dependency);
        ArgumentNullException.ThrowIfNull(services);

        return new(services, dependency.Resolve);
    }
}
