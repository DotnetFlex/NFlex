using System;
using System.Linq;
using System.Linq.Expressions;

namespace NFlex.Core.Query
{
    public abstract class BaseQuery<TEntity> : IQuery<TEntity>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        protected FilterBuilder<TEntity> Filter { get; set; } = new FilterBuilder<TEntity>();

        public void AppendFilter(Expression<Func<TEntity, bool>> expr)
        {
            Filter.And(expr);
        }

        public FilterBuilder<TEntity> GetFilter()
        {
            BuildFilter();
            if (PageIndex < 1) PageIndex = 1;
            if (PageSize == 0) PageSize = 20;
            return Filter;
        }

        public int GetSkip()
        {
            return PageSize * (PageIndex - 1);
        }

        public abstract IQueryable<TEntity> Sort(IQueryable<TEntity> queryable);

        protected virtual void BuildFilter() { }
        
    }
}
