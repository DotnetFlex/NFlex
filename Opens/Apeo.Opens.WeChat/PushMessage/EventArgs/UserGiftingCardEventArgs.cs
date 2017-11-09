using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin.PushMessage
{
    public class UserGiftingCardEventArgs:EventArgsBase
    {
        /// <summary>
        /// 卡券ID
        /// </summary>
        public string CardId { get; set; }

        /// <summary>
        /// code序列号
        /// </summary>
        public string UserCardCode { get; set; }

        /// <summary>
        /// 是否转赠退回，0代表不是，1代表是
        /// </summary>
        public int IsReturnBack { get; set; }

        /// <summary>
        /// 接收卡券用户的openid
        /// </summary>
        public string FriendUserName { get; set; }

        /// <summary>
        /// 是否是群转赠
        /// </summary>
        public int IsChatRoom { get; set; }
    }
}
