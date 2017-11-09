using NFlex.Core.Ioc;
using NFlex.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Core
{
    public interface ISqlExecuter: IPerWebRequestDependency
    {
        List<T> ExecuteQuery<T>(string sql);
        int ExecuteCommand(string sql);
        T ExecuteScalar<T>(string sql);
        void BulkInsert<T>(IList<T> list);


        IUpdateContext<T> Update<T>(Expression<Func<T, bool>> predicate) where T : class;
        int Delete<T>(Expression<Func<T, bool>> predicate) where T : class;
    }
}
