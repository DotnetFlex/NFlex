using System.Web.Compilation;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using NFlex.Ioc;
using Autofac;
using System;

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
                .PropertiesAutowired();

            builder.RegisterAssemblyTypes(ass)
                .Where(t => typeof(IPerDependency).IsAssignableFrom(t) && !t.IsAbstract)
                .AsImplementedInterfaces()
                .InstancePerDependency()
                .PropertiesAutowired();

            builder.RegisterAssemblyTypes(ass)
                .Where(t => typeof(ISingletonDependency).IsAssignableFrom(t) && !t.IsAbstract)
                .AsImplementedInterfaces()
                .SingleInstance()
                .PropertiesAutowired();

            builder.RegisterAssemblyTypes(ass)
                .Where(t => typeof(IPerLifetimeDependency).IsAssignableFrom(t) && !t.IsAbstract)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .PropertiesAutowired();


            builder.RegisterAssemblyTypes(ass)
                .Where(t => typeof(IPerRequestDependency).IsAssignableFrom(t) && !t.IsAbstract)
                .InstancePerRequest()
                .PropertiesAutowired();

            builder.RegisterAssemblyTypes(ass)
                .Where(t => typeof(IPerDependency).IsAssignableFrom(t) && !t.IsAbstract)
                .InstancePerDependency()
                .PropertiesAutowired();

            builder.RegisterAssemblyTypes(ass)
                .Where(t => typeof(ISingletonDependency).IsAssignableFrom(t) && !t.IsAbstract)
                .SingleInstance()
                .PropertiesAutowired();

            builder.RegisterAssemblyTypes(ass)
                .Where(t => typeof(IPerLifetimeDependency).IsAssignableFrom(t) && !t.IsAbstract)
                .InstancePerLifetimeScope()
                .PropertiesAutowired();
        }
    }
}
