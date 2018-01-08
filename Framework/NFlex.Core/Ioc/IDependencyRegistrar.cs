using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Core.Ioc
{
    public interface IDependencyRegistrar
    {
        void Register(Assembly[] ass, ContainerBuilder builder);
    }
}
