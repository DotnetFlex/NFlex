using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin.PushMessage
{
    public class UserGetCardEventArgs:EventArgsBase
    {
        /// <summary>
        /// 卡券ID
        /// </summary>
        public string CardId { get; set; }

        /// <summary>
        /// 是否为转赠领取，1代表是，0代表否
        /// </summary>
        public int IsGiveByFriend { get; set; }

        /// <summary>
        /// code序列号
        /// </summary>
        public string UserCardCode { get; set; }

        /// <summary>
        /// 当IsGiveByFriend为1时填入的字段，表示发起转赠用户的openid
        /// </summary>
        public string FriendUserName { get; set; }

        /// <summary>
        /// 领取场景值，用于领取渠道数据统计。可在生成二维码接口及添加Addcard接口中自定义该字段的字符串值
        /// </summary>
        public string OuterStr { get; set; }

        /// <summary>
        /// 为保证安全，微信会在转赠发生后变更该卡券的code号，该字段表示转赠前的code
        /// </summary>
        public string OldUserCardCode { get; set; }

        /// <summary>
        /// 用户删除会员卡后可重新找回，当用户本次操作为找回时，该值为1，否则为0
        /// </summary>
        public int IsRestoreMemberCard { get; set; }
        public int IsRecommendByFriend { get; set; }
    }
}
