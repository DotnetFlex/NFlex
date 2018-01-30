using System.Linq;
using System.Reflection;
using NFlex.Ioc;
using Autofac;

namespace NFlex.Core.Ioc
{
    public class CommonDependencyRegistrar : IDependencyRegistrar
    {
        public void Register(Assembly[] ass, ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ass)
                .Where(t => typeof(IPerRequestDependency).IsAssignableFrom(t) && !t.IsAbstract)
                .AsImplementedInterfaces()
                .InstancePerRequest()
                .PropertiesAutowired()
                .PreserveExistingDefaults();

            builder.RegisterAssemblyTypes(ass)
                .Where(t => typeof(IPerDependency).IsAssignableFrom(t) && !t.IsAbstract)
                .AsImplementedInterfaces()
                .InstancePerDependency()
                .PropertiesAutowired()
                .PreserveExistingDefaults();

            builder.RegisterAssemblyTypes(ass)
                .Where(t => typeof(ISingletonDependency).IsAssignableFrom(t) && !t.IsAbstract)
                .AsImplementedInterfaces()
                .SingleInstance()
                .PropertiesAutowired()
                .PreserveExistingDefaults();

            builder.RegisterAssemblyTypes(ass)
                .Where(t => typeof(IPerLifetimeDependency).IsAssignableFrom(t) && !t.IsAbstract)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .PropertiesAutowired()
                .PreserveExistingDefaults();


            builder.RegisterAssemblyTypes(ass)
                .Where(t => typeof(IPerRequestDependency).IsAssignableFrom(t) && !t.IsAbstract)
                .InstancePerRequest()
                .PropertiesAutowired()
                .PreserveExistingDefaults();

            builder.RegisterAssemblyTypes(ass)
                .Where(t => typeof(IPerDependency).IsAssignableFrom(t) && !t.IsAbstract)
                .InstancePerDependency()
                .PropertiesAutowired()
                .PreserveExistingDefaults();

            builder.RegisterAssemblyTypes(ass)
                .Where(t => typeof(ISingletonDependency).IsAssignableFrom(t) && !t.IsAbstract)
                .SingleInstance()
                .PropertiesAutowired()
                .PreserveExistingDefaults();

            builder.RegisterAssemblyTypes(ass)
                .Where(t => typeof(IPerLifetimeDependency).IsAssignableFrom(t) && !t.IsAbstract)
                .InstancePerLifetimeScope()
                .PropertiesAutowired()
                .PreserveExistingDefaults();

            
        }
    }
}
