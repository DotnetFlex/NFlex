using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin.Models.Results
{
    public class GetIndustryResult:Result
    {
        /// <summary>
        /// 帐号设置的主营行业
        /// </summary>
        public Item primary_industry { get; set; }

        /// <summary>
        /// 帐号设置的副营行业
        /// </summary>
        public Item secondary_industry { get; set; }

        public class Item
        {
            public string first_class { get; set; }
            public string second_class { get; set; }
        }
    }
}
