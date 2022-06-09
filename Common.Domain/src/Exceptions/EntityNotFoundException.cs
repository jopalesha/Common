using System;
using System.Collections.Generic;
using System.Text;

namespace Jopalesha.Common.Domain.Exceptions
{
    /// <summary>
    /// Entity not found exception.
    /// </summary>
    [Serializable]
    public class EntityNotFoundException<TEntity, TKey> : Exception where TEntity:IEntity<TKey>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityNotFoundException{TEntity, TKey}"/> class.
        /// </summary>
        /// <param name="id">Identifier.</param>
        public EntityNotFoundException(TKey id) : this($"There is no {nameof(TEntity)} entity with id {id}")
        {
            Id = id;
        }

        public TKey Id { get; }

        /// <inheritdoc />
        public EntityNotFoundException(string message) : base(message) { }

        /// <inheritdoc />
        public EntityNotFoundException(string message, Exception inner) : base(message, inner) { }

        /// <inheritdoc />
        protected EntityNotFoundException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}

