using NFlex.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Caching
{
    public interface ICacheProvider: ITransientDependency
    {
        ICache Instance();
    }
}
