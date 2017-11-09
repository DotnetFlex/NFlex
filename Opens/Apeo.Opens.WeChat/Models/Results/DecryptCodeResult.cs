using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin.Models.Results
{
    public class DecryptCodeResult:Result
    {
        /// <summary>
        /// 解密后获取的真实Code码
        /// </summary>
        public string code { get; set; }
    }
}
