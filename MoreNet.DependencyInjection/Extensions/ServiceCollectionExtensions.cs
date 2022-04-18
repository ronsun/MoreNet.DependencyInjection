using MoreNet.DependencyInjection;
using System;
using System.Collections.Generic;

#pragma warning disable SA1403 // File may only contain a single namespace
#pragma warning disable SA1402 // File may only contain a single type

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///  Adds a singleton service of the types specified in <see cref="INamedServiceContainer{TService}"/>
        ///  with the factory <see cref="NamedServiceDictionaryFactory{TService}(IServiceProvider)"/> for implementation.
        /// </summary>
        /// <typeparam name="TService">Service Type.</typeparam>
        /// <param name="services">Services.</param>
        /// <returns>Current <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddNamedSingleton<TService>(this IServiceCollection services)
            where TService : INameable
        {
            services.AddSingleton(NamedServiceDictionaryFactory<TService>);
            return services;
        }

        /// <summary>
        ///  Adds a scoped service of the types specified in <see cref="INamedServiceContainer{TService}"/>
        ///  with the factory <see cref="NamedServiceDictionaryFactory{TService}(IServiceProvider)"/> for implementation.
        /// </summary>
        /// <typeparam name="TService">Service Type.</typeparam>
        /// <param name="services">Services.</param>
        /// <returns>Current <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddNamedScoped<TService>(this IServiceCollection services)
            where TService : INameable
        {
            services.AddScoped(NamedServiceDictionaryFactory<TService>);
            return services;
        }

        /// <summary>
        ///  Adds a transient service of the types specified in <see cref="INamedServiceContainer{TService}"/>
        ///  with the factory <see cref="NamedServiceDictionaryFactory{TService}(IServiceProvider)"/> for implementation.
        /// </summary>
        /// <typeparam name="TService">Service Type.</typeparam>
        /// <param name="services">Services.</param>
        /// <returns>Current <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddNamedTransient<TService>(this IServiceCollection services)
            where TService : INameable
        {
            services.AddTransient(NamedServiceDictionaryFactory<TService>);
            return services;
        }

        /// <summary>
        /// Factory to implement service.
        /// </summary>
        /// <typeparam name="TService">Service type.</typeparam>
        /// <param name="serviceProvider">Service provider.</param>
        /// <returns>Implementation.</returns>
        internal static INamedServiceContainer<TService> NamedServiceDictionaryFactory<TService>(IServiceProvider serviceProvider)
            where TService : INameable
        {
            var implemetations = serviceProvider.GetServices<TService>();
            var underlyingDictionary = new Dictionary<string, TService>();
            foreach (var impl in implemetations)
            {
                if (underlyingDictionary.ContainsKey(impl.Name))
                {
                    throw new InvalidOperationException($"Duplicate Name: {impl.Name}");
                }

                underlyingDictionary.Add(impl.Name, impl);
            }

            return new NamedServiceDictionary<TService>(underlyingDictionary);
        }
    }
}

// These extension methods enhanced from Microsoft.Extensions.DependencyInjection.Extensions,
// so use the same namespace.
namespace Microsoft.Extensions.DependencyInjection.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the specified <typeparamref name="TService"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// service using the factory <see cref="DependencyInjection.ServiceCollectionExtensions.NamedServiceDictionaryFactory{TService}(IServiceProvider)"/> for implementation.
        /// if the service type hasn't already been registered.
        /// </summary>
        /// <typeparam name="TService">Service Type.</typeparam>
        /// <param name="services">Services.</param>
        /// <returns>Current <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection TryAddNamedSingleton<TService>(this IServiceCollection services)
            where TService : INameable
        {
            services.TryAddSingleton(DependencyInjection.ServiceCollectionExtensions.NamedServiceDictionaryFactory<TService>);
            return services;
        }

        /// <summary>
        /// Adds the specified <typeparamref name="TService"/> as a <see cref="ServiceLifetime.Scoped"/>.
        /// service using the factory <see cref="DependencyInjection.ServiceCollectionExtensions.NamedServiceDictionaryFactory{TService}(IServiceProvider)"/> for implementation.
        /// if the service type hasn't already been registered.
        /// </summary>
        /// <typeparam name="TService">Service Type.</typeparam>
        /// <param name="services">Services.</param>
        /// <returns>Current <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection TryAddNamedScoped<TService>(this IServiceCollection services)
            where TService : INameable
        {
            services.TryAddScoped(DependencyInjection.ServiceCollectionExtensions.NamedServiceDictionaryFactory<TService>);
            return services;
        }

        /// <summary>
        /// Adds the specified <typeparamref name="TService"/> as a <see cref="ServiceLifetime.Transient"/>.
        /// service using the factory <see cref="DependencyInjection.ServiceCollectionExtensions.NamedServiceDictionaryFactory{TService}(IServiceProvider)"/> for implementation.
        /// if the service type hasn't already been registered.
        /// </summary>
        /// <typeparam name="TService">Service Type.</typeparam>
        /// <param name="services">Services.</param>
        /// <returns>Current <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection TryAddNamedTransient<TService>(this IServiceCollection services)
            where TService : INameable
        {
            services.TryAddTransient(DependencyInjection.ServiceCollectionExtensions.NamedServiceDictionaryFactory<TService>);
            return services;
        }

        /// <summary>
        /// Adds singleton service if an existing service <typeparamref name="TService"/>
        /// and an implementation that does not already exist in services.
        /// </summary>
        /// <typeparam name="TService">Service type.</typeparam>
        /// <typeparam name="TImplementation">Implementation type.</typeparam>
        /// <param name="services">Service collection.</param>
        /// <returns>Current <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection TryAddSingletonEnumerable<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService
        {
            var descriptor = ServiceDescriptor.Singleton<TService, TImplementation>();
            services.TryAddEnumerable(descriptor);
            return services;
        }

        /// <summary>
        /// Adds scoped service if an existing service <typeparamref name="TService"/>
        /// and an implementation that does not already exist in services.
        /// </summary>
        /// <typeparam name="TService">Service type.</typeparam>
        /// <typeparam name="TImplementation">Implementation type.</typeparam>
        /// <param name="services">Service collection.</param>
        /// <returns>Current <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection TryAddScopedEnumerable<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService
        {
            var descriptor = ServiceDescriptor.Scoped<TService, TImplementation>();
            services.TryAddEnumerable(descriptor);
            return services;
        }

        /// <summary>
        /// Adds transient service if an existing service <typeparamref name="TService"/>
        /// and an implementation that does not already exist in services.
        /// </summary>
        /// <typeparam name="TService">Service type.</typeparam>
        /// <typeparam name="TImplementation">Implementation type.</typeparam>
        /// <param name="services">Service collection.</param>
        /// <returns>Current <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection TryAddTransientEnumerable<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService
        {
            var descriptor = ServiceDescriptor.Transient<TService, TImplementation>();
            services.TryAddEnumerable(descriptor);
            return services;
        }
    }
}

#pragma warning restore SA1403 // File may only contain a single namespace
#pragma warning restore SA1402 // File may only contain a single type