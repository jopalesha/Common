using Jopalesha.CheckWhenDoIt;

namespace Jopalesha.Common.Infrastructure.Cache;

/// <summary>
/// The exception that is thrown when cache item is not found.
/// </summary>
public class CacheItemNotFoundException : CacheException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CacheItemNotFoundException"/> class.
    /// </summary>
    /// <param name="key">Key.</param>
    public CacheItemNotFoundException(string key) : this(key, $"Item {key} does not found")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CacheItemNotFoundException"/> class.
    /// </summary>
    /// <param name="key">Key.</param>
    /// <param name="message">Error message.</param>
    public CacheItemNotFoundException(string key, string message) : base(message)
    {
        Key = Check.NotEmpty(key);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CacheItemNotFoundException"/> class with inner exception.
    /// </summary>
    /// <param name="key">Key.</param>
    /// <param name="message">Error message.</param>
    /// <param name="inner">Inner exception.</param>
    public CacheItemNotFoundException(
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
