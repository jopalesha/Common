using System.Collections.Generic;

namespace Jopalesha.Common.Contract
{
    /// <summary>
    /// List of items result.
    /// </summary>
    /// <typeparam name="T">Item type.</typeparam>
    public class ListResultDto<T>
    {
        /// <summary>
        /// Gets or sets items.
        /// </summary>
        public List<T> Items { get; set; } = new();

        /// <summary>
        /// Gets or sets total items count.
        /// </summary>
        public int TotalCount { get; set; }
    }
}
