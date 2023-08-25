namespace Jopalesha.Common.Infrastructure.Cache
{
    /// <summary>
    /// Cache.
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// Add value to cache.
        /// </summary>
        /// <typeparam name="T">Value Type.</typeparam>
        /// <param name="key">Key.</param>
        /// <param name="value">Value.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task Add<T>(string key, T value, CancellationToken token = default);

        /// <summary>
        /// Add list of items to cache.
        /// </summary>
        /// <param name="items">Collection of key/value pairs.</param>
        /// <param name="token">Cancellation Token.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task AddRange(IDictionary<string, object> items, CancellationToken token = default);

        /// <summary>
        /// Get value by key.
        /// </summary>
        /// <typeparam name="T">Value type.</typeparam>
        /// <param name="key">Key.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Returns value if exists. Throws <see cref="CacheItemNotFoundException"/> if not exists.</returns>
        Task<T> Get<T>(string key, CancellationToken token = default);

        /// <summary>
        /// Find value by key.
        /// </summary>
        /// <typeparam name="T">Value Type.</typeparam>
        /// <param name="key">Key.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Returns value if exists. Null if not exists.</returns>
        Task<T> Find<T>(string key, CancellationToken token = default);

        /// <summary>
        /// Delete value by key.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task Delete(string key, CancellationToken token = default);

        /// <summary>
        /// Delete all cache items.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task Clear(CancellationToken token = default);
    }
}
