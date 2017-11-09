using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin.Models.Results
{
    public class BatchGetUserInfoResult:Result
    {
        public List<UserInfo> user_info_list { get; set; }
    }
}
