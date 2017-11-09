using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NFlex.Opens.Weixin.PushMessage
{
    [XmlRoot("xml")]
    public class EventArgsBase:PushObject
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public string Event { get; set; }
    }
}
