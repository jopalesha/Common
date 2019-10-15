using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Jopalesha.Common.Domain;
using Jopalesha.Common.Domain.Models;
using Jopalesha.Common.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Jopalesha.Common.Data.EntityFramework
{
    public abstract class Repository<TEntity, TKey> : IRepository<TEntity, TKey> 
        where TEntity : class, IHasId<TKey>
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _set;
        protected readonly IQueryable<TEntity> _query;

        protected Repository(DbContext context)
        {
            _context = context;
            _set = context.Set<TEntity>();
            _query = AddIncludes(_set.AsQueryable());
        }

        public async Task<TEntity> Get(TKey id, CancellationToken token)
        {
            return FindOrThrow(await _query.SingleOrDefaultAsync(it => it.Id.Equals(id), token), id);
        }

        public TEntity Get(TKey id)
        {
            return FindOrThrow(_query.SingleOrDefault(it => it.Id.Equals(id)), id);
        }

        public virtual TEntity Add(TEntity entity)
        {
            return _set.Add(entity).Entity;
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken token)
        {
            return (await _set.AddAsync(entity, token)).Entity;
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            _set.AddRange(entities);
        }

        public virtual TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _query.SingleOrDefault(predicate);
        }

        public IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate, int? takeCount = null)
        {
            var query = _query.Where(predicate);

            if (takeCount.HasValue)
            {
                query = query.Take(takeCount.Value);
            }

            return query.ToList();
        }

        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return _set.Any(predicate);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _query.ToList();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _query.ToListAsync(cancellationToken);
        }

        public async Task Remove(TKey key, CancellationToken token)
        {
            var item = await _set.FindAsync(new object[] {key}, token);
            if (item != null)
            {
                _set.Remove(item);
            }
        }

        public async Task<ListResult<TEntity>> GetAll(Query<TEntity> query, CancellationToken token)
        {
            var queryable = FormQueryable(query);
            var count = queryable.CountAsync(token);

            if (query.Count != 0)
            {
                queryable = queryable.Take(query.Count);
            }

            var items = queryable.ToListAsync(token);

            return new ListResult<TEntity>(await items, await count);
        }

        protected async Task<TEntity> FindAsync(object id, CancellationToken token)
        {
            return await _set.FindAsync(new[] {id}, token);
        }

        private static TEntity FindOrThrow(TEntity entity, TKey id)
        {
            if (entity == null)
            {
                // TODO CREATE REPOSITORY EXCEPTION
                throw new ArgumentException($"There is no item for type {typeof(TEntity)} with {id}");
            }

            return entity;
        }

        protected virtual Expression<Func<TEntity, object>>[] GetIncludes => null;

        private IQueryable<TEntity> AddIncludes(IQueryable<TEntity> set)
        {
            var includes = GetIncludes;

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
