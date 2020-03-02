using System.Collections.Generic;
using Jopalesha.Common.Infrastructure.Helpers;
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local
// ReSharper disable NonReadonlyMemberInGetHashCode

namespace Jopalesha.Common.Domain
{
    public abstract class Entity<TKey> : IEntity<TKey>
    {
        private readonly IEqualityComparer<TKey> _comparer;

        protected Entity(IKey<TKey> id) : this(id, EqualityComparer<TKey>.Default)
        {
        }

        protected Entity(IKey<TKey> id, IEqualityComparer<TKey> comparer) : this()
        {
            Id = Check.NotNull(id).Value;
            _comparer = Check.NotNull(comparer, nameof(comparer));
        }
        
        private Entity() { }


        public TKey Id { get; private set; }


        #region Equality

        public sealed override bool Equals(object obj)
        {
            if (obj is Entity<TKey> entity)
            {
                return Equals(entity);
            }

            return false;
        }

        public sealed override int GetHashCode() => _comparer.GetHashCode(Id);

        public virtual bool Equals(Entity<TKey> other)
        {
            return other != null && _comparer.Equals(Id, other.Id);
        }

        public static bool operator ==(Entity<TKey> left, Entity<TKey> right) => Equals(left, right);

        public static bool operator !=(Entity<TKey> left, Entity<TKey> right) => !(left == right);

        #endregion
    }
}
