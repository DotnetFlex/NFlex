using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin.Models.Results
{
    public class GetUserTagsResult:Result
    {
        public List<int> tagid_list { get; set; }
    }
}
