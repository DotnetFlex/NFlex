using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Core.Query
{
    public interface IQuery<TEntity>
    {
        int PageIndex { get; set; }
        int PageSize { get; set; }

        FilterBuilder<TEntity> GetFilter();

        int GetSkip();

        IQueryable<TEntity> Sort(IQueryable<TEntity> queryable);
    }
}
