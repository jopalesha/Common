namespace Jopalesha.Common.Domain
{
    /// <summary>
    /// Entity.
    /// </summary>
    /// <typeparam name="TKey">Id type.</typeparam>
    public interface IEntity<TKey> : IEquatable<IEntity<TKey>>, IHasId<TKey>
    {
    }
}
