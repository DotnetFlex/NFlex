using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin.Models.Results
{
    public class GetCustomerServiceListResult:Result
    {
        public List<CustomerServiceAccount> kf_list { get; set; }
    }
}
