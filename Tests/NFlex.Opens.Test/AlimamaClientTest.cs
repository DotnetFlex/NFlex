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
            var items = client.SearchItems("https://detail.tmall.com/item.htm?spm=a230r.1.14.27.23ed24f4yiTy0z&id=545951843433&ns=1&abbucket=9");
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
            client.Login("lingcnet", "ljzlingc.223");
            var items = client.SearchItems("Xiaomi/小米AI音箱小爱同学迷你智能网络音响语音蓝牙小艾mini");
            var adzones = client.GetAdzones(items[0].auctionId);

            var adzone = adzones.webAdzones.FirstOrDefault();
            var adzoneId = adzone.id;
            var siteId = adzone.sub.FirstOrDefault().id;
            var urlInfo = client.CreatePromotUrl(items[0].auctionId, siteId, adzoneId);
        }
    }
}
