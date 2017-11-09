using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NFlex.Opens.Weixin.PushMessage
{
    [XmlRoot("xml")]
    public class WithKeyEventArgs:EventArgsBase
    {
        /// <summary>
        /// 事件KEY值
        /// </summary>
        public string EventKey { get; set; }
    }
}
