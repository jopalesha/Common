using System.Threading;
using System.Threading.Tasks;
using Jopalesha.Common.Application.BackgroundServices;
using Microsoft.Extensions.Hosting;

namespace Jopalesha.Common.Hosting
{
    internal class HostedBackgroundService : BackgroundService
    {
        private readonly IBackgroundService _service;

        public HostedBackgroundService(IBackgroundService hostedService)
        {
            _service = hostedService;
        }

        protected override async Task ExecuteAsync(CancellationToken token)
        {
            await _service.ExecuteAsync(token);
        }
    }
}
