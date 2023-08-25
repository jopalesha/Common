namespace Jopalesha.Common.Application.AsyncQueue
{
    public interface IAsyncQueue
    {
        void AddJob(IAsyncQueueRequest job);

        IAsyncQueueRequest DequeueJob();
    }
}
