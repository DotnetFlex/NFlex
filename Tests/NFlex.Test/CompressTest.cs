using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NFlex.Test
{
    public class CompressTest
    {
        [Fact]
        public void GZipCompressTest()
        {
            string str = "123456abcdef";
            string cStr = Compress.GZipCompress(str);
            string dStr = Compress.GZipDecompress(cStr);
        }
    }
}
