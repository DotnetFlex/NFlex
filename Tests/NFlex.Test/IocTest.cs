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
using Castle.DynamicProxy;
using System.Diagnostics;
using Autofac.Extras.DynamicProxy;
using NFlex.Core;
using NFlex.Core.Interceptors;
using System.ComponentModel.DataAnnotations;
using NFlex.Data.EF;

namespace NFlex.Test
{
    public class IocTest
    {

        [Fact]
        public void AopTest()
        {
            IocContainer.Initialize(false);
            var test = IocContainer.Create<ITestService>();
            test.Test(new Test.dto { Name = "张三夺夺夺夺", Age = 180 });
        }
    }
    
    public interface ITestService : IService {
        void Test(dto d);
    }
    public class TestService: ITestService
    {
        public IUnitOfWork unitofwork { get; set; }
        public void Test(dto d)
        {
            //Debug.WriteLine(string.Format("测试方法:{0},{1}",d.Name,d.Age));
        }
    }

    public class DbContext:DbContextBase
    {
        public DbContext() : base("TicketCenter") { }
    }

    public class dto
    {
        [Range(18,34,ErrorMessage ="年龄应在18-34岁之间")]
        public int Age { get; set; }
        [StringLength(5,ErrorMessage ="姓名不能超过5个字符")]
        public string Name { get; set; }

        public string Custom { get; set; }
    }
}
