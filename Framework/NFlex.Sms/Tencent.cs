using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Sms
{
    /// <summary>
    /// 腾讯云短信
    /// </summary>
    public class Tencent
    {

        private string AppId { get; set; }
        private string AppKey { get; set; }
        public Tencent(string appid, string appKey)
        {
            AppId = appid;
            AppKey = appKey;
        }

        /// <summary>
        /// 发送国内短信
        /// </summary>
        /// <param name="templateId">模板编号</param>
        /// <param name="paramsList">模板参数</param>
        /// <param name="mobile">接收短信的手机号</param>
        public SmsResult<SendMessageResult> SendMessage(string templateId, string mobile, params object[] paramsList)
        {
            var random = Guid.NewGuid().ToString("N");
            var time = Convert.ToInt32(Common.TimeStamp);
            var sign = GetSign(random, time, "mobile", mobile);
            var json = new
            {
                tel = new { nationcode = "86", mobile = mobile },
                tpl_id = templateId,
                @params = paramsList ??new object[] { },
                sig = sign,
                time = time,
                extend = "",
                ext = ""
            };

            return Send<SendMessageResult>(json, "sendsms", random);
        }

        /// <summary>
        /// 发送国内短信
        /// </summary>
        /// <param name="templateId">模板编号</param>
        /// <param name="paramsList">模板参数</param>
        /// <param name="mobile">接收短信的手机号列表</param>
        public SmsResult<SendMultMessageResult> SendMessage(string templateId, IEnumerable<string> mobiles, params object[] paramsList)
        {
            var random = Guid.NewGuid().ToString("N").GetHashCode().ToString();
            var time = Convert.ToInt32(Common.TimeStamp);
            var sign = GetSign(random, time, "mobile", string.Join(",", mobiles.ToArray()));
            var json = new
            {
                tel = mobiles.Select(t => new { nationcode = "86", mobile = t }),
                tpl_id = templateId,
                @params = paramsList,
                sig = sign,
                time = time,
                extend = "",
                ext = ""
            };

            return Send<SendMultMessageResult>(json, "sendmultisms2", random);
        }
        /// <summary>
        /// 发送国际短信（不支持群发）
        /// </summary>
        /// <param name="templateId">模板编号</param>
        /// <param name="paramsList">模板参数</param>
        /// <param name="mobile">接收短信的手机号</param>
        public SmsResult<SendIntelMessageResult> SendIntelMessage(string templateId, string mobile, params object[] paramsList)
        {
            var random = Guid.NewGuid().ToString("N");
            var time = Convert.ToInt32(Common.TimeStamp);
            var sign = GetSign(random, time, "tel", mobile);
            var json = new
            {
                tel = mobile,
                ext = "",
                extend = "",
                tpl_id = templateId,
                @params = paramsList,
                sig = sign,
                time = time,
                type = 0
            };

            return Send<SendIntelMessageResult>(json, "sendisms", random);
        }

        public SmsResult<T> Send<T>(object json,string url,string random) where T: ResultMessageBase
        {
            HttpClient client = new HttpClient("https://yun.tim.qq.com/v5/tlssmssvr");
            var result = client.AddQuery("sdkappid", AppId)
                .AddQuery("random", random)
                .SetJson(json.ToJson())
                .Post(url)
                .JsonTo<T>();

            return new SmsResult<T>(result.result == 0, result.result.ToString(), result.errmsg)
            {
                ResultBody = result
            };
        }

        private string GetSign(string random, int time, string mobileKey, string mobile)
        {
            var sourceString = string.Format("appkey={0}&random={1}&time={2}&{3}={4}",
                AppKey, random, time, mobileKey, mobile);
            return Encrypt.Sha256(sourceString, Encoding.UTF8);
        }

        public class ResultMessageBase
        {
            public int result { get; set; }
            public string errmsg { get; set; }
        }

        public class SendMessageResult: ResultMessageBase
        {
            
            public string ext { get; set; }
            public string sid { get; set; }
            public int fee { get; set; }
        }

        public class SendMultMessageResult: ResultMessageBase
        {
            public string ext { get; set; }
            public List<SendMultMessageResultDetail> detail { get; set; }
        }

        public class SendMultMessageResultDetail: ResultMessageBase
        {
            public string mobile { get; set; }
            public string nationcode { get; set; }
            public string sid { get; set; }
            public int fee { get; set; }
        }

        public class SendIntelMessageResult: ResultMessageBase
        {
            public string ext { get; set; }
            public string nationcode { get; set; }
            public string sid { get; set; }
            public int fee { get; set; }
        }
    }
}
