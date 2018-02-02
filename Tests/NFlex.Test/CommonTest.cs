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
            dynamic obj = new DynamicObject();
            obj.Title = "aaa";
            obj["Test"] = "Test";
            var json = Json.ToJson(obj);

        }
    }
    
    public enum TestEnum
    {
        a=1,
        b=2,
        c=4,
        d=8
    }
}
