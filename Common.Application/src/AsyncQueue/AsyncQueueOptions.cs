using Jopalesha.Common.Infrastructure;

namespace Jopalesha.Common.Application.AsyncQueue
{
    public class AsyncQueueOptions
    {
        public AsyncQueueOptions(int consumersCount)
        {
            ConsumersCount = Check.IsTrue(consumersCount, it => it > 0 && it < 10);
        }

        public int ConsumersCount { get; }
    }
}