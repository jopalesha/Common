using System.Threading;
using System.Threading.Tasks;

namespace Jopalesha.Common.Infrastructure.Notification
{
    public interface INotificationService
    {
        Task SendMessage(string message, CancellationToken token = default);
    }
}