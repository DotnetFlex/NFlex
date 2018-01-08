using NFlex.Core.Ioc;
using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;

using System;

namespace NFlex.Web.Mvc.Ioc
{
    public class ControllerDependencyRegistrar : IDependencyRegistrar, IDependencyResolverSet
    {
        public void Register(Assembly[] ass, ContainerBuilder builder)
        {
            builder.RegisterControllers(ass).PropertiesAutowired();
            builder.RegisterFilterProvider();
        }

        public void SetResolver(IContainer container)
        {
            var resolver = new AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(resolver);
        }
    }
}
