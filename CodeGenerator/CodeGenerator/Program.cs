
//using Microsoft.VisualStudio.TextTemplating;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using RazorEngine;
using RazorEngine.Templating;
using System.Diagnostics;
using System.Threading;
using NFlex;

namespace CodeGenerator
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());


            //string connStr = "Data Source=.;Initial Catalog=Jewelry;Integrated Security=SSPI;";
            string connStr = "Server=192.168.1.156;Database=QuPiao_Pay;Integrated Security=false;User ID=sa;Password=F5H295WxcN;MultipleActiveResultSets=true";
            //string connStr = "Server=192.168.1.156;Database=QuPiao_Distribute;Integrated Security=false;User ID=sa;Password=F5H295WxcN;MultipleActiveResultSets=true";

            //var tmp = new Schemas.SchemaLoader().Load(connStr);

            #region Razor引擎
            //foreach (var table in tmp)
            //{
            //    TemplateConfig config = null;
            //    string templateFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "templates\\testtemplate.cshtml");
            //    string content = Apeo.File.Read(templateFile, System.Text.Encoding.UTF8).Trim();
            //    if (content.IndexOf("<template ") == 0 && content.IndexOf("</template>") != -1)
            //    {
            //        var configStr = content.Substring(0, content.IndexOf("</template>\r\n") + "</template>\r\n".Length);
            //        content = content.Replace(configStr, "");
            //        config = Apeo.Serialize.FromXml<TemplateConfig>(configStr);
            //    }


                //string fileContent = Engine.Razor.RunCompile(content, table.ToString(), null, table);
            //}
            #endregion

        }
    }
}
