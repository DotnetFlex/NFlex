using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Taobao.AlimamaResult
{
    public class CreatePromotUrlResult
    {
        public UrlInfo data { get; set; }
        public class UrlInfo
        {
            public string clickUrl { get; set; }
            public string couponLink { get; set; }
            public string couponLinkTaoToken { get; set; }
            public string couponShortLinkUrl { get; set; }
            public string qrCodeUrl { get; set; }
            public string shortLinkUrl { get; set; }
            public string taoToken { get; set; }
            public decimal tkCommonRate { get; set; }
            public string auction { get; set; }
        }
    }
}
