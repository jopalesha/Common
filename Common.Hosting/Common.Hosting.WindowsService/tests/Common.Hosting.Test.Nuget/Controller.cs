using System.Net;
using System.Net.Http;
using Common.Hosting.Test.Nuget.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Common.Hosting.Test.Nuget
{
    [Route("api/test")]
    [ApiController]
    public class TestController
    {
        private readonly IControllerService _testService;

        public TestController(IControllerService testService)
        {
            _testService = testService;
        }

        [HttpGet]
        public int Get()
        {
            return _testService.Get();
        }

        [HttpPost]
        public HttpResponseMessage Set([FromBody]int value)
        {
            _testService.Set(value);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
