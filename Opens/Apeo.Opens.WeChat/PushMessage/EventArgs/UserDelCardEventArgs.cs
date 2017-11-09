using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin.PushMessage
{
    public class UserDelCardEventArgs:EventArgsBase
    {
        /// <summary>
        /// 卡券ID
        /// </summary>
        public string CardId { get; set; }
        /// <summary>
        /// code序列号。自定义code及非自定义code的卡券被领取后都支持事件推送
        /// </summary>
        public string UserCardCode { get; set; }
    }
}
