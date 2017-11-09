using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin.Models
{
    public class Matchrule
    {
        /// <summary>
        /// 用户分组id，可通过用户分组管理接口获取
        /// </summary>
        public string tag_id { get; set; }

        /// <summary>
        /// 性别：男（1）女（2），不填则不做匹配
        /// </summary>
        public string sex { get; set; }

        /// <summary>
        /// 客户端版本，当前只具体到系统型号：IOS(1), Android(2),Others(3)，不填则不做匹配
        /// </summary>
        public string country { get; set; }

        /// <summary>
        /// 国家信息，是用户在微信中设置的地区，具体请参考地区信息表
        /// </summary>
        public string province { get; set; }

        /// <summary>
        /// 省份信息，是用户在微信中设置的地区，具体请参考地区信息表
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// 城市信息，是用户在微信中设置的地区，具体请参考地区信息表
        /// </summary>
        public string client_platform_type { get; set; }

        /// <summary>
        /// 语言信息，是用户在微信中设置的语言，具体请参考语言表
        /// </summary>
        public string language { get; set; }
    }
}
