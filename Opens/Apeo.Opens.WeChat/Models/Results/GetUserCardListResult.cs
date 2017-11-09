using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin.Models.Results
{
    public class GetUserCardListResult:Result
    {
        /// <summary>
        /// 卡券列表
        /// </summary>
        public List<CardInfo> card_list { get; set; }
        /// <summary>
        /// 是否有可用的朋友的券
        /// </summary>
        public bool has_share_card { get; set; }
        public class CardInfo
        {
            public string code { get; set; }
            public string card_id { get; set; }
        }
    }
}
