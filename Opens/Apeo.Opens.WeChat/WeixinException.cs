using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin
{
    public class WeixinException:Exception
    {
        public WeixinException(string message) : base(message) { }
        public WeixinException(Exception ex) : base(ex.Message, ex) { }
    }
}
