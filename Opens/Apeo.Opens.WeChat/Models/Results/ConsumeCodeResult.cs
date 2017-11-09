using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin.Models.Results
{
    public class ConsumeCodeResult:Result
    {
        /// <summary>
        /// 用户在该公众号内的唯一身份标识
        /// </summary>
        public string openid { get; set; }
        public CardInfo card { get; set; }
        public class CardInfo
        {
            /// <summary>
            /// 卡券ID
            /// </summary>
            public string card_id { get; set; }
        }
    }
}
