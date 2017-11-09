using NFlex.Core.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Windsor;
using System.Reflection;
using Castle.MicroKernel.Registration;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace NFlex.Web.Api.Ioc
{
    public class ApiControllerDependencyRegistrar : IDependencyRegistrar
    {
        public ApiControllerDependencyRegistrar()
        {
            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), new ApiControllerActivator());
        }

        public void Register(Assembly ass, IWindsorContainer iocContainer)
        {
            iocContainer.Register(
                Classes.FromAssembly(ass)
                .BasedOn<ApiController>()
                .LifestyleTransient()
                );
        }
    }
}
