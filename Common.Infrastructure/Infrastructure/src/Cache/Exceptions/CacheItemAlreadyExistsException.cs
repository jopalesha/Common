using System;

namespace Jopalesha.Common.Infrastructure.Cache
{
    public class CacheItemAlreadyExistsException : CacheException
    {
        public CacheItemAlreadyExistsException()
        {
        }

        public CacheItemAlreadyExistsException(string message) : base(message)
        {
        }

        public CacheItemAlreadyExistsException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
