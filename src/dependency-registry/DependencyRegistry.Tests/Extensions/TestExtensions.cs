using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace PrimeFuncPack.DependencyRegistry.Tests;

internal static class TestExtensions
{
    internal static IServiceCollection? GetInnerServices<T>(this DependencyRegistrar<T> registrar)
        where T : class
        =>
        typeof(DependencyRegistrar<T>).GetField(
            "services", BindingFlags.Instance | BindingFlags.NonPublic)?
        .GetValue(registrar) as IServiceCollection;

    internal static Func<IServiceProvider, T>? GetInnerResolver<T>(this DependencyRegistrar<T> registrar)
        where T : class
        =>
        typeof(DependencyRegistrar<T>).GetField(
            "resolver", BindingFlags.Instance | BindingFlags.NonPublic)?
        .GetValue(registrar) as Func<IServiceProvider, T>;
}