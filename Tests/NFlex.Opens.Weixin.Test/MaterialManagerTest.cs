using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NFlex.Opens.Weixin.Test
{
    public class MaterialManagerTest
    {
        WxClient client = WeixinClient.Instance;

        [Fact]
        public void UploadImage()
        {
            var result=client.Material.UploadImage(@"d:\qupiao.jpg");
            var imageUrl = result.url;
            //http://mmbiz.qpic.cn/mmbiz_jpg/KUuYeLldRiat5iaNRE6KKWoJAiaYNd6vUKVGm2ibJpMPw7Lmc9FAG4C4dSPueibkNroYLtyrPiaXjWibSr3ia7qQQICPrA/0
        }
    }
}
