using System;

namespace Jopalesha.Common.Application.BackgroundServices
{
    public class BackgroundServiceOptions
    {
        public BackgroundServiceOptions(TimeSpan interval)
        {
            Interval = interval;
        }

        public TimeSpan Interval { get; }

        public static BackgroundServiceOptions Default => new BackgroundServiceOptions(TimeSpan.FromSeconds(10));
    }
}
