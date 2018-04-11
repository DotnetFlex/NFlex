using NFlex.Core.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Data.EF
{
    internal class PagerSelector<TEntity>: IPagerSelector<TEntity>
    {
        private IQuery<TEntity> _query;
        private int _total;
        private IQueryable<TEntity> _queryable;
        public PagerSelector(IQuery<TEntity> query, int total, IQueryable<TEntity> queryable)
        {
            _query = query;
            _total = total;
            _queryable = queryable;
        }

        public IPager<TResult> Select<TResult>(Expression<Func<TEntity, TResult>> selector)
        {
            var recoreds = _queryable.Select(selector).ToList();
            return new Pager<TResult>(_query.PageIndex,_query.PageSize, _total, recoreds);
        }

        public IPager<TEntity> ToPager()
        {
            var recoreds = _queryable.ToList();
            return new Pager<TEntity>(_query.PageIndex, _query.PageSize, _total, recoreds);
        }

        public IPager<TResult> Convert<TResult>(Func<TEntity, TResult> converter)
        {
            var recoreds = _queryable.Select(converter).ToList();//.Select(converter).ToList();
            return new Pager<TResult>(_query.PageIndex, _query.PageSize, _total, recoreds);
        }
    }
}
