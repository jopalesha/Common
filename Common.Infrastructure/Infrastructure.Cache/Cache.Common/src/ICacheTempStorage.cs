using System.Collections.Generic;

namespace Jopalesha.Common.Infrastructure.Cache.Common
{
    public interface ICacheTempStorage
    {
        void Add(string key, object value);

        void AddRange(IDictionary<string, object> values);

        IDictionary<string, object> GetAll();
    }
}