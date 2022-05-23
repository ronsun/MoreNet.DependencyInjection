using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;

namespace MoreNet.DependencyInjection.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///  Adds a named singleton service of the types specified in <see cref="INamedServiceContainer{TService}"/>.
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
        ///  Adds a named scoped service of the types specified in <see cref="INamedServiceContainer{TService}"/>.
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
        ///  Adds a named transient service of the types specified in <see cref="INamedServiceContainer{TService}"/>.
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
        /// Adds the specified <typeparamref name="TService"/> as a <see cref="ServiceLifetime.Singleton"/>
        /// if the service type hasn't already been registered.
        /// </summary>
        /// <typeparam name="TService">Service Type.</typeparam>
        /// <param name="services">Services.</param>
        /// <returns>Current <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection TryAddNamedSingleton<TService>(this IServiceCollection services)
            where TService : INameable
        {
            services.TryAddSingleton(NamedServiceDictionaryFactory<TService>);
            return services;
        }

        /// <summary>
        /// Adds the specified <typeparamref name="TService"/> as a <see cref="ServiceLifetime.Scoped"/>
        /// if the service type hasn't already been registered.
        /// </summary>
        /// <typeparam name="TService">Service Type.</typeparam>
        /// <param name="services">Services.</param>
        /// <returns>Current <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection TryAddNamedScoped<TService>(this IServiceCollection services)
            where TService : INameable
        {
            services.TryAddScoped(NamedServiceDictionaryFactory<TService>);
            return services;
        }

        /// <summary>
        /// Adds the specified <typeparamref name="TService"/> as a <see cref="ServiceLifetime.Transient"/>
        /// if the service type hasn't already been registered.
        /// </summary>
        /// <typeparam name="TService">Service Type.</typeparam>
        /// <param name="services">Services.</param>
        /// <returns>Current <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection TryAddNamedTransient<TService>(this IServiceCollection services)
            where TService : INameable
        {
            services.TryAddTransient(NamedServiceDictionaryFactory<TService>);
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