using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin.Models.Results
{
    public class GetVideoMaterialResult:Result
    {
        /// <summary>
        /// 视频标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 视频描述
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 下载地址
        /// </summary>
        public string down_url { get; set; }
    }
}
