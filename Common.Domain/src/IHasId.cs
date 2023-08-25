namespace Jopalesha.Common.Domain
{
    /// <summary>
    /// Interface, which indicates, that type has identifier.
    /// </summary>
    /// <typeparam name="TKey">Type of identifier.</typeparam>
    public interface IHasId<out TKey>
    {
        /// <summary>
        /// Gets id.
        /// </summary>
        TKey Id { get; }
    }
}
