using System.Collections.Generic;

namespace Jopalesha.Common.Contract
{
    public class ListResultDto<T>
    {
        public List<T> Items { get; set; }

        public int TotalCount { get; set; }
    }
}
