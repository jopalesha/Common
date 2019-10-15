namespace Jopalesha.Common.Infrastructure.Cache.Sql
{
    public class SqlCacheOptions
    {
        public SqlCacheOptions(string connectionName) : this(connectionName, false)
        {
            ConnectionName = connectionName;
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
