using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;
namespace NFlex.Test
{
    public class HttpClientTest
    {
        [Fact]
        public void GetTest()
        {
            var client = new HttpClient();
        }

        [Fact]
        public void BaseUriTest()
        {
            var result = new HttpClient("https://www.baidu.com")
                .AddQuery("wd","博客园")
                .Get("/s")
                .ToString();
        }

        [Fact]
        public void CookieTest()
        {
            var result = new HttpClient()
                .AddCookie("TestCookie","TestValue",".baidu.com")
                .AddCookie("Test2",Common.TimeStamp.ToString())
                .Get("https://www.baidu.com")
                .ToString();
        }

        [Fact]
        public void ResponseHeaderTest()
        {
            var result = new HttpClient()
                .AddCookie("test", "value")
                .Get("https://www.baidu.com");
        }

        [Fact]
        public void MultRequestTest()
        {
            var client = new HttpClient();
            var result = client.AddCookie("test", "value").Get("https://www.baidu.com");
            result = client.AddQuery("wd", "王者荣耀").Get("https://www.baidu.com/s");
        }

        [Fact]
        public void GetParamTest()
        {
            var result = new HttpClient()
                .AddQuery("wd", "博客园")
                .Get("https://www.baidu.com/s")
                .ToString();
        }

        [Fact]
        public void GetParamsTest()
        {
            var result = new HttpClient()
                .AddQuery("wd", "博客园")
                .AddQuery("t",Common.TimeStamp)
                .Get("https://www.baidu.com/s")
                .ToString();
        }

        [Fact]
        public void PostParamsTest()
        {
            var result = new HttpClient()
                .AddForm("u", "TestUser")
                .AddForm("p", "TestPassword")
                .Post("https://www.baidu.com")
                .ToString();
        }

        [Fact]
        public void PostFileTest()
        {
            var client = new HttpClient();
            var result=client.Options("https://upload.api.cli.im/upload.php?kid=cliim").ToString();

            result = client
                .AddHeader("Referer", "https://cli.im/text?d5f3d3620b7772259bf45c707dbfc05c")
                .AddHeader("Origin","https://cli.im")
                .AddFile("Filedata", @"E:\NFlex\Documents\ExampleImages\樱木花道.jpg")
                .Post("https://upload.api.cli.im/upload.php?kid=cliim")
                .ToString();
        }


        [Fact]
        public void ResultJsonTest()
        {
            var client = new HttpClient();
            var result = client.Options("https://upload.api.cli.im/upload.php?kid=cliim")
                .JsonTo<ResultJsonDto>();
        }

        [Fact]
        public void PostJsonTest()
        {
            var result = new HttpClient()
                .AddHeader("Referer", "http://petstore.swagger.io/?_ga=2.49015974.1257762365.1509037445-94624861.1509037445")
                .AddHeader("Origin", "http://petstore.swagger.io")
                .SetJson(@"
                    {
                      ""id"": 0,
                      ""category"": {
                                    ""id"": 0,
                        ""name"":""string""
                      },
                      ""name"": ""doggie"",
                      ""photoUrls"": [
                        ""string""
                      ],
                      ""tags"": [
                        {
                          ""id"": 0,
                          ""name"": ""string""
                        }
                      ],
                      ""status"": ""available""
                    }
                ")
                .Post("http://petstore.swagger.io/v2/pet")
                .ToString();
        }

        [Fact]
        public void IpTest()
        {
            for (int i = 0; i < 100; i++)
            {
                var result = new HttpClient()
                    .AddHeader("X-Forwarded-For", string.Format("{0}.{1}.{2}.{3}", Common.Random(10, 100), Common.Random(1, 255), Common.Random(1, 255), Common.Random(1, 255)))
                    .AddHeader("Referer", "http://www.xinhuanet.com/legal/sfxzgs2017/jdrmjc.htm")
                    .AddQuery("tk",Encrypt.Md5(Guid.NewGuid().ToString()))
                    .AddQuery("_", Common.TimeStamp.ToString())
                    .AddQuery("id", "2247")
                    .AddQuery("options", "28468,")
                    .Get("http://hd.xuan.news.cn/api/poll/vote.do?callback=jQuery112405774672465411428_1511488162226");
            }
        }

        [Fact]
        public void ProxyTest()
        {
            HttpClient client = new HttpClient("http://zhaomu.hotoos.com");
            client.Encoding = Encoding.Default;
            client.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
            client.AddHeader("Accept-Encoding", "gzip, deflate");
            client.AddHeader("Accept-Language", "zh-CN,zh;q=0.8,en-us;q=0.6,en;q=0.5;q=0.4");
            client.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.95 Safari/537.36 MicroMessenger/6.5.2.501 NetType/WIFI WindowsWechat QBCore/3.43.691.400 QQBrowser/9.0.2524.400");
            //ServicePointManager.SecurityProtocol=SecurityProtocolType.Tls12
            //client.Proxy = new WebProxy("112.117.59.203", 9999);
            var t1 = client.Get("http://zhaomu.hotoos.com/zhengwen/xiangqing.aspx?id=678&from=timeline&isappinstalled=0");
            var result = client
                .AddCookie("zhengwen", "id=41130&timespan=1513738317318&sign=2a9beda8442c91c524ada15bee3b101c")
                .AddQuery("action", "vote")
                .AddQuery("n", Common.RandomDouble())
                .AddForm("voteid", "678")
                .Post("http://zhaomu.hotoos.com/zhengwen/server.ashx")
                .ToString();

        }

        [Fact]
        public void AsyncTest()
        {
            HttpClient client = new HttpClient();
            var result = client.GetAsync("http://www.baidu.com").Result.ToString();

        }

        class ResultJsonDto
        {
            public string Status { get; set; }
            public string info { get; set; }
            public string data { get; set; }
        }
    }
}
