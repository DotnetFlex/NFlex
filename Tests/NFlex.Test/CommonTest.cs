using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using NFlex;
using System.Linq.Expressions;
using System.Diagnostics;
using System.Net;

namespace NFlex.Test
{
    public class CommonTest
    {
        [Fact]
        public void RsaEncrypt()
        {
            string str = "ljzlingc.223";
            string pbk = "9a39c3fefeadf3d194850ef3a1d707dfa7bec0609a60bfcc7fe4ce2c615908b9599c8911e800aff684f804413324dc6d9f982f437e95ad60327d221a00a2575324263477e4f6a15e3b56a315e0434266e092b2dd5a496d109cb15875256c73a2f0237c5332de28388693c643c8764f137e28e8220437f05b7659f58c4df94685";
            string exponent = "010001";
            string result = Encrypt(str, pbk, exponent);

            HttpClient client = new HttpClient();
            client.Encoding = Encoding.Default;
            //var uc1 = new Cookie();
            //uc1.Name = "uc1";
            //uc1.
            var html = client
                //.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8")
                //.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.104 Safari/537.36 Core/1.53.4549.400 QQBrowser/9.7.12900.400")
                //.AddHeader("Referer", "https://login.taobao.com/member/login.jhtml?style=mini&newMini2=true&css_style=alimama_index&from=alimama&redirectURL=http%3A%2F%2Fwww.alimama.com&full_redirect=true&disableQuickLogin=true")
                //.AddHeader("Origin", "https://login.taobao.com")
                //.AddHeader("Upgrade-Insecure-Requests", "1")
                //.AddCookie("_uab_collina", "152346856186176600118634")
                //.AddCookie("v", "0")
                //.AddCookie("_tb_token_", "e8e01f8d5bb74")
                //.AddCookie("cna", "MzdVE34vTGsCAXFu6Dkc+kQ0")
                //.AddCookie("existShop", "MTUyMzQ2ODYwOA%3D%3D")
                //.AddCookie("lid", "lingcnet")
                //.AddCookie("lgc", "lingcnet")
                //.AddCookie("tracknick", "lingcnet")
                //.AddCookie("dnk", "lingcnet")
                //.AddCookie("cookie2", "1b33b7a51d60216db3a4e4e725bfaea2")
                //.AddCookie("sg", "t35")
                //.AddCookie("csg", "8c802d81")
                //.AddCookie("cookie1", "UNbSCQI5ME2zAeVMNwWgw3vyIIfXfdRZIN2ySN1hWMA%3D")
                //.AddCookie("unb", "167743433")
                //.AddCookie("skt", "07cd9427aba23609")
                //.AddCookie("t", "f5515af1aae4fd1b890834fa72ee719c")
                //.AddCookie("_cc_", "V32FPkk%2Fhw%3D%3D")
                //.AddCookie("tg", "0")
                //.AddCookie("_l_g_", "Ug%3D%3D")
                //.AddCookie("_nk_", "lingcnet")
                //.AddCookie("cookie17", "UoezSHM8RpIT")
                //.AddCookie("lc", "VygpDzrkZPaL%2FsnF9g%3D%3D")
                //.AddCookie("cookieCheck", "18915")
                //.AddCookie("_umdata", "6AF5B463492A874D6D38F206EA56E7EDB1FE5D5B90BED9C6A1C2B65DFDA8FA2938F08446661C721BCD43AD3E795C914C3BBD07BCABC40F64FFFAA62A42001B3F")
                //.AddCookie("isg", "BBISyAtDnGMzKOAfRclTgUYPdtg0iyQ5tdW5N9xr5kWw77DpyLFOzdnNW0tTn45V")
                .AddForm("TPL_username", "lingcnet")
                .AddForm("TPL_password", "")
                .AddForm("ncoSig", "")
                .AddForm("ncoSessionid", "")
                .AddForm("ncoToken", "a80f1ce317ea662f94e0d004bd23a0f4a9728e2a")
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
                .AddForm("TPL_password_2", result)
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
                .AddForm("um_token","")// "HV01PAAZ0b87260f716ee8395ace4bd300362673")
                .AddForm("ua","")//, "107#ssOG8zS9spohgMC8/Sr+Xz91lnblgOPUtsSRT8DwgzODlwsKOtsc3sObln93g/IyXzSYT8fagzOxxXGQ/dAvOwOrRmbkgVYEiwTUTrm7gLbMYwfCzezW4sR0KNJgbKiisRGxW6cZA3tfQqDfgQIRXU6ZWUetzQwTMdN7SvB8KOKMbTdo65tmjJsrMEe5jk05R0OUPeJcjeVjSRBsNSCXXQPmbLc6QXXlE8TnlDmilF0q5cMEPy3l+QI8Qvzop1+yEargcepuGv+dggqDMDldef03F68M8fr/EUHRcUpzC6p2gT/YBBMHXDz581AqvKl9FaQgee/ZCBeSkT+G7BFXcTNAZ6aZFZQdteiU8uTJ3d927HWmKyWm3kGI1ms8f0lJqu/wuY/0tvE52jj2Kyji3fvq12fhFmqmCqj33eOeumqnBQjTPJLOCy4uem2r8dlewVQwGVgvGxH7WH/hjyYgudaICd05ufu8bo9ZUMi/3pyVkBEy+k9kuccUvpRwIcHmcYgBo3QqvmD3iDiU7kTVCy4WF5foFZ3Rwne1G/gI87+i69p56yOPhBNplv2advrih+e1G/gI87+NyD+Ygjtme129dm2ZFv+nuVtwCzej8JT2jp+Bmg9Jh6IIeZ8oc1HiIui18uer8vDoy9QjBgKJtJwZQm28c1i/CupIqMjct1i2B9iHNyr3wZ53Z5va929m9ryh3qKHrdqT7JHkm99PhZBQQ6dZcc+2a/93hMVC8DV2B9+OkDr2exm3vf0alJQnh/iqGeVHeviky6t4B9/PwvZbZ6xRQ9eza/V1hrYO8J3sP1/Is9+Dq05HQffadxjcorj7FtY7CJDjyEuJkQhy3x1qxTfdZJp2th/PwwVWqft7+HiG7ipLCyRIedvd8XEXEIK4lu7cgRM7PcP/zYXlXbs9bKbNQFEX")
                .Post("https://login.taobao.com/member/login.jhtml?redirectURL=http%3A%2F%2Fwww.alimama.com")
                .ToString();

        }

        static string Encrypt(string str,string pbk,string exp)
        {
            RSAParameters rsaParameters = new RSAParameters()
            {
                Exponent = FromHex(exp), // new byte[] {01, 00, 01}
                Modulus = FromHex(pbk),   // new byte[] {A3, A6, ...}
            };
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(rsaParameters);
            byte[] sample = rsa.Encrypt(Encoding.UTF8.GetBytes(str), false);
            return BitConverter.ToString(sample).Replace("-","").ToLower();
        }
        static byte[] FromHex(string hex)
        {
            if (string.IsNullOrEmpty(hex) || hex.Length % 2 != 0) throw new ArgumentException("not a hexidecimal string");
            List<byte> bytes = new List<byte>();
            for (int i = 0; i < hex.Length; i += 2)
            {
                bytes.Add(Convert.ToByte(hex.Substring(i, 2), 16));
            }
            return bytes.ToArray();
        }
        public static byte[] Hex2Byte(string byteStr)
        {
            try
            {
                byteStr = byteStr.ToUpper().Replace(" ", "");
                int len = byteStr.Length / 2;
                byte[] data = new byte[len];
                for (int i = 0; i < len; i++)
                {
                    data[i] = Convert.ToByte(byteStr.Substring(i * 2, 2), 16);

                }
                return data;
            }
            catch (Exception ex)
            {
                // SystemLog.ErrLog("bin2byte失败：" + ex.Message);
                return new byte[] { };
            }
        }
    }
}
