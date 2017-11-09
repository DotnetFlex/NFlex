using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin.Models.Results
{
    public class CheckCodeResult:Result
    {
        /// <summary>
        /// 已存在的Code
        /// </summary>
        public List<string> exist_code { get; set; }

        /// <summary>
        /// 不存在的Code
        /// </summary>
        public List<string> not_exist_code { get; set; }
    }
}
