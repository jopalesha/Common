using Jopalesha.Common.Application.AsyncQueue;

namespace Jopalesha.Common.Hosting.Test.Sample.Handlers
{
    public class PutRequest: IAsyncQueueRequest
    {
        public int Value { get; set; }

        public string Name => nameof(PutRequest);
    }
}