using System.Collections.Generic;

namespace Jopalesha.Common.Hosting.Test.Sample.Components
{
    public class HistoryStorage : IHistoryStorage
    {
        private readonly List<int> _storage;

        public HistoryStorage()
        {
            _storage = new List<int>();
        }

        public void Add(int value)
        {
            _storage.Add(value);
        }
    }
}