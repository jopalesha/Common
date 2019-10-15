using System.Threading.Tasks;
using Jopalesha.Common.Application.AsyncQueue;
using Jopalesha.Common.Hosting.Test.Sample.Components;
using Jopalesha.Common.Hosting.Test.Sample.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace Jopalesha.Common.Hosting.Test.Sample
{
    [Route("api/test")]
    [ApiController]
    public class TestController
    {
        private readonly IControllerService _testService;
        private readonly IAsyncQueue _asyncQueue;

        public TestController(
            IControllerService testService,
            IAsyncQueue asyncQueue)
        {
            _testService = testService;
            _asyncQueue = asyncQueue;
        }

        [HttpGet]
        public int Get()
        {
            return _testService.Get();
        }

        [HttpPost]
        public Task PutToCacheAsync(int value)
        {
            var request = new PutRequest {Value = value};

            _asyncQueue.AddJob(request);
            return Task.CompletedTask;
        }

        [HttpDelete]
        public Task Delete()
        {
            var request = new DeleteRequest();
            _asyncQueue.AddJob(request);
            return Task.CompletedTask;
        }
    }
}
