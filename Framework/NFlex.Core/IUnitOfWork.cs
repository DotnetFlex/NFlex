using NFlex.Core.Ioc;
using NFlex.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Core
{
    public interface IUnitOfWork:IPerWebRequestDependency
    {
        /// <summary>
        /// 提交更新
        /// </summary>
        int Commit();
    }
}
