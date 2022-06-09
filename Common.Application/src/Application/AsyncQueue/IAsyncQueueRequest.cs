using MediatR;

namespace Jopalesha.Common.Application.AsyncQueue
{
    public interface IAsyncQueueRequest : IRequest
    {
        string Name { get; }
    }
}
