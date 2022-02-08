using Microsoft.Extensions.DependencyInjection;
using System;

namespace PrimeFuncPack;

public static class DependencyRegistryExtensions
{
    public static DependencyRegistrar<T> ToRegistrar<T>(this Dependency<T> dependency, IServiceCollection services)
        where T : class
        =>
        InnerToRegistrar(
            dependency ?? throw new ArgumentNullException(nameof(dependency)),
            services ?? throw new ArgumentNullException(nameof(services)));

    private static DependencyRegistrar<T> InnerToRegistrar<T>(Dependency<T> dependency, IServiceCollection services)
        where T : class
        =>
        new(services, dependency.Resolve);
}
