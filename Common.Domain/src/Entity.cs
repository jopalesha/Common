using System.Collections.Generic;
using Jopalesha.CheckWhenDoIt;


// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local
// ReSharper disable NonReadonlyMemberInGetHashCode

namespace Jopalesha.Common.Domain
{
    public abstract class Entity<T> : IEntity<T>
    {
        private readonly T _id;

        protected virtual IEqualityComparer<T> Comparer => EqualityComparer<T>.Default;

        protected Entity(Id<T> id) : this()
        {
            if (!(Check.NotNull(id) is GeneratedId<T>))
            {
                _id = id.Value;
            }
        }

        private Entity() { }

        public T Id => Check.NotDefault(_id);


        #region Equality

        public sealed override bool Equals(object obj)
        {
            if (obj is Entity<T> entity)
            {
                return Equals(entity);
            }

            return false;
        }

        public sealed override int GetHashCode() => Comparer.GetHashCode(Id);

        public virtual bool Equals(Entity<T> other)
        {
            return other != null && Comparer.Equals(Id, other.Id);
        }

        public static bool operator ==(Entity<T> left, Entity<T> right) => Equals(left, right);

        public static bool operator !=(Entity<T> left, Entity<T> right) => !(left == right);

        #endregion
    }
}
