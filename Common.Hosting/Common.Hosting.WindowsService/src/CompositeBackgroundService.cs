using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Jopalesha.Common.Application.BackgroundServices;
using Microsoft.Extensions.Hosting;

namespace Jopalesha.Common.Hosting
{
    internal class CompositeBackgroundService : BackgroundService
    {
        private readonly IEnumerable<IBackgroundService> _backgroundServices;

        public CompositeBackgroundService(IEnumerable<IBackgroundService> backgroundServices)
        {
            _backgroundServices = backgroundServices;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.WhenAll(_backgroundServices.Select(it => it.ExecuteAsync(stoppingToken)));
        }
    }
}