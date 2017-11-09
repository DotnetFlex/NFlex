using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NFlex.Opens.Weixin.Test
{
    public class CustomerManagerTest
    {
        WxClient client = WeixinClient.Instance;

        [Fact]
        public void SendCard()
        {
            var r = client.Customer.SendText("oMgrrwLVRpynRlThoJ2dv0jhX4E0", "测试");
            var result=client.Customer.SendCard("oMgrrwLVRpynRlThoJ2dv0jhX4E0", "pMgrrwHHIznmB53Z5gJqelLiYmBM");
        }
    }
}
