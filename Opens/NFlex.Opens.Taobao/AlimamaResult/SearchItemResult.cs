using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Taobao.AlimamaResult
{
    public class SearchItemResult
    {
        public Data data { get; set; }

        public class Data
        {
            public List<Item> pageList { get; set; }
        }

        public class Item
        {
            public string auctionId { get; set; }
            public string auctionUrl { get; set; }
            public string biz30day { get; set; }
            public decimal couponAmount { get; set; }
            public DateTime? couponEffectiveEndTime { get; set; }
            public DateTime? couponEffectiveStartTime { get; set; }
            public string couponInfo { get; set; }
            public int couponLeftCount { get; set; }
            public int couponTotalCount { get; set; }
            public int dayLeft { get; set; }
            public string leafCatId { get; set; }
            public string nick { get; set; }
            public string picUrl { get; set; }
            public decimal reservePrice { get; set; }
            public decimal rlRate { get; set; }
            public string rootCatId { get; set; }
            public string sameItemPid { get; set; }
            public string sellerId { get; set; }
            public string shopTitle { get; set; }
            public string title { get; set; }
            public decimal tkCommFee { get; set; }
            public decimal tkCommonFee { get; set; }
            public decimal tkCommonRate { get; set; }
            public decimal tkRate { get; set; }
            public decimal totalFee { get; set; }
            public int totalNum { get; set; }
            public decimal zkPrice { get; set; }
        }
    }
}
