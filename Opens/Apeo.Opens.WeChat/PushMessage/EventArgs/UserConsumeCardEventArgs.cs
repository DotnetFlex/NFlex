using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin.PushMessage
{
    public class UserConsumeCardEventArgs:EventArgsBase
    {
        /// <summary>
        /// 卡券ID
        /// </summary>
        public string CardId { get; set; }
        /// <summary>
        /// 卡券Code码
        /// </summary>
        public string UserCardCode { get; set; }
        /// <summary>
        /// 核销来源
        /// </summary>
        public ConsumeSource ConsumeSource { get; set; }
        /// <summary>
        /// 门店名称，当前卡券核销的门店名称（只有通过自助核销和买单核销时才会出现该字段）
        /// </summary>
        public string LocationName { get; set; }
        /// <summary>
        /// 核销该卡券核销员的openid（只有通过卡券商户助手核销时才会出现）
        /// </summary>
        public string StaffOpenId { get; set; }
        /// <summary>
        /// 自助核销时，用户输入的验证码
        /// </summary>
        public string VerifyCode { get; set; }
        /// <summary>
        /// 自助核销时，用户输入的备注金额
        /// </summary>
        public string RemarkAmount { get; set; }
        /// <summary>
        /// 开发者发起核销时传入的自定义参数，用于进行核销渠道统计
        /// </summary>
        public string OuterStr { get; set; }
    }
}
