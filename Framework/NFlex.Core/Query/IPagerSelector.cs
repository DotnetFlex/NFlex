using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Core.Query
{
    /// <summary>
    /// 分页查询结果选择器
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IPagerSelector<TEntity>
    {
        /// <summary>
        /// 转换为分页对象
        /// </summary>
        IPager<TEntity> ToPager();

        /// <summary>
        /// 将查询结果转换为指定格式
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="converter"></param>
        IPager<TResult> Convert<TResult>(Func<TEntity, TResult> converter);

        /// <summary>
        /// 在查询数据时指定查询内容与返回类型
        /// <para>
        /// （选择器中不可使用自定义方法）
        /// </para>
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        IPager<TResult> Select<TResult>(Expression<Func<TEntity, TResult>> selector);
    }
}
