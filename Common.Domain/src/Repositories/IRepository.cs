using System.Linq.Expressions;
using Jopalesha.Common.Domain.Exceptions;
using Jopalesha.Common.Domain.Models;

namespace Jopalesha.Common.Domain.Repositories
{
    /// <summary>
    /// Repository.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    /// <typeparam name="TKey">Entity id type.</typeparam>
    public interface IRepository<TEntity, in TKey> where TEntity : IHasId<TKey>
    {
        /// <summary>
        /// Find entity by Id.
        /// </summary>
        /// <param name="id">Entity identifier.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Entity if exits; otherwise, null.</returns>
        Task<TEntity> FindAsync(TKey id, CancellationToken token = default);

        /// <summary>
        /// Find entity by id.
        /// </summary>
        /// <param name="id">Entity identifier.</param>
        /// <returns>Entity if exists; otherwise, null.</returns>
        TEntity Find(TKey id);

        /// <summary>
        /// Get item by id.
        /// </summary>
        /// <param name="id">Entity identifier.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Entity if exists; otherwise, throws <see cref="EntityNotFoundException{TEntity}"/>.</returns>
        /// <exception cref="EntityNotFoundException{TEntity}">Entity not exists.</exception>
        Task<TEntity> GetAsync(TKey id, CancellationToken token = default);

        /// <summary>
        /// Get item by id.
        /// </summary>
        /// <param name="id">Entity identifier.</param>
        /// <returns>Entity if exists; otherwise, throws <see cref="EntityNotFoundException{TEntity}"/> exception.</returns>
        /// <exception cref="EntityNotFoundException{TEntity}">Entity not exists.</exception>
        TEntity Get(TKey id);

        /// <summary>Returns the only element of a sequence, and throws an exception if more than one such element exists.</summary>
        /// <param name="predicate">Predicate.</param>
        /// <returns>The single element of the input sequence that satisfies a condition.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="predicate" /> is <see langword="null" />.</exception>
        /// <exception cref="InvalidOperationException">More than one element satisfies the condition in <paramref name="predicate" />.</exception>
        TEntity Single(Func<TEntity, bool> predicate);

        /// <summary>
        /// Get item by predicate.
        /// </summary>
        /// <param name="predicate">Predicate.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>The single element of the input sequence that satisfies a condition.</returns>
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken token = default);

        /// <summary>Returns the only element of a sequence, or a null value if the sequence is empty;
        /// this method throws an exception if there is more than one element in the sequence.</summary>
        /// <param name="predicate">Predicate.</param>
        /// <returns>The single element of the input sequence, or null if the sequence contains no elements.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="predicate" /> is <see langword="null" />.</exception>
        /// <exception cref="InvalidOperationException">More than one element satisfies the condition in <paramref name="predicate" />.</exception>
        TEntity SingleOrDefault(Func<TEntity, bool> predicate);

        /// <summary>Returns the only element of a sequence, or a null value if the sequence is empty;
        /// this method throws an exception if there is more than one element in the sequence.</summary>
        /// <param name="predicate">Predicate.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>The single element of the input sequence, or null if the sequence contains no elements.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="predicate" /> is <see langword="null" />.</exception>
        /// <exception cref="InvalidOperationException">More than one element satisfies the condition in <paramref name="predicate" />.</exception>
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken token = default);

        /// <summary>
        /// Add entity.
        /// </summary>
        /// <param name="entity">The entity to be added to repository.</param>
        /// <returns>Added entity.</returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// Add entity async.
        /// </summary>
        /// <param name="entity">The entity to be added to repository.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Added entity.</returns>
        Task<TEntity> AddAsync(TEntity entity, CancellationToken token = default);

        /// <summary>
        /// Add items range.
        /// </summary>
        /// <param name="items">The entities to be added to repository.</param>
        void AddRange(IEnumerable<TEntity> items);

        /// <summary>
        /// Add items range.
        /// </summary>
        /// <param name="items">The entities to be added to repository.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task AddRangeAsync(IEnumerable<TEntity> items);

        /// <summary>
        /// Get all items.
        /// </summary>
        /// <returns>Enumerable of items.</returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Get all items.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Enumerable of items.</returns>
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken token = default);

        /// <summary>
        /// Remove entity.
        /// </summary>
        /// <param name="entity">Entity.</param>
        void Remove(TEntity entity);

        /// <summary>
        /// Remove entity by key.
        /// </summary>
        /// <param name="entity">Entity.</param>
        void Remove(TKey entity);

        /// <summary>
        /// Remove entity by key.
        /// </summary>
        /// <param name="key">Identifier.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task RemoveAsync(TKey key, CancellationToken token = default);

        /// <summary>
        /// Get entities by query.
        /// </summary>
        /// <param name="query">Function, which forms query.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Entities list result.</returns>
        Task<ListResult<TEntity>> GetAll(Func<IQueryable<TEntity>, IQueryable<TEntity>> query,
            CancellationToken token = default);

        /// <summary>
        /// Determines whether the repository contains element with defined id.
        /// </summary>
        /// <param name="key">Entity id.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>true if repository contains one element with matched id; otherwise, false.</returns>
        Task<bool> ExistsAsync(TKey key, CancellationToken token = default);

        /// <summary>
        /// Determines whether the repository contains element with defined id.
        /// </summary>
        /// <param name="key">Entity id.</param>
        /// <returns>true if repository contains one element with matched id; otherwise, false.</returns>
        bool Exists(TKey key);

        /// <summary>
        ///  Determines whether the repository contains elements that match the conditions defined by the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate delegate that defines the conditions of the elements to search for.</param>
        /// <returns>true if repository contains one or more elements that match the conditions defined by the specified predicate;
        /// otherwise, false.</returns>
        bool Any(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Determines whether the repository contains elements that match the conditions defined by the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate delegate that defines the conditions of the elements to search for.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>true if repository contains one or more elements that match the conditions defined by the specified predicate;
        /// otherwise, false.</returns>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken token = default);

        /// <summary>
        /// Returns a number of elements in sequence.
        /// </summary>
        /// <returns>The number of elements in sequence.</returns>
        int Count();

        /// <summary>
        /// Returns a number of elements in sequence by predicate.
        /// </summary>
        /// <param name="predicate">Predicate.</param>
        /// <returns>The number of elements in sequence.</returns>
        public int Count(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Returns a number of elements in sequence.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>The number of elements in sequence.</returns>
        Task<int> CountAsync(CancellationToken token = default);


        /// <summary>
        /// Returns a number of elements in sequence.
        /// </summary>
        /// <param name="predicate">Predicate.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>The number of elements in sequence.</returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken token = default);
    }
}
