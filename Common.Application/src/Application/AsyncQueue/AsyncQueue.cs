using System.Collections.Concurrent;

namespace Jopalesha.Common.Application.AsyncQueue
{
    internal class AsyncQueue : IAsyncQueue
    {
        private readonly ConcurrentQueue<IAsyncQueueRequest> _requests = new ConcurrentQueue<IAsyncQueueRequest>();

        public void AddJob(IAsyncQueueRequest job)
        {
            _requests.Enqueue(job);
        }
        
        public IAsyncQueueRequest DequeueJob()
        {
            _requests.TryDequeue(out var request);
            return request;
        }
    }
}
