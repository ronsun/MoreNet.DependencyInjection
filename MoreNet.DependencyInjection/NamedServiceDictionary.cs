using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MoreNet.DependencyInjection
{
    /// <summary>
    /// An friendly interface inherit from <see cref="IReadOnlyDictionary{TKey, TValue}"/>.
    /// The key in <see cref="string"/> type and value in <typeparamref name="TService"/> type.
    /// </summary>
    /// <typeparam name="TService">Service type.</typeparam>
    internal class NamedServiceDictionary<TService> : ReadOnlyDictionary<string, TService>, INamedServiceContainer<TService>
        where TService : INameable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NamedServiceDictionary{TService}"/> class.
        /// </summary>
        /// <param name="dictionary">Injected <see cref="IDictionary{TKey, TValue}"/>.</param>
        public NamedServiceDictionary(IDictionary<string, TService> dictionary)
            : base(dictionary)
        {
        }

        /// <inheritdoc/>
        public TService GetService(string name)
        {
            TryGetValue(name, out TService value);
            return value;
        }
    }
}
