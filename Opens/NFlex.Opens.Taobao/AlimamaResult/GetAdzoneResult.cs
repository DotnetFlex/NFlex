using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Taobao.AlimamaResult
{
    public class GetAdzoneResult
    {
        public Data data { get; set; }

        public class Data
        {
            public List<WebInfo> webList { get; set; }
            public List<Adzone> webAdzones { get; set; }
            public List<Adzone> otherAdzones { get; set; }
        }
        public class WebInfo
        {
            public string description { get; set; }
            public string name { get; set; }
            public string recordno { get; set; }
            public string siteId { get; set; }
            public string typeName { get; set; }
            public string type { get; set; }
            public string url { get; set; }
        }

        public class Adzone
        {
            public string id { get; set; }
            public string name { get; set; }
            public List<AdzoneItem> sub { get; set; }
        }

        public class AdzoneItem
        {
            public string id { get; set; }
            public string name { get; set; }
        }
    }
}
