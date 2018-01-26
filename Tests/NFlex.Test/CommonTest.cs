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
        public void CardCreate()
        {
            var t = TestEnum.c;
            var t1 = (TestEnum.a | TestEnum.b);
            var t2 = (TestEnum.c) |t;
            var t3 = (TestEnum.c | TestEnum.d);

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
