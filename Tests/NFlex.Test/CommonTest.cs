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
            var c = Common.CreateNumberId19();
            var a="0.89265452879139".GetHashCode();
            var b="0.280527401380486".GetHashCode();

        }
    }
}
