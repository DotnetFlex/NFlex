using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin.Models.Results
{
    public class GetTagUsersResult:Result
    {
        public int count { get; set; }
        public GetTagUsersData data { get; set; }
        public string next_openid { get; set; }

        public class GetTagUsersData
        {
            public List<string> openid { get; set; }
        }
    }

}
