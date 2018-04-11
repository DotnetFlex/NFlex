using System;
using System.Collections.Generic;
using System.Linq;

namespace NFlex.Core.Query
{
    internal sealed class Pager<TEntity>:IPager<TEntity>
    {
        public int PageIndex { get; }
        public int PageSize { get; }
        public int PageCount { get; }
        public int TotalCount { get; }
        public List<TEntity> Records { get;}

        public Pager() { }

        public Pager(int pageIndex,int pageSize,int total,List<TEntity> data)
        {
            TotalCount = total;
            PageIndex = pageIndex;
            PageSize = pageSize;
            PageCount = (TotalCount + PageSize - 1) / PageSize;
            Records = data;
        }

        //public IPager<TResult> Convert<TResult>(Func<TEntity, TResult> converter)
        //{
        //    var result = new Pager<TResult>();
        //    result.PageIndex = PageIndex;
        //    result.PageSize = PageSize;
        //    result.PageCount = PageCount;
        //    result.TotalCount = TotalCount;
        //    result.Records = Records.Select(converter).ToList();
        //    return result;
        //}
    }
}
