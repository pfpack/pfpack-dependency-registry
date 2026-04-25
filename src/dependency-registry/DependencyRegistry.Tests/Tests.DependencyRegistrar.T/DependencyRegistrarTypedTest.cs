using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace PrimeFuncPack.DependencyRegistry.Tests;

public static partial class DependencyRegistrarTypedTest
{
    private static DependencyRegistrar<T> InnerCreateRegistrar<T>(
        IServiceCollection services, Func<IServiceProvider, T> resolver)
        where T : class
    {
        var constructor = typeof(DependencyRegistrar<T>).GetConstructor(
            BindingFlags.Instance | BindingFlags.NonPublic,
            null,
            [typeof(IServiceCollection), typeof(Func<IServiceProvider, T>)],
            null)
            ?? throw new InvalidOperationException(
                $"The non-public constructor {nameof(DependencyRegistrar<>)}(IServiceCollection, Func<IServiceProvider, T>) was not found.");

        return (DependencyRegistrar<T>)constructor.Invoke([services, resolver]);
    }
}