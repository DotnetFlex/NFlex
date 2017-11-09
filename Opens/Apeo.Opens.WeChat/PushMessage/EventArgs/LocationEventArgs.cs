using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NFlex.Opens.Weixin.PushMessage
{
    [XmlRoot("xml")]
    public class LocationEventArgs:EventArgsBase
    {
        /// <summary>
        /// 地理位置纬度
        /// </summary>
        public decimal Latitude { get; set; }

        /// <summary>
        /// 地理位置经度
        /// </summary>
        public decimal Longitude { get; set; }

        /// <summary>
        /// 地理位置精度
        /// </summary>
        public decimal Precision { get; set; }
    }
}
