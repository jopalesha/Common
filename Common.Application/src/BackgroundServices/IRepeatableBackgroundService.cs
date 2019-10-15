using System;

namespace Jopalesha.Common.Application.BackgroundServices
{
    public interface IRepeatableBackgroundService : IBackgroundService
    {
        TimeSpan TimeOut { get; }
    }
}
