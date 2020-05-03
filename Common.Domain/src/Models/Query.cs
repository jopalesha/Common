using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Jopalesha.CheckWhenDoIt;
using Jopalesha.Helpers.Extensions;

namespace Jopalesha.Common.Domain.Models
{
    public class Query<TEntity>
    {
        private readonly List<Expression<Func<TEntity, object>>> _includeExpressions = new List<Expression<Func<TEntity, object>>>();

        public int Offset { get; private set; }

        public int Count { get; private set; }

        public Expression<Func<TEntity, bool>> WhereExpression { get; private set; }

        public IReadOnlyList<Expression<Func<TEntity, object>>> IncludeExpressions => _includeExpressions;

        public Query<TEntity> Where(Expression<Func<TEntity, bool>> expression)
        {
            Check.NotNull(expression);
            WhereExpression = WhereExpression == null ? expression : WhereExpression.And(expression);
            return this;
        }

        public Query<TEntity> Include(Expression<Func<TEntity, object>> expression)
        {
            _includeExpressions.Add(Check.NotNull(expression));
            return this;
        }

        public Query<TEntity> Skip(int count)
        {
            Offset = Check.True(count, It.IsPositive.Integer);
            return this;
        }

        public Query<TEntity> Take(int count)
        {
            Count = Check.True(count, It.IsPositive.Integer);
            return this;
        }
    }
}
