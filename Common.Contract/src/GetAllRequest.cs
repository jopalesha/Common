using System.ComponentModel.DataAnnotations;

namespace Jopalesha.Common.Contract
{
    public class GetAllRequest
    {
        public GetAllRequest()
        {
            Count = 100;
        }

        [Range(1, 1000)]
        public int Count { get; set; }
    }
}
