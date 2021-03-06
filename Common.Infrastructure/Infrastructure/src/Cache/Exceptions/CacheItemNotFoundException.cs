﻿using System;

namespace Jopalesha.Common.Infrastructure.Cache
{
    public class CacheItemNotFoundException : CacheException
    {
        public CacheItemNotFoundException()
        {
        }

        public CacheItemNotFoundException(string message) : base(message)
        {
        }

        public CacheItemNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
