using NFlex.Opens.Taobao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NFlex.Opens.Test
{
    public class AlimamaClientTest
    {
        [Fact]
        public void LoginTest()
        {
            AlimamaClient client = new AlimamaClient();
            client.Login("账号", "密码");
        }

        [Fact]
        public void SearchItemsTest()
        {
            AlimamaClient client = new AlimamaClient();
            client.Login("账号", "密码");
            var items = client.SearchItems("https://detail.tmall.com/item.htm?id=43928349416&ut_sk=1.WLV3mdV/iocDAMPoBA3gN3Ws_21380790_1524482366545.Copy.1&sourceType=item&price=749&suid=2643B0AB-E71D-43BB-BF38-F7E3DE0E1F22&un=d7988144eace38568a195baaa22d8f6a&share_crt_v=1&cpp=1&shareurl=true&spm=a313p.22.1bz.943087919139&short_name=h.WxmKHjx&app=chrome");
        }

        [Fact]
        public void GetAdzonesTest()
        {
            AlimamaClient client = new AlimamaClient();
            client.Login("账号", "密码");
            var items = client.SearchItems("https://detail.tmall.com/item.htm?spm=a230r.1.14.27.23ed24f4yiTy0z&id=545951843433&ns=1&abbucket=9");
            var adzones = client.GetAdzones(items[0].auctionId);
        }



        [Fact]
        public void CreatePromotUrlTest()
        {
            AlimamaClient client = new AlimamaClient();
            client.Login("账号", "密码");
            var items = client.SearchItems("Xiaomi/小米AI音箱小爱同学迷你智能网络音响语音蓝牙小艾mini");
            var adzones = client.GetAdzones(items[0].auctionId);

            var adzone = adzones.webAdzones.FirstOrDefault();
            var adzoneId = adzone.id;
            var siteId = adzone.sub.FirstOrDefault().id;
            var urlInfo = client.CreatePromotUrl(items[0].auctionId, siteId, adzoneId);
        }
    }
}
