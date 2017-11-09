using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin.PushMessage
{
    public class UserViewCardEventArgs: EventArgsBase
    {
        /// <summary>
        /// 卡券ID
        /// </summary>
        public string CardId { get; set; }
        /// <summary>
        /// 商户自定义code值。非自定code推送为空串
        /// </summary>
        public string UserCardCode { get; set; }
        /// <summary>
        /// 商户自定义二维码渠道参数，用于标识本次扫码打开会员卡来源来自于某个渠道值的二维码
        /// </summary>
        public string OuterStr { get; set; }
    }
}
