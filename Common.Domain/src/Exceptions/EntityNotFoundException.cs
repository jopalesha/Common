namespace Jopalesha.Common.Domain.Exceptions;

/// <summary>
/// Entity not found exception.
/// </summary>
/// <typeparam name="TEntity">Entity type.</typeparam>
[Serializable]
public class EntityNotFoundException<TEntity> : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EntityNotFoundException{TEntity}"/> class.
    /// </summary>
    /// <param name="id">Identifier.</param>
    public EntityNotFoundException(object id) : this($"There is no {nameof(TEntity)} entity with id {id}")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityNotFoundException{TEntity}"/> class.
    /// </summary>
    /// <param name="message">Error message.</param>
    public EntityNotFoundException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityNotFoundException{TEntity}"/>
    /// class, with inner exception.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <param name="inner">Inner exception.</param>
    public EntityNotFoundException(string message, Exception inner) : base(message, inner)
    {
    }
}
