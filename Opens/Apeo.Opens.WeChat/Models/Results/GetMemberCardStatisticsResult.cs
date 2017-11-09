using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin.Models.Results
{
    public class GetMemberCardStatisticsResult:Result
    {
        /// <summary>
         /// 日期信息
         /// </summary>
        public DateTime ref_date { get; set; }

        /// <summary>
        /// 浏览次数
        /// </summary>
        public int view_cnt { get; set; }

        /// <summary>
        /// 浏览人数
        /// </summary>
        public int view_user { get; set; }

        /// <summary>
        /// 领取次数
        /// </summary>
        public int receive_cnt { get; set; }

        /// <summary>
        /// 领取人数
        /// </summary>
        public int receive_user { get; set; }

        /// <summary>
        /// 使用次数
        /// </summary>
        public int verify_cnt { get; set; }

        /// <summary>
        /// 使用人数
        /// </summary>
        public int verify_user { get; set; }

        /// <summary>
        /// 激活人数
        /// </summary>
        public int active_user { get; set; }

        /// <summary>
        /// 有效会员总人数
        /// </summary>
        public int total_user { get; set; }

        /// <summary>
        /// 历史领取会员卡总人数
        /// </summary>
        public int total_receive_user { get; set; }
    }
}
