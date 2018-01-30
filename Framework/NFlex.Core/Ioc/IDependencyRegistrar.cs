using Autofac;
using System.Reflection;

namespace NFlex.Core.Ioc
{
    public interface IDependencyRegistrar
    {
        void Register(Assembly[] ass, ContainerBuilder builder);
    }
}
