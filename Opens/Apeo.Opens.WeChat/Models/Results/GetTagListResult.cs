using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin.Models.Results
{
    public class GetTagListResult:Result
    {
        public List<Tag> tags { get; set; }
    }
}
