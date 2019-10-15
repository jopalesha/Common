using System.Threading;
using System.Threading.Tasks;

namespace Jopalesha.Common.Application.BackgroundServices
{
    public interface IBackgroundService
    {
        Task ExecuteAsync(CancellationToken token);
    }
}