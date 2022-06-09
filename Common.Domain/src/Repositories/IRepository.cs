using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
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
        /// Get item by id.
        /// </summary>
        /// <param name="id">Entity identifier.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Entity if exists; otherwise, throws .</returns>
        Task<TEntity> Get(TKey id, CancellationToken token = default);

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
        /// Remove entity by key.
        /// </summary>
        /// <param name="key">Identifier.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task Remove(TKey key, CancellationToken token = default);

        /// <summary>
        /// Get entities by query.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<ListResult<TEntity>> GetAll(Action<IQueryable<TEntity>> query, CancellationToken token = default);

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
        /// Determines whether the repository contains elements that match the conditions defined by the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate delegate that defines the conditions of the elements to search for.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>true if repository contains one or more elements that match the conditions defined by the specified predicate;
        /// otherwise, false.</returns>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken token = default);

        /// <summary>
        ///  Determines whether the repository contains elements that match the conditions defined by the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate delegate that defines the conditions of the elements to search for.</param>
        /// <returns>true if repository contains one or more elements that match the conditions defined by the specified predicate;
        /// otherwise, false.</returns>
        bool Any(Expression<Func<TEntity, bool>> predicate);
    }
}
