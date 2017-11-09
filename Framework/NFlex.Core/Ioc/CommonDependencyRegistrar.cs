using Castle.Windsor;
using Castle.MicroKernel.Registration;
using System.Web.Compilation;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using NFlex.Ioc;

namespace NFlex.Core.Ioc
{
    public class CommonDependencyRegistrar : IDependencyRegistrar
    {
        public void Register(Assembly ass, IWindsorContainer iocContainer)
        {

            iocContainer.Register(Classes.FromAssembly(ass)
                .BasedOn<IPerWebRequestDependency>()
                .WithService.Self()
                .WithService.AllInterfaces()
                .LifestylePerWebRequest()
                );

            iocContainer.Register(Classes.FromAssembly(ass)
                .BasedOn<ITransientDependency>()
                .WithService.Self()
                .WithService.AllInterfaces()
                .LifestyleTransient()
                );

            iocContainer.Register(Classes.FromAssembly(ass)
                .BasedOn<ISingletonDependency>()
                .WithService.Self()
                .WithService.AllInterfaces()
                .LifestyleSingleton()
                );

            iocContainer.Register(Classes.FromAssembly(ass)
                .IncludeNonPublicTypes()
                .BasedOn<IInterceptor>()
                .WithService.Self()
                .LifestyleTransient()
                );
        }
    }
}
