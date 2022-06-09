using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Jopalesha.Common.Domain;
using Jopalesha.Common.Domain.Exceptions;
using Jopalesha.Common.Domain.Models;
using Jopalesha.Common.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Jopalesha.Common.Data.EntityFramework
{
    /// <summary>
    /// Base repository class.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    /// <typeparam name="TKey">Entity key type.</typeparam>
    public abstract class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _set;
        protected readonly IQueryable<TEntity> _query;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity, TKey}"/> class.
        /// </summary>
        /// <param name="context">Data context.</param>
        protected Repository(DbContext context)
        {
            _context = context;
            _set = context.Set<TEntity>();
            _query = AddIncludes(_set.AsQueryable());
        }

        /// <inheritdoc />
        public async Task<TEntity> Get(TKey id, CancellationToken token) =>
            FindOrThrow(await _query.SingleOrDefaultAsync(it => it.Id.Equals(id), token), id);

        /// <inheritdoc />
        public TEntity Get(TKey id) => FindOrThrow(_query.SingleOrDefault(it => it.Id.Equals(id)), id);

        /// <inheritdoc />
        public virtual TEntity Add(TEntity entity) => _set.Add(entity).Entity;

        /// <inheritdoc />
        public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken token) => (await _set.AddAsync(entity, token)).Entity;

        public virtual void AddRange(IEnumerable<TEntity> entities) => _set.AddRange(entities);

        /// <inheritdoc />
        public async Task<ListResult<TEntity>> GetAll(Action<IQueryable<TEntity>> formQuery, CancellationToken token)
        {
            var query = _set.AsQueryable().AsNoTracking();
            formQuery(query);
            var count = await query.CountAsync(token);
            var items = await query.ToListAsync(token);

            return new ListResult<TEntity>(items, count);
        }

        /// <inheritdoc />
        public async Task<bool> ExistsAsync(TKey key, CancellationToken token) => await AnyAsync(it => it.Id.Equals(key), token);

        /// <inheritdoc />
        public bool Exists(TKey key) => Any(it => it.Id.Equals(key));

        /// <inheritdoc />
        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken token) =>
            await _set.AnyAsync(predicate, token);

        /// <inheritdoc />
        public bool Any(Expression<Func<TEntity, bool>> predicate) => _set.Any(predicate);

        /// <inheritdoc />
        public IEnumerable<TEntity> GetAll() => _query.ToList();

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken token) => await _query.ToListAsync(token);

        /// <inheritdoc />
        public async Task Remove(TKey key, CancellationToken token)
        {
            var item = await _set.FindAsync(new object[] {key}, token);
            if (item != null)
            {
                _set.Remove(item);
            }
        }

        public async Task<TEntity> FindAsync(TKey id, CancellationToken token) =>
            await _set.FindAsync(new[] { id }, token);

        private static TEntity FindOrThrow(TKey id)
        {
            var entity=Find(id)

            if (entity == null)
            {
                // TODO CREATE REPOSITORY EXCEPTION
                throw new EntityNotFoundException<TEntity,TKey>(id);
            }

            return entity;
        }

        /// <summary>
        /// Get expression with properties, which should be included to query.
        /// </summary>
        /// <returns>Expression with included properties.</returns>
        protected virtual Expression<Func<TEntity, object>>[] GetIncludes() => Array.Empty<Expression<Func<TEntity, object>>>();

        private IQueryable<TEntity> AddIncludes(IQueryable<TEntity> set)
        {
            var includes = GetIncludes();

            if (includes != null && includes.Length > 0)
            {
                set = includes.Aggregate(set, (current, include) => current.Include(include));
            }

            return set;
        }

        private IQueryable<TEntity> FormQueryable(Query<TEntity> query)
        {
            var result = _set.AsQueryable();

            if (query.WhereExpression != null)
            {
                result = result.Where(query.WhereExpression);
            }

            if (query.IncludeExpressions.Count > 0)
            {
                result = query.IncludeExpressions.Aggregate(result, (current, include) => current.Include(include));
            }

            if (query.Offset != 0)
            {
                result = result.Skip(query.Offset);
            }

            return result;
        }
    }
}
