using System.Collections.Generic;
using System.Linq;
using Jopalesha.Common.Infrastructure;

namespace Jopalesha.Common.Domain.Models
{
    public class ListResult<T>
    {
        public ListResult(IEnumerable<T> items, int totalCount)
        {
            Items = Check.NotNull(items, nameof(items)).ToList();
            TotalCount = Check.IsTrue(totalCount, it => it >= 0, nameof(totalCount));
        }

        public IReadOnlyList<T> Items { get; }

        public int TotalCount { get; }
    }
}

