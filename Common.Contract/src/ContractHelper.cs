using System.Collections.Generic;

namespace Jopalesha.Common.Contract
{
    /// <summary>
    /// Contract helper.
    /// </summary>
    public static class ContractHelper
    {
        /// <summary>
        /// Creates empty list result.
        /// </summary>
        /// <typeparam name="T">Item type.</typeparam>
        /// <returns>Empty list result.</returns>
        public static ListResultDto<T> EmptyResultDto<T>() => new() { Items = new List<T>(), TotalCount = 0 };
    }
}
