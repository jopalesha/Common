using Jopalesha.Common.Infrastructure.Cache.Common;

namespace Jopalesha.Common.Infrastructure.Cache.Sql
{
    public class SqlCacheOptions : ICacheOptions
    {
        public SqlCacheOptions(string connectionName) : this(connectionName, false)
        {
        }

        public SqlCacheOptions(string connectionName, bool isBackground)
        {
            ConnectionName = connectionName;
            IsBackground = isBackground;
        }


        public string ConnectionName { get; }

        public bool IsBackground { get; }
    }
}
