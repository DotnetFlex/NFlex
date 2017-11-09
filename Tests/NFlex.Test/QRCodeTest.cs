using NFlex.QRCode;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NFlex.Test
{
    public class QRCodeTest
    {
        string content = "https://www.baidu.com/link?url=7bYlKbsNzC1K1pSWemRzrOCwzqSprpgZhIon8_Cfx2s4dB51idr4BpTY-B763KNwKlLheYZ2lmhGa7LrZ8WFqq&wd=&eqid=f3cbf46600025b320000000659547933";

        [Fact]
        public void QRCodeTest1()
        {
             var image = content.CreateQRCode(ErrorCorrectionLevel.H,30);
        }

        [Fact]
        public void VerifyCodeTest()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }
            var result= string.Format("{0:N}", i - DateTime.Now.Ticks);

            byte[] buffer = Guid.NewGuid().ToByteArray();
            var result2= BitConverter.ToInt64(buffer, 0);

        }
    }
}
