using System.ComponentModel.DataAnnotations;

namespace Jopalesha.Common.Infrastructure.Cache.Sql
{
    internal class CacheItem 
    {
        [Key]
        public string Key { get; set; }

        public string Value { get; set; }
    }
}
