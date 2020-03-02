using System;

namespace Jopalesha.Common.Domain
{
    public interface IEntity<TKey> : IEquatable<Entity<TKey>>, IHasId<TKey>
    {
    }
}