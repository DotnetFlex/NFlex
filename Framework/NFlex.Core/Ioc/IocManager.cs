using Castle.DynamicProxy;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NFlex.Ioc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Compilation;

namespace NFlex.Core.Ioc
{
    public class IocManager
    {
        private static IWindsorContainer _container;
        private static List<Assembly> _assemblies;
        /// <summary>
        /// 需要跳过的程序集列表
        /// </summary>
        private const string AssemblySkipLoadingPattern = "^System|^mscorlib|^Microsoft|^AjaxControlToolkit|^Antlr3|^Autofac|^NSubstitute|^AutoMapper|^Castle|^ComponentArt|^CppCodeProvider|^DotNetOpenAuth|^EntityFramework|^EPPlus|^FluentValidation|^ImageResizer|^itextsharp|^log4net|^MaxMind|^MbUnit|^MiniProfiler|^Mono.Math|^MvcContrib|^Newtonsoft|^NHibernate|^nunit|^Org.Mentalis|^PerlRegex|^QuickGraph|^Recaptcha|^Remotion|^RestSharp|^Telerik|^Iesi|^TestFu|^UserAgentStringLibrary|^VJSharpCodeProvider|^WebActivator|^WebDev|^WebGrease|^xunit.";

        public static bool IsWeb { get; set; }

        static IocManager()
        {
            IsWeb = true;
            _container = new WindsorContainer().Install();
        }

        public static T Create<T>()
        {
            return _container.Resolve<T>();
        }

        public static object Create(Type type)
        {
            return _container.Resolve(type);
        }

        public static bool IsRegistered(Type type)
        {
            return _container.Kernel.HasComponent(type);
        }

        public static bool IsRegistered<TType>()
        {
            return _container.Kernel.HasComponent(typeof(TType));
        }

        public static void Register(IDependencyRegistrar registrar)
        {
            if (_assemblies == null)
                _assemblies = GetAssemblies().ToList();

            foreach (var ass in GetAssemblies())
                registrar.Register(ass,_container);
        }

        public static void Register<TType>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton) where TType : class
        {
            _container.Register(ApplyLifestyle(Component.For<TType>(), lifeStyle));
        }

        public static void Register<TType, TImpl>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType : class
            where TImpl : class, TType
        {
            _container.Register(ApplyLifestyle(Component.For<TType, TImpl>().ImplementedBy<TImpl>(), lifeStyle));
        }

        public static void Release(object obj)
        {
            _container.Release(obj);
        }

        public static IDisposableDependencyWrapper<T> CreateAsDisposable<T>(Type type)
        {
            return new DisposableDependencyWrapper<T>((T)Create(type));
        }

        private static ComponentRegistration<T> ApplyLifestyle<T>(ComponentRegistration<T> registration, DependencyLifeStyle lifeStyle)
            where T : class
        {
            switch (lifeStyle)
            {
                case DependencyLifeStyle.Transient:
                    return registration.LifestyleTransient();
                case DependencyLifeStyle.Singleton:
                    return registration.LifestyleSingleton();
                case DependencyLifeStyle.PerWebRequest:
                    return registration.LifestylePerWebRequest();
                case DependencyLifeStyle.Scoped:
                    return registration.LifestyleScoped();
                case DependencyLifeStyle.PerThread:
                    return registration.LifestylePerThread();
                default:
                    return registration;
            }
        }

        private static Assembly[] GetAssemblies()
        {
            var _assemblies = IsWeb ? BuildManager.GetReferencedAssemblies().Cast<Assembly>() : Reflection.GetAssemblies(AppDomain.CurrentDomain.BaseDirectory);// AppDomain.CurrentDomain.GetAssemblies();
            return _assemblies
                .Where(assembly => !Regex.IsMatch(assembly.FullName, AssemblySkipLoadingPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled))
                .ToArray();
        }
    }
}
