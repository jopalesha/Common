using System.Collections.Generic;
using System.Linq;
using Jopalesha.CheckWhenDoIt;

namespace Jopalesha.Common.Domain.Models
{
    public class ListResult<T>
    {
        public ListResult(IEnumerable<T> items, int totalCount)
        {
            Items = Check.NotNull(items, nameof(items)).ToList();
            TotalCount = Check.IsTrue(totalCount, It.IsPositive.Integer, nameof(totalCount));
        }

        public IReadOnlyList<T> Items { get; }

        public int TotalCount { get; }
    }
}

