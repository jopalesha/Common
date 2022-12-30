namespace Jopalesha.Common.Infrastructure.Cache
{
    /// <summary>
    /// Base cache exception.
    /// </summary>
    public class CacheException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CacheException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public CacheException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheException"/> class with inner exception.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="inner">Inner exception.</param>
        public CacheException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
