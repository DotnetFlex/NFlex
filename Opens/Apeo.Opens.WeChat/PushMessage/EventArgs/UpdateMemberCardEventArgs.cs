using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin.PushMessage
{
    public class UpdateMemberCardEventArgs:EventArgsBase
    {
        /// <summary>
        /// 卡券ID
        /// </summary>
        public string CardId { get; set; }

        /// <summary>
        /// Code码
        /// </summary>
        public string UserCardCode { get; set; }

        /// <summary>
        /// 变动的积分值
        /// </summary>
        public int ModifyBonus { get; set; }

        /// <summary>
        /// 变动的余额值
        /// </summary>
        public int ModifyBalance { get; set; }
    }
}
