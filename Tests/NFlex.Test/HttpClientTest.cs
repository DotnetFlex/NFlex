using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            var result = client.Get("https://www.baidu.com").ToString();
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
        public void ApiTest()
        {
            Hashtable Parameters = new Hashtable();
            Parameters.Add("channel", "qupiaowang");
            Parameters.Add("source", "WECHAT");
            Parameters.Add("key", "BEE5390FB3054D6503F4CF9EB5E77039");

            StringBuilder sb = new StringBuilder();
            ArrayList akeys = new ArrayList(Parameters.Keys);
            akeys.Sort();

            foreach (string k in akeys)
            {
                string v = (string)Parameters[k];
                sb.Append(k + "=" + v + "&");
            }
            sb.Remove(sb.Length - 1, 1);
            string sign = Encrypt.Md5(sb.ToString());

            HttpClient client = new HttpClient();
            var result = client
                .AddHeader("OpenRu", "qupiaowang")
                .AddHeader("OpenRs", "WECHAT")
                .AddHeader("Authorization", "basic " + sign.ToUpper())
                .AddHeader("req-source", "qupiao")
                .Get("http://120.76.163.32:9013/api/order/5EB71B97-D3E1-49E8-95D2-A82D0147BD0A/1/10")
                .ToString();
            var json = Compress.GZipDecompress(result);
        }

        class ResultJsonDto
        {
            public string Status { get; set; }
            public string info { get; set; }
            public string data { get; set; }
        }
    }
}
