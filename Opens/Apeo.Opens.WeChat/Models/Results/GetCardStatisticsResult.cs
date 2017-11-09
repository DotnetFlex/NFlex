using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin.Models.Results
{
    public class GetCardStatisticsResult:Result
    {
        public List<Data> list { get; set; }
        public class Data
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
            /// 转赠次数
            /// </summary>
            public int given_cnt { get; set; }

            /// <summary>
            /// 转赠人数
            /// </summary>
            public int given_user { get; set; }

            /// <summary>
            /// 过期次数
            /// </summary>
            public int expire_cnt { get; set; }

            /// <summary>
            /// 过期人数
            /// </summary>
            public int expire_user { get; set; }
        }
    }
}
