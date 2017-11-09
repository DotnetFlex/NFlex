using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NFlex.Opens.Weixin.PushMessage
{
    [XmlRoot("xml")]
    public class ScanEventArgs : WithKeyEventArgs
    {
        /// <summary>
        /// 二维码的ticket
        /// </summary>
        public string Ticket { get; set; }
    }
}
