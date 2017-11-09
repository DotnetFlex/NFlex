using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin.Models.Results
{
    public class GetNewsMaterialResult:Result
    {
        public List<Article> news_item { get; set; }
    }
}
