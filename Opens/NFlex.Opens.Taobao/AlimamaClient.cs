using CsQuery;
using NFlex;
using NFlex.Opens.Taobao.AlimamaResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace NFlex.Opens.Taobao
{
    public class AlimamaClient
    {
        const string EXPONENT = "010001";

        private HttpClient _client;


        public HttpClient Client
        {
            get {
                if (_client == null)
                {
                    _client = new HttpClient();
                    _client.Encoding = Encoding.Default;
                }
                return _client;
            }
        }

        public bool Login(string username,string password)
        {
            var loginHtml = Client.Get("https://login.taobao.com/member/login.jhtml?style=mini&newMini2=true&css_style=alimama_index&from=alimama&redirectURL=http%3A%2F%2Fwww.alimama.com&full_redirect=true&disableQuickLogin=true")
                .ToString();
            CQ cq = loginHtml;
            var pbk = cq.Find("#J_PBK").Val();
            var html = Client
                .AddForm("TPL_username", username)
                .AddForm("TPL_password", "")
                .AddForm("ncoSig", "")
                .AddForm("ncoSessionid", "")
                .AddForm("ncoToken",cq.Find("#J_NcoToken").Val())// "a80f1ce317ea662f94e0d004bd23a0f4a9728e2a")
                .AddForm("slideCodeShow", "false")
                .AddForm("useMobile", "false")
                .AddForm("lang", "zh_CN")
                .AddForm("loginsite", "0")
                .AddForm("newlogin", "0")
                .AddForm("TPL_redirect_url", "http://www.alimama.com")
                .AddForm("from", "alimama")
                .AddForm("fc", "default")
                .AddForm("style", "mini")
                .AddForm("css_style", "alimama_index")
                .AddForm("keyLogin", "false")
                .AddForm("qrLogin", "true")
                .AddForm("newMini", "false")
                .AddForm("newMini2", "true")
                .AddForm("tid", "")
                .AddForm("loginType", "3")
                .AddForm("minititle", "alimama_index")
                .AddForm("minipara", "")
                .AddForm("pstrong", "")
                .AddForm("sign", "")
                .AddForm("need_sign", "")
                .AddForm("isIgnore", "")
                .AddForm("full_redirect", "true")
                .AddForm("sub_jump", "")
                .AddForm("popid", "")
                .AddForm("callback", "")
                .AddForm("guf", "")
                .AddForm("not_duplite_str", "")
                .AddForm("need_user_id", "")
                .AddForm("poy", "")
                .AddForm("gvfdcname", "")
                .AddForm("gvfdcre", "")
                .AddForm("from_encoding", "")
                .AddForm("sub", "")
                .AddForm("TPL_password_2", Encrypt(password,pbk, EXPONENT))
                .AddForm("loginASR", "1")
                .AddForm("loginASRSuc", "1")
                .AddForm("allp", "")
                .AddForm("oslanguage", "zh-CN")
                .AddForm("sr", "1920*1080")
                .AddForm("osVer", "windows|6.1")
                .AddForm("naviVer", "chrome|53.02785104")
                .AddForm("osACN", "Mozilla")
                .AddForm("osAV", "5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.104 Safari/537.36 Core/1.53.4549.400 QQBrowser/9.7.12900.400")
                .AddForm("osPF", "Win32")
                .AddForm("miserHardInfo", "")
                .AddForm("appkey", "00000000")
                .AddForm("nickLoginLink", "")
                .AddForm("mobileLoginLink", "https://login.taobao.com/member/login.jhtml?style=mini&newMini2=true&css_style=alimama_index&from=alimama&redirectURL=http://www.alimama.com&full_redirect=true&disableQuickLogin=true&useMobile=true")
                .AddForm("showAssistantLink", "")
                .AddForm("um_token","HV01PAAZ0b87260f716ee8395ace4bd300362673")
                .AddForm("ua", "107#ssOG8zS9spohgMC8/Sr+Xz91lnblgOPUtsSRT8DwgzODlwsKOtsc3sObln93g/IyXzSYT8fagzOxxXGQ/dAvOwOrRmbkgVYEiwTUTrm7gLbMYwfCzezW4sR0KNJgbKiisRGxW6cZA3tfQqDfgQIRXU6ZWUetzQwTMdN7SvB8KOKMbTdo65tmjJsrMEe5jk05R0OUPeJcjeVjSRBsNSCXXQPmbLc6QXXlE8TnlDmilF0q5cMEPy3l+QI8Qvzop1+yEargcepuGv+dggqDMDldef03F68M8fr/EUHRcUpzC6p2gT/YBBMHXDz581AqvKl9FaQgee/ZCBeSkT+G7BFXcTNAZ6aZFZQdteiU8uTJ3d927HWmKyWm3kGI1ms8f0lJqu/wuY/0tvE52jj2Kyji3fvq12fhFmqmCqj33eOeumqnBQjTPJLOCy4uem2r8dlewVQwGVgvGxH7WH/hjyYgudaICd05ufu8bo9ZUMi/3pyVkBEy+k9kuccUvpRwIcHmcYgBo3QqvmD3iDiU7kTVCy4WF5foFZ3Rwne1G/gI87+i69p56yOPhBNplv2advrih+e1G/gI87+NyD+Ygjtme129dm2ZFv+nuVtwCzej8JT2jp+Bmg9Jh6IIeZ8oc1HiIui18uer8vDoy9QjBgKJtJwZQm28c1i/CupIqMjct1i2B9iHNyr3wZ53Z5va929m9ryh3qKHrdqT7JHkm99PhZBQQ6dZcc+2a/93hMVC8DV2B9+OkDr2exm3vf0alJQnh/iqGeVHeviky6t4B9/PwvZbZ6xRQ9eza/V1hrYO8J3sP1/Is9+Dq05HQffadxjcorj7FtY7CJDjyEuJkQhy3x1qxTfdZJp2th/PwwVWqft7+HiG7ipLCyRIedvd8XEXEIK4lu7cgRM7PcP/zYXlXbs9bKbNQFEX")
                .Post("https://login.taobao.com/member/login.jhtml?redirectURL=http%3A%2F%2Fwww.alimama.com")
                .ToString();

            if (html.IndexOf("<title>页面跳转中</title>") == -1)
                return false;

            var urls = GetUrls(html);
            Client.AddHeader("Referer", "https://login.taobao.com/member/login.jhtml?redirectURL=http%3A%2F%2Fwww.alimama.com")
                .Get(urls[1]);

            return true;
        }

        public List<SearchItemResult.Item> SearchItems(string searchStr)
        {
            Client.Encoding = Encoding.UTF8;
            var result = Client.AddQuery("q", searchStr.UrlEncode())
                .AddQuery("_t", Common.TimeStamp)
                .AddQuery("auctionTag", "")
                .AddQuery("perPageSize", "50")
                .AddQuery("shopTag", "")
                .AddQuery("t", Common.TimeStamp)
                .AddQuery("_tb_token_", "")
                .AddQuery("pvid", "")
                .Get("http://pub.alimama.com/items/search.json")
                ;
            return result.JsonTo<SearchItemResult>()
                .data.pageList;
        }

        public GetAdzoneResult.Data GetAdzones(string itemId)
        {
            Client.Encoding = Encoding.UTF8;
            var result = Client
                .AddQuery("tag", "29")
                .AddQuery("itemId", itemId)
                .AddQuery("blockId", "")
                .AddQuery("t", Common.TimeStamp)
                .AddQuery("_tb_token_", "")
                .AddQuery("pvid", "")
                .Get("http://pub.alimama.com/common/adzone/newSelfAdzone2.json")
                ;
            return result.JsonTo<GetAdzoneResult>().data;
        }

        public CreatePromotUrlResult.UrlInfo CreatePromotUrl(string itemId,string siteId,string adzoneId)
        {

            Client.Encoding = Encoding.UTF8;
            var result = Client
                .AddQuery("auctionid", itemId)
                .AddQuery("adzoneid", adzoneId)
                .AddQuery("siteid", siteId)
                .AddQuery("scenes", "1")
                .AddQuery("t", Common.TimeStamp)
                .AddQuery("_tb_token_", "")
                .AddQuery("pvid", "")
                .Get("http://pub.alimama.com/common/code/getAuctionCode.json")
                ;
            return result.JsonTo<CreatePromotUrlResult>().data;
        }

        private string Encrypt(string str, string pbk, string exp)
        {
            RSAParameters rsaParameters = new RSAParameters()
            {
                Exponent = FromHex(exp),
                Modulus = FromHex(pbk),
            };
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(rsaParameters);
            byte[] sample = rsa.Encrypt(Encoding.UTF8.GetBytes(str), false);
            return BitConverter.ToString(sample).Replace("-", "").ToLower();
        }
        private byte[] FromHex(string hex)
        {
            if (string.IsNullOrEmpty(hex) || hex.Length % 2 != 0) throw new ArgumentException("not a hexidecimal string");
            List<byte> bytes = new List<byte>();
            for (int i = 0; i < hex.Length; i += 2)
            {
                bytes.Add(Convert.ToByte(hex.Substring(i, 2), 16));
            }
            return bytes.ToArray();
        }
        private List<string> GetUrls(string html)
        {
            List<string> urls = new List<string>();
            Regex re = new Regex(@"(?<url>http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?)");
            MatchCollection mc = re.Matches(html);
            foreach (Match m in mc)
            {
                urls.Add(m.Result("${url}"));
            }
            return urls;
        }
    }
}
