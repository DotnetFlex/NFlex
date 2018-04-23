using Autofac;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Compilation;

namespace NFlex.Core.Ioc
{
    public static class IocContainer
    {
        private static IContainer _container;
        private static ContainerBuilder _builder;
        private const string AssemblySkipLoadingPattern = "^System|^mscorlib|^Microsoft|^AjaxControlToolkit|^Antlr3|^Autofac|^NSubstitute|^AutoMapper|^Castle|^ComponentArt|^CppCodeProvider|^DotNetOpenAuth|^EntityFramework|^EPPlus|^FluentValidation|^ImageResizer|^itextsharp|^log4net|^MaxMind|^MbUnit|^MiniProfiler|^Mono.Math|^MvcContrib|^Newtonsoft|^NHibernate|^nunit|^Org.Mentalis|^PerlRegex|^QuickGraph|^Recaptcha|^Remotion|^RestSharp|^Telerik|^Iesi|^TestFu|^UserAgentStringLibrary|^VJSharpCodeProvider|^WebActivator|^WebDev|^WebGrease|^xunit.";
        private static string _skipPattern = "";
        static IocContainer()
        {
            _builder = new ContainerBuilder();
        }


        public static void Initialize(bool isWeb,string skipPattern="")
        {
            _skipPattern = skipPattern;
            var assemblies =  GetAssemblies(isWeb);
            foreach(var ass in assemblies)
            {
                foreach(var reg in  Reflection.GetTypesByInterface<IDependencyRegistrar>(ass))
                {
                    reg.Register(assemblies,_builder);
                }
            }

            _container = _builder.Build();

            foreach (var ass in assemblies)
            {
                foreach (var reg in Reflection.GetTypesByInterface<IDependencyResolverSet>(ass))
                {
                    reg.SetResolver(_container);
                }
            }
        }

        public static T Create<T>() { return _container.Resolve<T>(); }

        public static object Create(Type type) { return _container.Resolve(type); }


        public static bool IsRegistred<T>() { return _container.IsRegistered<T>(); }

        public static bool IsRegistred(Type type) { return _container.IsRegistered(type); }


        private static Assembly[] GetAssemblies(bool isWeb)
        {
            if (!isWeb)
            {
                var files = Files.GetAllFiles(AppDomain.CurrentDomain.BaseDirectory).Where(t => t.EndsWith(".exe") || t.EndsWith(".dll"));
                files = files.Where(t => !Regex.IsMatch(Path.GetFileName(t), AssemblySkipLoadingPattern.TrimEnd('|')+"|" + _skipPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled));
                foreach (var file in files)
                {
                    var assemblyName = AssemblyName.GetAssemblyName(file);
                    if (!AppDomain.CurrentDomain.GetAssemblies().Any(assembly => AssemblyName.ReferenceMatchesDefinition(assemblyName, assembly.GetName())))
                        AppDomain.CurrentDomain.Load(assemblyName);
                }
            }

            var _assemblies = isWeb ? BuildManager.GetReferencedAssemblies().Cast<Assembly>() : AppDomain.CurrentDomain.GetAssemblies();
            return _assemblies
                .Where(assembly => !Regex.IsMatch(assembly.FullName, AssemblySkipLoadingPattern.TrimEnd('|')+_skipPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled))
                .ToArray();
        }
    }
}
