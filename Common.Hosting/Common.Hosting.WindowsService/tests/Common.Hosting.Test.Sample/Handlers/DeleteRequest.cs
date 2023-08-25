using Jopalesha.Common.Application.AsyncQueue;

namespace Jopalesha.Common.Hosting.Test.Sample.Handlers
{
    public class DeleteRequest : IAsyncQueueRequest
    {
        public string Name => nameof(DeleteRequest);
    }
}