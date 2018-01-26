using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Core.Query
{
    public abstract class BaseQuery<TEntity> : IQuery<TEntity>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        protected FilterBuilder<TEntity> Filter { get; set; }

        public FilterBuilder<TEntity> GetFilter()
        {
            Filter = new FilterBuilder<TEntity>();
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
