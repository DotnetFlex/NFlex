using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using NFlex;
using System.Linq.Expressions;

namespace NFlex.Test
{
    public class CommonTest
    {
        [Fact]
        public void CardCreate()
        {
            var obj = new object();
            Method(obj);
            var result = obj == null;
            
        }

        private void Method(object obj)
        {
            obj = null;
        }
    }
}
