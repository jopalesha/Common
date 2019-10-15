namespace Jopalesha.Common.Application.AsyncQueue
{
    public abstract class AsyncQueueRequestHandler<T> : MediatR.AsyncRequestHandler<T>
        where T : IAsyncQueueRequest
    {
    }
}
