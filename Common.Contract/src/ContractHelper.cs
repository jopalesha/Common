using System.Collections.Generic;

namespace Jopalesha.Common.Contract
{
    public static class ContractHelper
    {
        public static ListResultDto<T> EmptyResultDto<T>()
        {
            return new ListResultDto<T>
            {
                Items = new List<T>(),
                TotalCount = 0
            };
        }
    }
}
