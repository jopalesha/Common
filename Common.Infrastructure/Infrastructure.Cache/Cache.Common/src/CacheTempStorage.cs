using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Jopalesha.Helpers.Extensions;

namespace Jopalesha.Common.Infrastructure.Cache.Common
{
    internal class CacheTempStorage : ICacheTempStorage
    {
        private readonly ConcurrentQueue<(string Key, object Value)> _storage;

        public CacheTempStorage()
        {
            _storage = new ConcurrentQueue<(string, object)>();
        }

        public void Add(string key, object value)
        {
            if (!_storage.Any(it => string.Equals(it.Key, key, StringComparison.OrdinalIgnoreCase)))
            {
                _storage.Enqueue((key, value));
            }
        }

        public void AddRange(IDictionary<string, object> items)
        {
            foreach (var (key, value) in items)
            {
                Add(key, value);
            }
        }

        public IDictionary<string, object> GetAll() => _storage.DequeueAll().ToDictionary(it => it.Key, it => it.Value);
    }
}
