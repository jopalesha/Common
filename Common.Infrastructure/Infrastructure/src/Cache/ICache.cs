using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Jopalesha.Common.Infrastructure.Cache
{
    public interface ICache
    {
        /// <summary>
        /// Add value to cache
        /// </summary>
        /// <typeparam name="T">Value Type</typeparam>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <param name="token">Cancellation token</param>
        Task Add<T>(string key, T value, CancellationToken token = default);

        /// <summary>
        /// Add list of items to cache
        /// </summary>
        /// <param name="items">Dictionary of items</param>
        /// <param name="token">Cancellation Token</param>
        Task AddRange(IDictionary<string, object> items, CancellationToken token = default);

        /// <summary>
        /// Get value by key
        /// </summary>
        /// <typeparam name="T">Value type</typeparam>
        /// <param name="key">Key</param>
        /// <param name="token">Cancellation token</param>
        /// <returns>Value if exists. Throws <see cref="CacheItemNotFoundException"/> if not exists</returns>
        Task<T> Get<T>(string key, CancellationToken token = default);

        /// <summary>
        /// Find value by key
        /// </summary>
        /// <typeparam name="T">Value Type</typeparam>
        /// <param name="key">Key</param>
        /// <param name="token">Token</param>
        /// <returns>Value if exists. Null if not exists</returns>
        Task<T> Find<T>(string key, CancellationToken token = default);

        /// <summary>
        /// Delete value by key
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="token">Cancellation token</param>
        Task Delete(string key, CancellationToken token = default);

        /// <summary>
        /// Delete all cache items
        /// </summary>
        /// <param name="token">Cancellation token</param>
        Task Clear(CancellationToken token = default);
    }
}
