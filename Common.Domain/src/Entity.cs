using System.Collections.Generic;
using Jopalesha.CheckWhenDoIt;
using Jopalesha.CheckWhenDoIt.Conditions;
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local
// ReSharper disable NonReadonlyMemberInGetHashCode

namespace Jopalesha.Common.Domain
{
    /// <summary>
    /// Base entity class.
    /// </summary>
    /// <typeparam name="T">Type of id.</typeparam>
    public abstract class Entity<T> : IEntity<T>
    {
        private readonly T _id;
        private readonly bool _isGeneratedId;

        /// <summary>
        /// Gets entity comparer.
        /// </summary>
        protected virtual IEqualityComparer<T> Comparer => EqualityComparer<T>.Default;

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity{T}"/> class.
        /// </summary>
        /// <param name="id">Id.</param>
        protected Entity(Id<T> id) : this()
        {
            if (Check.NotNull(id) is GeneratedId<T>)
            {
                _isGeneratedId = true;
            }

            _id = id.Value;
        }

        private Entity() { }

        /// <inheritdoc />
        public T Id => When.False(_isGeneratedId).Then(_id).ElseThrows();

        #region Equality

        /// <inheritdoc />
        public sealed override bool Equals(object obj)
        {
            if (obj is Entity<T> entity)
            {
                return Equals(entity);
            }

            return false;
        }

        /// <inheritdoc />
        public sealed override int GetHashCode() => Comparer.GetHashCode(Id);

        /// <inheritdoc />
        public virtual bool Equals(Entity<T> other) => other != null && Comparer.Equals(Id, other.Id);

        /// <summary>
        /// Equality operator.
        /// </summary>
        /// <param name="left">First item.</param>
        /// <param name="right">Second item.</param>
        /// <returns>True if items are equal; otherwise, false.</returns>
        public static bool operator ==(Entity<T> left, Entity<T> right) => Equals(left, right);

        /// <summary>
        /// Non equality operator.
        /// </summary>
        /// <param name="left">First item.</param>
        /// <param name="right">Second item.</param>
        /// <returns>True if items are not equal; otherwise, false.</returns>
        public static bool operator !=(Entity<T> left, Entity<T> right) => !(left == right);

        #endregion
    }
}
