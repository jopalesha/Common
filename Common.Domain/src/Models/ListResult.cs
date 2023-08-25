using Jopalesha.CheckWhenDoIt;

namespace Jopalesha.Common.Domain.Models
{
    /// <summary>
    /// List result.
    /// </summary>
    /// <typeparam name="T">Item type.</typeparam>
    public class ListResult<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListResult{T}"/> class.
        /// </summary>
        /// <param name="items">Items.</param>
        /// <param name="totalCount">Total count.</param>
        public ListResult(IEnumerable<T> items, int totalCount)
        {
            Items = Check.NotNull(items, nameof(items)).ToList();
            TotalCount = Check.True(totalCount, It.IsNatural, nameof(totalCount));
        }

        /// <summary>
        /// Gets items.
        /// </summary>
        public IReadOnlyList<T> Items { get; }

        /// <summary>
        /// Gets total count.
        /// </summary>
        public int TotalCount { get; }
    }
}
