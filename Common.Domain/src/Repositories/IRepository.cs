using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Jopalesha.Common.Domain.Models;

namespace Jopalesha.Common.Domain.Repositories
{
    public interface IRepository<TEntity, in TKey> where TEntity : IHasId<TKey>
    {
        /// <summary>
        /// Get item by id
        /// </summary>
        /// <param name="id">Entity identificator</param>
        /// <param name="token">Cancellation token</param>
        /// <returns>Entity</returns>
        Task<TEntity> Get(TKey id, CancellationToken token = default);

        /// <summary>
        /// Add entity
        /// </summary>
        /// <param name="entity">Entity to add</param>
        /// <returns>Added entity</returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// Add entity async
        /// </summary>
        /// <param name="entity">Entity to add</param>
        /// <param name="token">Cancellation token</param>
        /// <returns>Added entity</returns>
        Task<TEntity> AddAsync(TEntity entity, CancellationToken token = default);

        /// <summary>
        /// Add items range
        /// </summary>
        /// <param name="items">Adding items</param>
        void AddRange(IEnumerable<TEntity> items);

        /// <summary>
        /// Get all items
        /// </summary>
        /// <returns>All items</returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Get all items async
        /// </summary>
        /// <param name="token">Cancellation token</param>
        /// <returns>All items</returns>
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken token = default);

        /// <summary>
        /// Remove entity
        /// </summary>
        /// <param name="key">Identificator</param>
        /// <param name="token">Cancellation token</param>
        Task Remove(TKey key, CancellationToken token = default);

        /// <summary>
        /// Get entities by query
        /// </summary>
        /// <param name="query">Search Query</param>
        /// <param name="token">Cancellation token</param>
        /// <returns></returns>
        Task<ListResult<TEntity>> GetAll(Query<TEntity> query, CancellationToken token = default);
    }
}
