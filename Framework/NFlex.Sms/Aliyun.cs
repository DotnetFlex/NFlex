using NFlex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Sms
{
    /// <summary>
    /// 阿里云短信
    /// </summary>
    public class Aliyun
    {
        private string AccessKeyId { get; set; }
        private string AccessSecret { get; set; }
        private string Format { get; set; } = "JSON";
        private string SignatureMethod { get; set; } = "HMAC-SHA1";
        private string SignatureVersion { get; set; } = "1.0";
        private string Timestamp { get { return DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ"); } }
        private string SignatureNonce { get { return Guid.NewGuid().ToString(); } }
        private string Version { get; set; } = "2017-05-25";
        private string RegionId { get; set; } = "cn-hangzhou";

        public Aliyun(string accessKeyId,string accessSecret)
        {
            AccessKeyId = accessKeyId;
            AccessSecret = accessSecret;
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="signName">短信签名</param>
        /// <param name="templateCode">模板编号</param>
        /// <param name="paramsObject">模板参数对象</param>
        /// <param name="phoneNumbers">手机号码（最多 10000 个）</param>
        public SmsResult<ResultContent> SendMessage(string signName, string templateCode, IEnumerable<string> phoneNumbers, object paramsObject)
            =>SendMessage(signName, templateCode, phoneNumbers, paramsObject.ToJson());

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="signName">短信签名</param>
        /// <param name="templateCode">模板编号</param>
        /// <param name="paramsObject">模板参数对象</param>
        /// <param name="phoneNumber">手机号码</param>
        public SmsResult<ResultContent> SendMessage(string signName, string templateCode, string phoneNumber, object paramsObject)
            => SendMessage(signName, templateCode,  new []{ phoneNumber }, paramsObject.ToJson());

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="signName">短信签名</param>
        /// <param name="templateCode">模板编号</param>
        /// <param name="paramsList">模板参数键值对集合</param>
        /// <param name="phoneNumbers">手机号码（最多 10000 个）</param>
        public SmsResult<ResultContent> SendMessage(string signName, string templateCode, IEnumerable<string> phoneNumbers, Dictionary<string, string> paramsList)
            => SendMessage(signName, templateCode, phoneNumbers, paramsList.ToJson());
        

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="signName">短信签名</param>
        /// <param name="templateCode">模板编号</param>
        /// <param name="paramsList">模板参数键值对集合</param>
        /// <param name="phoneNumber">手机号码</param>
        public SmsResult<ResultContent> SendMessage(string signName, string templateCode, string phoneNumber, Dictionary<string, string> paramsList)
            => SendMessage(signName, templateCode, new[] { phoneNumber }, paramsList.ToJson());

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="signName">短信签名</param>
        /// <param name="templateCode">模板编号</param>
        /// <param name="paramsJson">模板参数键值Json字符串</param>
        /// <param name="phoneNumber">手机号码</param>
        /// <returns></returns>
        public SmsResult<ResultContent> SendMessage(string signName, string templateCode, string phoneNumber, string paramsJson)
            => SendMessage(signName, templateCode, new[] { phoneNumber }, paramsJson);

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="signName">短信签名</param>
        /// <param name="templateCode">模板编号</param>
        /// <param name="paramsJson">模板参数键值Json字符串</param>
        /// <param name="phoneNumbers">手机号码（最多 10000 个）</param>
        /// <returns></returns>
        public SmsResult<ResultContent> SendMessage(string signName,string templateCode, IEnumerable<string> phoneNumbers,string paramsJson)
        {
            var paras = new SortedDictionary<string, string>();
            paras.Add("AccessKeyId", AccessKeyId);
            paras.Add("Format", Format);
            paras.Add("SignatureMethod", SignatureMethod);
            paras.Add("SignatureVersion", SignatureVersion);
            paras.Add("SignatureNonce", SignatureNonce);
            paras.Add("Timestamp", Timestamp);

            paras.Add("Action", "SendSms");
            paras.Add("Version", Version);
            paras.Add("RegionId", RegionId);
            paras.Add("SignName", signName);
            paras.Add("TemplateCode", templateCode);
            paras.Add("TemplateParam", paramsJson);
            paras.Add("PhoneNumbers", string.Join(",", phoneNumbers));
            var queryList = paras.OrderBy(x => x.Key, new AsciiComparer()).Select(t => t.Key.UrlEncode() + "=" + t.Value.UrlEncode()).ToList();
            var queryString = string.Join("&", queryList);

            var sign = GetSign(queryString);
            HttpClient client = new HttpClient();
            var result = client.Get("http://dysmsapi.aliyuncs.com/?Signature=" + sign + "&" + queryString)
                .JsonTo<ResultContent>();
            if (result == null) return new SmsResult<ResultContent>(false, "-1", "未知错误");
            if (result.Code == "OK") return new SmsResult<ResultContent>(true, "OK", "发送成功",result);
            return new SmsResult<ResultContent>(false, result.Code, result.Message,result);
        }

        private string GetSign(string queryString)
        {
            var signSource = "GET&" + ("/".UrlEncode()) + "&" + queryString.UrlEncode();
            var secret = AccessSecret + "&";
            var sign = Encrypt.HmacSha1(Encoding.UTF8.GetBytes(signSource), Encoding.UTF8.GetBytes(secret));
            return Convert.ToBase64String(sign).UrlEncode();
        }

        public class ResultContent
        {
            /// <summary>
            /// 响应消息
            /// </summary>
            public string Message { get; set; }
            /// <summary>
            /// 请求响应编号
            /// </summary>
            public string RequestId { get; set; }
            /// <summary>
            /// 响应编码
            /// </summary>
            public string Code { get; set; }
            /// <summary>
            /// 业务编号
            /// </summary>
            public string BizId { get; set; }
            /// <summary>
            /// 服务器编码
            /// </summary>
            public string HostId { get; set; }
        }
    }
}
