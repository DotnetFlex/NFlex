using System.Linq;

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
