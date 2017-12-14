using NFlex.Sms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NFlex.Test
{
    public class SmsTest
    {
        [Fact]
        public void AliyunSendSmsTest()
        {
            var sms = new Aliyun("LTAIbBLpUVspRYeE", "aYQHoa5Ai121WxTDSChJiJqXTg6Oig");
            var result=sms.SendMessage("趣票网", "SMS_112475491", "13603020203", new { Ordernumber= "123456789" });
        }

        [Fact]
        public void TencentSendSmsTest()
        {
            var sms = new Tencent("1400053703", "173623c2f73341d46f4114ccc6a675f3");
            var result = sms.SendMessage("63881", "13570825903", "2345");
        }
    }
}
