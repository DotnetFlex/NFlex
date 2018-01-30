using Autofac;

namespace NFlex.Core.Ioc
{
    public interface IDependencyResolverSet
    {
        void SetResolver(IContainer container);
    }
}
