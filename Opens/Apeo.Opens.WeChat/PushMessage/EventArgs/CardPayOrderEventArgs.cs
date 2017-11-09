using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin.PushMessage
{
    public class CardPayOrderEventArgs:EventArgsBase
    {
        /// <summary>
        /// 本次推送对应的订单号
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 本次订单号的状态
        /// </summary>
        public CardPayOrderStatus Status { get; set; }

        /// <summary>
        /// 购买券点时，支付二维码的生成时间
        /// </summary>
        public int CreateOrderTime { get; set; }

        /// <summary>
        /// 购买券点时，实际支付成功的时间
        /// </summary>
        public int PayFinishTime { get; set; }

        /// <summary>
        /// 支付方式，一般为微信支付充值
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 剩余免费券点数量
        /// </summary>
        public string FreeCoinCount { get; set; }

        /// <summary>
        /// 剩余付费券点数量
        /// </summary>
        public string PayCoinCount { get; set; }

        /// <summary>
        /// 本次变动的免费券点数量
        /// </summary>
        public string RefundFreeCoinCount { get; set; }

        /// <summary>
        /// 本次变动的付费券点数量
        /// </summary>
        public string RefundPayCoinCount { get; set; }

        /// <summary>
        /// 所要拉取的订单类型
        /// </summary>
        public CardPayOrderType OrderType { get; set; }

        /// <summary>
        /// 系统备注，说明此次变动的缘由，如开通账户奖励、门店奖励、核销奖励以及充值、扣减
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 所开发票的详情
        /// </summary>
        public string ReceiptInfo { get; set; }
    }
}
