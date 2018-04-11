using System;
using System.Collections.Generic;

namespace NFlex.Core.Query
{
    public interface IPager<TEntity>
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        int PageIndex { get;}

        /// <summary>
        /// 每页包含的记录数
        /// </summary>
        int PageSize { get;}

        /// <summary>
        /// 总页数
        /// </summary>
        int PageCount { get; }

        /// <summary>
        /// 总记录数
        /// </summary>
        int TotalCount { get;}

        /// <summary>
        /// 记录列表
        /// </summary>
        List<TEntity> Records { get;  }

        //IPager<TResult> Convert<TResult>(Func<TEntity, TResult> converter);
    }
}
