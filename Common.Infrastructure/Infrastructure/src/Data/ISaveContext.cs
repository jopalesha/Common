namespace Jopalesha.Common.Infrastructure.Data
{
    /// <summary>
    /// Database context saver.
    /// </summary>
    public interface ISaveContext
    {
        /// <summary>
        /// Save context.
        /// </summary>
        void Save();

        /// <summary>
        /// Save context async.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task SaveAsync(CancellationToken token = default);
    }
}
