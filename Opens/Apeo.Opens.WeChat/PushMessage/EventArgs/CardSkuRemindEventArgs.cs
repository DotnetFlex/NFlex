using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin.PushMessage
{
    public class CardSkuRemindEventArgs:EventArgsBase
    {
        /// <summary>
        /// 卡券ID
        /// </summary>
        public string CardId { get; set; }

        /// <summary>
        /// 报警详细信息
        /// </summary>
        public string Detail { get; set; }
    }
}
