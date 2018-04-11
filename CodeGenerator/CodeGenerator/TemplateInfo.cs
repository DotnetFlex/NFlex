using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFlex;

namespace CodeGenerator
{
    public class TemplateInfo
    {
        public string Content { get; set; }
        public TemplateConfig Config { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }

        public static TemplateInfo Get(string fileName)
        {
            var info = new TemplateInfo();
            info.Key = fileName;
            info.Name = Path.GetFileName(fileName);
            string content = NFlex.Files.Read(fileName, Encoding.UTF8).Trim();
            if (content.IndexOf("<template ") == 0 && content.IndexOf("</template>") != -1)
            {
                var configStr = content.Substring(0, content.IndexOf("</template>\r\n") + "</template>\r\n".Length);
                info.Content = content.Replace(configStr, "");
                info.Config = Xml.ToObject< TemplateConfig>(configStr);
                info.Config.fileName = string.IsNullOrEmpty(info.Config.fileName) ? ".cs" : info.Config.fileName;
                return info;
            }
            else
            {
                return null;
            }
        }
    }
}
