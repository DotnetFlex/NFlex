using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin.Models.Results
{
    public class GetDepositCountResult:Result
    {
        /// <summary>
        /// 已导入Code数量
        /// </summary>
        public int count { get; set; }
    }
}
