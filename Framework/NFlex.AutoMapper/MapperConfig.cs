using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace NFlex.AutoMapper
{
    public static class MapperConfig
    {
        private static IEnumerable<Assembly> _assemblies;
        /// <summary>
        /// 需要跳过的程序集列表
        /// </summary>
        private const string AssemblySkipLoadingPattern = "^System|^mscorlib|^Microsoft|^AjaxControlToolkit|^Antlr3|^Autofac|^NSubstitute|^AutoMapper|^Castle|^ComponentArt|^CppCodeProvider|^DotNetOpenAuth|^EntityFramework|^EPPlus|^FluentValidation|^ImageResizer|^itextsharp|^log4net|^MaxMind|^MbUnit|^MiniProfiler|^Mono.Math|^MvcContrib|^Newtonsoft|^NHibernate|^nunit|^Org.Mentalis|^PerlRegex|^QuickGraph|^Recaptcha|^Remotion|^RestSharp|^Telerik|^Iesi|^TestFu|^UserAgentStringLibrary|^VJSharpCodeProvider|^WebActivator|^WebDev|^WebGrease|^xunit";

        public static void Initialize()
        {
            _assemblies = AppDomain.CurrentDomain.GetAssemblies();//_assemblies = Reflection.GetAssemblies(AppDomain.CurrentDomain.BaseDirectory);
            _assemblies = FilterSystemAssembly(_assemblies);

            var mappers = new List<IMap>();
            foreach (var ass in _assemblies)
                mappers.AddRange(Reflection.GetTypesByInterface<IMap>(ass));

            
            Mapper.Initialize(config =>
            {
                foreach (var mapper in mappers)
                {
                    mapper.Register(config);
                }

            });
        }

        private static Assembly[] FilterSystemAssembly(IEnumerable<Assembly> assemblies)
        {
            return assemblies
                .Where(assembly => !Regex.IsMatch(assembly.FullName, AssemblySkipLoadingPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled))
                .ToArray();
        }
    }
}
