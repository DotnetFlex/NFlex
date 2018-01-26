using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Core.Query
{
    internal sealed class Pager<TEntity>:IPager<TEntity>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int TotalCount { get; set; }
        public List<TEntity> Records { get; set; }

        public Pager() { }

        public Pager(IQuery<TEntity> query,int total,List<TEntity> data)
        {
            TotalCount = total;
            PageIndex = query.PageIndex;
            PageSize = query.PageSize;
            PageCount = (TotalCount + PageSize - 1) / PageSize;
            Records = data;
        }

        public IPager<TResult> Convert<TResult>(Func<TEntity, TResult> converter)
        {
            var result = new Pager<TResult>();
            result.PageIndex = PageIndex;
            result.PageSize = PageSize;
            result.PageCount = PageCount;
            result.TotalCount = TotalCount;
            result.Records = Records.Select(converter).ToList();
            return result;
        }
    }
}
