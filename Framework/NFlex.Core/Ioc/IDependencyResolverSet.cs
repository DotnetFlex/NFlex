using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Core.Ioc
{
    public interface IDependencyResolverSet
    {
        void SetResolver(IContainer container);
    }
}
