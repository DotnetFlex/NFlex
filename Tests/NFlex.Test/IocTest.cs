using NFlex.Core.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using System.Reflection;
using NFlex.Ioc;
using Xunit;
using NFlex.Logging.Log4Net;

namespace NFlex.Test
{
    public class IocTest
    {
        [Fact]
        public void Test1()
        {
            IocContainer.Initialize(false);
            var obj = IocContainer.Create<TestClassA>();
            var obj2 = IocContainer.Create<TestClassB>();
        }
    }

    public class TestClassA: IPerLifetimeDependency
    {
        public TestClass AClass { get; set; }
    }


    public class TestClassB : IPerLifetimeDependency
    {
        public TestClass BClass { get; set; }
    }

    public class TestClass: IPerLifetimeDependency
    {
        public Guid TracId { get; set; }
        public TestClass()
        {
            TracId = Guid.NewGuid();
        }
    }

    public class registerar : IDependencyRegistrar
    {
        public void Register(Assembly[] ass, ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ass).Where(t => typeof(IPerLifetimeDependency).IsAssignableFrom(t)).InstancePerLifetimeScope().PropertiesAutowired();
        }
    }
}
