using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Extras.DynamicProxy;
using NFlex.Core.Interceptors;

namespace NFlex.Core.Ioc
{
    public class InterceptorDependencyRegistrar : IDependencyRegistrar
    {
        public void Register(Assembly[] ass, ContainerBuilder builder)
        {

            builder.RegisterAssemblyTypes(ass)
                .Where(t => typeof(IService).IsAssignableFrom(t) && !t.IsAbstract)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .PropertiesAutowired()
                .InterceptedBy(typeof(ValidateInterceptor))
                .EnableInterfaceInterceptors()
                .PreserveExistingDefaults();

            builder.Register(t => new ValidateInterceptor());
        }
    }
}
