using System;
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

        class ResultJsonDto
        {
            public string Status { get; set; }
            public string info { get; set; }
            public string data { get; set; }
        }
    }
}
