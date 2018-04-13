using CsQuery;
using NFlex;
using NFlex.Opens.Taobao.AlimamaResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        
        private CookieContainer _cookie=new CookieContainer();

        private HttpClient GetClient()
        {
            return new HttpClient(_cookie);
        }

        public bool Login(string username,string password)
        {
            var client = GetClient();
            client.Encoding = Encoding.Default;
            var loginHtml = client.Get("https://login.taobao.com/member/login.jhtml?style=mini&newMini2=true&css_style=alimama_index&from=alimama&redirectURL=http%3A%2F%2Fwww.alimama.com&full_redirect=true&disableQuickLogin=true")
                .ToString();
            CQ cq = loginHtml;
            var pbk = cq.Find("#J_PBK").Val();
            var html = client
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
                .AddForm("um_token","")
                .AddForm("ua", "")
                .Post("https://login.taobao.com/member/login.jhtml?redirectURL=http%3A%2F%2Fwww.alimama.com")
                .ToString();

            if (html.IndexOf("<title>页面跳转中</title>") == -1)
                return false;

            var urls = html.GetUrls();
            client.AddHeader("Referer", "https://login.taobao.com/member/login.jhtml?redirectURL=http%3A%2F%2Fwww.alimama.com")
                .Get(urls[1]);

            return true;
        }

        public List<SearchItemResult.Item> SearchItems(string searchStr,int pageSize=50)
        {
            var client = GetClient();
            client.Encoding = Encoding.UTF8;
            var result = client.AddQuery("q", searchStr.UrlEncode())
                .AddQuery("_t", Common.TimeStamp)
                .AddQuery("auctionTag", "")
                .AddQuery("perPageSize", pageSize)
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
            var client = GetClient();
            client.Encoding = Encoding.UTF8;
            var result = client
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

            var client = GetClient();
            client.Encoding = Encoding.UTF8;
            var result = client
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
    }
}
