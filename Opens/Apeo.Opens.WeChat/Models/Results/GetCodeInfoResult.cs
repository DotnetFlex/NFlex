using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin.Models.Results
{
    public class GetCodeInfoResult:Result
    {
        /// <summary>
        /// 卡券信息
        /// </summary>
        public CardInfo card { get; set; }
        /// <summary>
        /// 用户openid
        /// </summary>
        public string openId { get; set; }
        /// <summary>
        /// 是否可以核销，true为可以核销，false为不可核销
        /// </summary>
        public bool can_consume { get; set; }
        /// <summary>
        /// 当前code对应卡券的状态
        /// </summary>
        public UserCardStatus user_card_status { get; set; }

        public class CardInfo
        {
            /// <summary>
            /// 卡券ID
            /// </summary>
            public string card_id { get; set; }
            /// <summary>
            /// 起始使用时间
            /// </summary>
            public int begin_time { get; set; }
            /// <summary>
            /// 结束时间
            /// </summary>
            public int end_time { get; set; }
        }
    }
}
