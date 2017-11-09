using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin.Models.Results
{
    public class GetUserListResult:Result
    {
        public int total { get; set; }
        public int count { get; set; }
        public string next_openid { get; set; }
        public Data data { get; set; }

        public class Data
        {
            public List<string> openid { get; set; }
        }
    }
}
