using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Jopalesha.Common.Infrastructure.Extensions
{
    public static class ConcurrentQueueExtensions
    {
        public static IList<T> DequeueAll<T>(this ConcurrentQueue<T> queue)
        {
            var result = new List<T>();
            var count = queue.Count;

            for (var i = 0; i < count; i++)
            {
                if (queue.TryDequeue(out var item))
                {
                    result.Add(item);
                }
            }

            return result;
        }
    }
}
