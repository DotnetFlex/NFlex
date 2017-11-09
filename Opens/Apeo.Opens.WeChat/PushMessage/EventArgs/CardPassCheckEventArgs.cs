using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin.PushMessage
{
    public class CardPassCheckEventArgs:EventArgsBase
    {
        /// <summary>
        /// 卡券ID
        /// </summary>
        public string CardId { get; set; }
        /// <summary>
        /// 审核不通过原因
        /// </summary>
        public string RefuseReason { get; set; }
    }
}
