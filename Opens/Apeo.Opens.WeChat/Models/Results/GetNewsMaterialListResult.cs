using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin.Models.Results
{
    public class GetNewsMaterialListResult:Result
    {
        public int total_count { get; set; }
        public int item_count { get; set; }
        public List<Item> item { get; set; }

        public class Item
        {
            public string media_id { get; set; }
            public int update_time { get; set; }
            public Content content { get; set; }
        }

        public class Content
        {
            public List<ArticleItem> news_item { get; set; }
        }

        public class ArticleItem:Article
        {
            /// <summary>
            /// 图文页的URL，或者，当获取的列表是图片素材列表时，该字段是图片的URL
            /// </summary>
            public string url { get; set; }

            /// <summary>
            /// 图文消息的封面图片的地址
            /// </summary>
            public string thumb_url { get; set; }
        }
    }
}
