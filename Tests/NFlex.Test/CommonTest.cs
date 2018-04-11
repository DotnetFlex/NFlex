using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using NFlex;
using System.Linq.Expressions;
using System.Diagnostics;

namespace NFlex.Test
{
    public class CommonTest
    {
        [Fact]
        public void DynamicObjectToJsonTest()
        {;
            var a = new A();

        }
    }
    
    public class A
    {
        public string Name { get; set; }
    }
}
