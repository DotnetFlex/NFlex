using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin.Models.Results
{
    public class CreateLandingPageResult:Result
    {
        public string url { get; set; }
        public int page_id { get; set; }
    }
}
