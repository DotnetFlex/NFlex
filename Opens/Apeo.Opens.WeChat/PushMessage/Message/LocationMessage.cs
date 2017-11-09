using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NFlex.Opens.Weixin.PushMessage
{

    [XmlRoot("xml")]
    public class LocationMessage:MessageBase
    {
        /// <summary>
        /// 地理位置维度
        /// </summary>
        public decimal Location_X { get; set; }

        /// <summary>
        /// 地理位置经度
        /// </summary>
        public decimal Location_Y { get; set; }

        /// <summary>
        /// 地图缩放大小
        /// </summary>
        public int Scale { get; set; }

        /// <summary>
        /// 地理位置信息
        /// </summary>
        public string Label { get; set; }
    }
}
