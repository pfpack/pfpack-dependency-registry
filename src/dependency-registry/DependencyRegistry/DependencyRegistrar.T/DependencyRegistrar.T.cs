using Microsoft.Extensions.DependencyInjection;
using System;

namespace PrimeFuncPack;

public sealed partial class DependencyRegistrar<T>
    where T : class
{
    private readonly IServiceCollection services;

    private readonly Func<IServiceProvider, T> resolver;

    internal DependencyRegistrar(IServiceCollection services, Func<IServiceProvider, T> resolver)
        =>
        (this.services, this.resolver) = (services, resolver);

    public static DependencyRegistrar<T> Create(IServiceCollection services, Func<IServiceProvider, T> resolver)
        =>
        new(
            services ?? throw new ArgumentNullException(nameof(services)),
            resolver ?? throw new ArgumentNullException(nameof(resolver)));
}
