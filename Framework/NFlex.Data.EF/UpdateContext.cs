using NFlex.Core;
using System;
using System.Linq;
using System.Linq.Expressions;
using EntityFramework.Extensions;

namespace NFlex.Data.EF
{
    public class UpdateContext<T> : IUpdateContext<T> where T:class
    {
        private IQueryable<T> _queryable;
        public UpdateContext(IQueryable<T> query)
        {
            _queryable = query;
        }

        public void Set(Expression<Func<T, T>> updateExpression)
        {
            _queryable.Update(updateExpression);
        }
    }
}
