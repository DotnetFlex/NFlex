using NFlex.Core.Query;
using NFlex.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NFlex.Core
{
    public interface IRepository<TAggregateRoot,in TKey>: IPerLifetimeDependency where TAggregateRoot:class,IAggregateRoot<TKey>
    {
        /// <summary>
        /// 查找实体集合
        /// </summary>
        IQueryable<TAggregateRoot> Queryable { get; }

        /// <summary>
        /// 查找实体集合并贪婪加载指定属性
        /// </summary>
        IQueryable<TAggregateRoot> QueryableIncluding(params Expression<Func<TAggregateRoot, object>>[] includeProperties);

        /// <summary>
        /// 查找实体集合
        /// </summary>
        /// <param name="predicate">条件</param>
        IQueryable<TAggregateRoot> QueryableAsNoTracking { get; }

        IPagerSelector<TAggregateRoot> Query(IQuery<TAggregateRoot> query, params Expression<Func<TAggregateRoot, object>>[] includeProperties);

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        void Add(TAggregateRoot entity);

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entities">实体</param>
        void Add(IEnumerable<TAggregateRoot> entities);

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="entity">实体</param>
        void Update(TAggregateRoot entity);


        void Remove(TKey key, bool ignoreSoftDelete = false);

        /// <summary>
        /// 移除实体
        /// </summary>
        /// <param name="entity">实体</param>
        void Remove(TAggregateRoot entity, bool ignoreSoftDelete = false);

        /// <summary>
        /// 移除实体集合
        /// </summary>
        /// <param name="entities">实体集合</param>
        void Remove(IEnumerable<TAggregateRoot> entities, bool ignoreSoftDelete = false);

        /// <summary>
        /// 移除实体
        /// </summary>
        /// <param name="predicate">条件</param>
        void Remove(Expression<Func<TAggregateRoot, bool>> predicate, bool ignoreSoftDelete = false);

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="id">实体标示</param>
        /// <returns></returns>
        TAggregateRoot Single(TKey id);

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="predicate">条件</param>
        TAggregateRoot Single(Expression<Func<TAggregateRoot, bool>> predicate, params Expression<Func<TAggregateRoot, object>>[] includeProperties);

        /// <summary>
        /// 获取实体个数
        /// </summary>
        /// <param name="predicate">条件</param>
        int Count(Expression<Func<TAggregateRoot, bool>> predicate = null);

        /// <summary>
        /// 判断实体是否存在
        /// </summary>
        /// <param name="predicate">条件</param>
        bool Exists(Expression<Func<TAggregateRoot, bool>> predicate);
    }

    public interface IRepository<TAggregateRoot>:IRepository<TAggregateRoot,Guid> where TAggregateRoot : class, IAggregateRoot<Guid> { }
}
