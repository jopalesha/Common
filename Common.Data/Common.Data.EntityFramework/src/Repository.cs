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
using Jopalesha.Helpers.Extensions;
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
        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity, TKey}"/> class.
        /// </summary>
        /// <param name="context">Data context.</param>
        protected Repository(DbContext context)
        {
            Context = context;
            Set = context.Set<TEntity>();
            Query = AddIncludes(Set.AsQueryable());
        }

        /// <summary>
        /// Gets context.
        /// </summary>
        protected DbContext Context { get; }

        /// <summary>
        /// Gets data set.
        /// </summary>
        protected DbSet<TEntity> Set { get; }

        /// <summary>
        /// Gets query.
        /// </summary>
        protected IQueryable<TEntity> Query { get; }

        /// <summary>
        /// Gets expression with properties, which should be included to query.
        /// </summary>
        /// <returns>Expression with included properties.</returns>
        protected virtual Expression<Func<TEntity, object>>[] Includes => Array.Empty<Expression<Func<TEntity, object>>>();

        /// <inheritdoc />
        public async Task<TEntity> GetAsync(TKey id, CancellationToken token) =>
            await FindAsync(id, token) ?? throw new EntityNotFoundException<TEntity>(id);

        /// <inheritdoc />
        public TEntity Get(TKey id) => Find(id) ?? throw new EntityNotFoundException<TEntity>(id);

        /// <inheritdoc />
        public virtual TEntity Add(TEntity entity) => Set.Add(entity).Entity;

        /// <inheritdoc />
        public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken token) =>
            (await Set.AddAsync(entity, token)).Entity;

        /// <inheritdoc />
        public virtual void AddRange(IEnumerable<TEntity> entities) => Set.AddRange(entities);

        /// <inheritdoc />
        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities) => await Set.AddRangeAsync(entities);

        /// <inheritdoc />
        public virtual TEntity Single(Func<TEntity, bool> predicate) =>
            SingleOrDefault(predicate) ?? throw new EntityNotFoundException<TEntity>("There is no entity by predicate");

        /// <inheritdoc />
        public virtual async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken token) =>
            await SingleOrDefaultAsync(predicate, token) ?? throw new EntityNotFoundException<TEntity>($"There is no entity for predicate {predicate.Body}");

        /// <inheritdoc />
        public TEntity SingleOrDefault(Func<TEntity, bool> predicate) => AddIncludes(Set).SingleOrDefault(predicate);

        /// <inheritdoc />
        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken token) =>
            await AddIncludes(Set).SingleOrDefaultAsync(predicate, token);

        /// <inheritdoc />
        public async Task<ListResult<TEntity>> GetAll(
            Func<IQueryable<TEntity>, IQueryable<TEntity>> formQuery,
            CancellationToken token)
        {
            var query = formQuery(AddIncludes(Set.AsNoTracking()));
            var count = await query.CountAsync(token);
            var items = await query.ToListAsync(token);

            return new ListResult<TEntity>(items, count);
        }

        /// <inheritdoc />
        public bool Exists(TKey key) => Any(it => it.Id.Equals(key));

        /// <inheritdoc />
        public async Task<bool> ExistsAsync(TKey key, CancellationToken token) =>
            await AnyAsync(it => it.Id.Equals(key), token);


        /// <inheritdoc />
        public bool Any(Expression<Func<TEntity, bool>> predicate) => Set.Any(predicate);


        /// <inheritdoc />
        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken token) =>
            await Set.AnyAsync(predicate, token);

        /// <inheritdoc />
        public int Count() => Set.Count();

        /// <inheritdoc />
        public int Count(Expression<Func<TEntity, bool>> predicate) => Set.Count(predicate);

        /// <inheritdoc />
        public Task<int> CountAsync(CancellationToken token) => Set.CountAsync(token);

        /// <inheritdoc />
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken token) =>
            await Set.CountAsync(predicate, token);

        /// <inheritdoc />
        public IEnumerable<TEntity> GetAll() => Query.ToList();

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken token) =>
            await Query.ToListAsync(token);

        /// <inheritdoc />
        public void Remove(TEntity entity) => Set.Remove(entity);

        /// <inheritdoc />
        public void Remove(TKey id) => Remove(Get(id));

        /// <inheritdoc />
        public async Task RemoveAsync(TKey key, CancellationToken token) => Remove(await GetAsync(key, token));

        /// <inheritdoc />
        public TEntity Find(TKey id) => Set.Find(id);

        /// <inheritdoc />
        public async Task<TEntity> FindAsync(TKey id, CancellationToken token) =>
            await Set.FindAsync(new object[] { id }, token);

        private IQueryable<TEntity> AddIncludes(IQueryable<TEntity> set)
        {
            if (!Includes.IsNullOrEmpty())
            {
                set = Includes.Aggregate(set, (current, include) => current.Include(include));
            }

            return set;
        }
    }
}
