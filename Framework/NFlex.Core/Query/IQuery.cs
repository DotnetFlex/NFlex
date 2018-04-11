using System;
using System.Linq;
using System.Linq.Expressions;

namespace NFlex.Core.Query
{
    public interface IQuery<TEntity>
    {
        int PageIndex { get; set; }
        int PageSize { get; set; }

        FilterBuilder<TEntity> GetFilter();

        int GetSkip();

        IQueryable<TEntity> Sort(IQueryable<TEntity> queryable);

        void AppendFilter(Expression<Func<TEntity, bool>> expr);
    }
}
