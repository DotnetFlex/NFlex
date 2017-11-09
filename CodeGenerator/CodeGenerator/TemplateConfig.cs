using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CodeGenerator
{
    [XmlRoot("template")]
    public class TemplateConfig
    {
        [XmlAttribute]
        public string folder { get; set; }
        [XmlAttribute]
        public string fileName { get; set; }
        [XmlAttribute]
        public bool isSingle { get; set; }
    }
}
