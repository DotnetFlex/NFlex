using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin.Models.Results
{
    public class CreateCardQrcodeResult:Result
    {
        /// <summary>
        /// 获取的二维码ticket，凭借此ticket调用通过ticket换取二维码接口可以在有效时间内换取二维码
        /// </summary>
        public string ticket { get; set; }

        /// <summary>
        /// 二维码的有效时间（单位：秒）
        /// </summary>
        public int expire_seconds { get; set; }

        /// <summary>
        /// 二维码图片解析后的地址，开发者可根据该地址自行生成需要的二维码图片
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 二维码显示地址，点击后跳转二维码页面
        /// </summary>
        public string show_qrcode_url { get; set; }
    }
}
