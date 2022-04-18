using System.Collections.Generic;

namespace MoreNet.DependencyInjection
{
    /// <summary>
    /// An friendly interface inherit from <see cref="IReadOnlyDictionary{TKey, TValue}"/>.
    /// The key in <see cref="string"/> type and value in <typeparamref name="TService"/> type.
    /// </summary>
    /// <typeparam name="TService">The service type.</typeparam>
    public interface INamedServiceContainer<TService>
        where TService : INameable
    {
        /// <summary>
        /// Get service in <typeparamref name="TService"/> type.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <returns>Found service.</returns>
        TService GetService(string name);
    }
}
