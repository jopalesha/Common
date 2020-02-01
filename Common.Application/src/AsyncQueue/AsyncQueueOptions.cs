using System;
using Jopalesha.Common.Infrastructure.Helpers;

namespace Jopalesha.Common.Application.AsyncQueue
{
    public class AsyncQueueOptions
    {
        public AsyncQueueOptions(int consumersCount) : this(consumersCount, TimeSpan.FromSeconds(10))
        {
        }

        public AsyncQueueOptions(int consumersCount, TimeSpan interval)
        {
            ConsumersCount = Check.IsTrue(consumersCount, it => it > 0 && it < 10);
            Interval = Check.IsTrue(interval, it => it.TotalSeconds >= 1);
        }

        public int ConsumersCount { get; }

        public TimeSpan Interval { get; }

        public static AsyncQueueOptions Default => new AsyncQueueOptions(1, TimeSpan.FromSeconds(10));
    }
}