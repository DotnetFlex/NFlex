using NFlex.Core.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using Autofac;
using Autofac.Integration.WebApi;

namespace NFlex.Web.Api.Ioc
{
    public class ApiControllerDependencyRegistrar : IDependencyRegistrar, IDependencyResolverSet
    {

        public void Register(Assembly[] ass, ContainerBuilder builder)
        {
            builder.RegisterApiControllers(ass).AsSelf().PropertiesAutowired();
            builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);
        }

        public void SetResolver(IContainer container)
        {
            var webApiResolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = webApiResolver;
        }
    }
}
