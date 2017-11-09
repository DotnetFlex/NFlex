using NFlex.Core.Ioc;
using Castle.Windsor;
using System.Reflection;
using Castle.MicroKernel.Registration;
using System.Web.Mvc;

namespace NFlex.Web.Mvc.Ioc
{
    public class ControllerDependencyRegistrar : IDependencyRegistrar
    {
        public void Register(Assembly ass, IWindsorContainer iocContainer)
        {
            iocContainer.Register(Classes.FromAssembly(ass)
                .BasedOn<Controller>()
                    .LifestyleTransient()
                );

            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(iocContainer.Kernel));
        }
    }
}
