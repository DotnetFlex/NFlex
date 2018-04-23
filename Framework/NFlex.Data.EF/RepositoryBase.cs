using System;
using System.Collections.Generic;
using System.Linq;
using NFlex.Core;
using System.Data.Entity;
using System.Linq.Expressions;
using NFlex.Core.Query;

namespace NFlex.Data.EF
{
    public abstract class RepositoryBase<TAggregateRoot,TKey>:IRepository<TAggregateRoot,TKey> where TAggregateRoot:class,IAggregateRoot<TKey>
    {
        protected IDbContext _dbContext { get; private set; }

        protected RepositoryBase(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private DbSet<TAggregateRoot> Set
        {
            get { return _dbContext.Set<TAggregateRoot>(); }
        }

        /// <summary>
        /// 查找实体集合
        /// </summary>
        public IQueryable<TAggregateRoot> Queryable
        {
            get { return Set; }
        }

        /// <summary>
        /// 查找实体集合并贪婪加载指定属性
        /// </summary>
        public IQueryable<TAggregateRoot> QueryableIncluding(params Expression<Func<TAggregateRoot, object>>[] includeProperties)
        {
            var query = Queryable;
            foreach(var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        /// <summary>
        /// 查找实体集合
        /// </summary>
        public IQueryable<TAggregateRoot> QueryableAsNoTracking {
            get { return Set.AsNoTracking(); }
        }

        public IPagerSelector<TAggregateRoot> Query(IQuery<TAggregateRoot> query, params Expression<Func<TAggregateRoot, object>>[] includeProperties)
        {
            var total = Queryable.Where(query.GetFilter()).Count();

            var queryable = Queryable;
            foreach (var includeProperty in includeProperties)
            {
                queryable = queryable.Include(includeProperty);
            }
            queryable= queryable.Where(query.GetFilter());
            queryable =query.Sort(queryable);
            queryable=queryable.Skip(query.GetSkip()).Take(query.PageSize);

            var pager = new PagerSelector<TAggregateRoot>(query, total, queryable);

            return pager;
        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        public void Add(TAggregateRoot entity)
        {
            Set.Add(entity);
        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entities">实体</param>
        public void Add(IEnumerable<TAggregateRoot> entities)
        {
            Set.AddRange(entities);
        }

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="entity">实体</param>
        public void Update(TAggregateRoot entity)
        {
            IsAttach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(TKey id, bool ignoreSoftDelete = false)
        {
            var entity = Single(id);
            Remove(entity, ignoreSoftDelete);
        }

        /// <summary>
        /// 移除实体
        /// </summary>
        /// <param name="entity">实体</param>
        public void Remove(TAggregateRoot entity ,bool ignoreSoftDelete=false)
        {
            if (entity is ISoftDelete && !ignoreSoftDelete)
                (entity as ISoftDelete).IsDeleted = true;
            else
                Set.Remove(entity);
        }

        /// <summary>
        /// 移除实体集合
        /// </summary>
        /// <param name="entities">实体集合</param>
        public void Remove(IEnumerable<TAggregateRoot> entities, bool ignoreSoftDelete = false)
        {
            if (entities == null) return;

            var list = entities.ToList();
            if (!list.Any()) return;

            foreach (var entity in list)
                Remove(entity,ignoreSoftDelete);
        }

        /// <summary>
        /// 移除实体
        /// </summary>
        /// <param name="predicate">条件</param>
        public void Remove(Expression<Func<TAggregateRoot, bool>> predicate, bool ignoreSoftDelete = false)
        {
            var entities = Set.Where(predicate);
            Remove(entities,ignoreSoftDelete);
        }

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="id">实体标示</param>
        /// <returns></returns>
        public TAggregateRoot Single(TKey id)
        {
            return Set.Find(id);
        }

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="predicate">条件</param>
        public TAggregateRoot Single(Expression<Func<TAggregateRoot, bool>> predicate, params Expression<Func<TAggregateRoot, object>>[] includeProperties)
        {
            var query = QueryableIncluding(includeProperties);
            return query.FirstOrDefault(predicate);
        }

        /// <summary>
        /// 获取实体个数
        /// </summary>
        /// <param name="predicate">条件</param>
        public int Count(Expression<Func<TAggregateRoot, bool>> predicate = null)
        {
            return predicate == null ? Set.Count() : Set.Where(predicate).Count();
        }

        /// <summary>
        /// 判断实体是否存在
        /// </summary>
        /// <param name="predicate">条件</param>
        public bool Exists(Expression<Func<TAggregateRoot, bool>> predicate)
        {
            return Set.Any(predicate);
        }

        protected virtual bool IsAttach(TAggregateRoot entity)
        {
            if (!Set.Local.Contains(entity))
            {
                Set.Attach(entity);
                return false;
            }
            return true;
        }
    }

    public abstract class RepositoryBase<TAggregateRoot>:RepositoryBase<TAggregateRoot,Guid>
        where TAggregateRoot:class,IAggregateRoot<Guid>
    {
        protected RepositoryBase(IDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
