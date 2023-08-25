namespace Jopalesha.Common.Infrastructure.Notification
{
    /// <summary>
    /// Notification service.
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Send message.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Asynchronous operation result.</returns>
        Task SendMessage(string message, CancellationToken token = default);
    }
}
