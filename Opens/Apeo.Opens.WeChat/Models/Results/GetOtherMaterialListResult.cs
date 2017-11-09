using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin.Models.Results
{
    public class GetOtherMaterialListResult:Result
    {
        public int total_count { get; set; }
        public int item_count { get; set; }
        public List<Item> item { get; set; }

        public class Item
        {
            public string media_id { get; set; }
            public string name { get; set; }
            public int update_time { get; set; }
            public string url { get; set; }
        }
    }
}
