using Jopalesha.CheckWhenDoIt;

namespace Jopalesha.Common.Infrastructure.Cache
{
    /// <summary>The exception that is thrown when cache item already exists.</summary>
    public class CacheItemAlreadyExistsException : CacheException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CacheItemAlreadyExistsException"/> class.
        /// </summary>
        /// <param name="key">Key.</param>
        public CacheItemAlreadyExistsException(string key) : this(key, $"Item {key} already exists")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheItemAlreadyExistsException"/> class.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="message">Error message.</param>
        public CacheItemAlreadyExistsException(string key, string message) : base(message)
        {
            Key = Check.NotEmpty(key);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheItemAlreadyExistsException"/> class with inner exception.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="message">Error message.</param>
        /// <param name="inner">Inner exception.</param>
        public CacheItemAlreadyExistsException(
            string key,
            string message,
            Exception inner) : base(message, inner)
        {
            Key = Check.NotEmpty(key);
        }

        /// <summary>
        /// Gets key.
        /// </summary>
        public string Key { get; }
    }
}
