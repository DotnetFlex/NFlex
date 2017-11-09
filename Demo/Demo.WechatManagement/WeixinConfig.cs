using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WechatManagement
{
    public class WeixinConfig
    {
        public string AppId { get; set; }
        public string AppSecret { get; set; }
        /// <summary>
        /// 开通微信公众时的自定义通讯凭证
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// API地址
        /// </summary>
        public string ApiUrl { get; set; }

        public string EncodingAESKey { get; set; } = "";
    }
}
