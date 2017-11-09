using RazorEngine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using RazorEngine.Templating;
using System.IO;
using System.Diagnostics;
using CodeGenerator.Entities;

namespace CodeGenerator
{
    public partial class MainForm : Form
    {
        public List<string> List { get; set; }
        DatabaseForm frmDatabase;
        OutputForm frmOutput;

        public MainForm()
        {
            InitializeComponent();
            this.dockPanel.Theme = new WeifenLuo.WinFormsUI.Docking.VS2015BlueTheme();
            vsToolStripExtender.SetStyle(toolBar, WeifenLuo.WinFormsUI.Docking.VisualStudioToolStripExtender.VsVersion.Vs2015, dockPanel.Theme);
            vsToolStripExtender.SetStyle(statusBar, WeifenLuo.WinFormsUI.Docking.VisualStudioToolStripExtender.VsVersion.Vs2015, dockPanel.Theme);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            frmDatabase = new DatabaseForm();
            frmDatabase.Compile += FrmDatabase_Compile;
            frmDatabase.Show(this.dockPanel, WeifenLuo.WinFormsUI.Docking.DockState.DockLeft);

            frmOutput = new OutputForm();
            frmOutput.Show(dockPanel, WeifenLuo.WinFormsUI.Docking.DockState.DockBottomAutoHide);

        }

        private async void FrmDatabase_Compile(DatabaseInfo database, List<Schemas.TableInfo> selectedTables, List<string> selectedTemplates, CompileTarget target,string outputFolder)
        {
            try
            {
                frmOutput.Clear();
                frmOutput.DockState = WeifenLuo.WinFormsUI.Docking.DockState.DockBottom;

                Stopwatch sw = new Stopwatch();
                sw.Start();
                frmOutput.WriteLine("-----已启动生成-----");
                frmOutput.WriteLine("正在读取数据库结构...");
                var tableSchema = await database.ReLoadDatabaseSchema();
                frmOutput.WriteLine("正在准备数据...");
                var _selectTableNames = selectedTables.Select(t => t.ToString()).ToList();
                var tables = tableSchema.Where(t => _selectTableNames.Contains(t.ToString())).ToList();
                frmOutput.WriteLine("正在加载模板...");

                var templates = new List<TemplateInfo>();
                foreach(string tmpFile in selectedTemplates)
                {
                    templates.Add(TemplateInfo.Get(tmpFile));
                }

                frmOutput.WriteLine("正在生成...");
                
                var singleTemplates = templates.Where(t => t.Config.isSingle);
                var entityTemplates = templates.Where(t => !t.Config.isSingle);

                if (singleTemplates.Any())
                {
                    var model = new DatabaseModel
                    {
                        DatabaseName = database.DatabaseName,
                        Tables = tables
                    };
                    foreach (var template in singleTemplates)
                    {
                        model.TemplateFile = template.Name;
                        string fileName = "";
                        string content = Razor.Parse(template.Content, model);
                        if (target == CompileTarget.File)
                        {
                            fileName = Path.Combine(outputFolder, template.Config.folder);
                            fileName = Path.Combine(fileName, string.Format(template.Config.fileName, model.DatabaseName));// string.Format("{0}{1}", Path.GetFileNameWithoutExtension(template.Name), template.Config.fileName));
                            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.Write);
                            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(content);
                            fs.Write(bytes, 0, bytes.Length);
                            fs.Close();
                        }
                        else
                        {
                            fileName = string.Format("{0}{1}", Path.GetFileNameWithoutExtension(template.Name), template.Config.fileName);
                            CodeForm frmCode = new CodeForm(content);
                            frmCode.Text = fileName;
                            frmCode.Show(dockPanel);
                        }
                        frmOutput.WriteLine(string.Format(">{0}", fileName));
                    }
                }

                for (int i = 0; i < tables.Count; i++)
                {
                    var table = tables[i];
                    var model = new TableModel
                    {
                        DatabaseName = database.DatabaseName,
                        Table = table
                    };
                    frmOutput.WriteLine(string.Format("{0}>{1}", i + 1, table.TableName));
                    foreach (var template in templates.Where(t=>!t.Config.isSingle))
                    {
                        model.TemplateFile = template.Name;
                        string fileName = "";
                        string content = Razor.Parse(template.Content, model);
                        if (target == CompileTarget.File)
                        {
                            fileName = Path.Combine(outputFolder, template.Config.folder);
                            if (!Directory.Exists(fileName)) Directory.CreateDirectory(fileName);
                            fileName = Path.Combine(fileName, string.Format(template.Config.fileName, table.TableName));// string.Format("{0}{1}", table.TableName, template.Config.fileName));
                            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.Write);
                            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(content);
                            fs.Write(bytes, 0, bytes.Length);
                            fs.Close();
                        }
                        else
                        {
                            fileName = string.Format(template.Config.fileName, table.TableName);// string.Format("{0}{1}", table.TableName, template.Config.fileName);
                            CodeForm frmCode = new CodeForm(content);
                            frmCode.Text = fileName;
                            frmCode.Show(dockPanel);
                        }
                        frmOutput.WriteLine(string.Format(">{0}", fileName));
                    }
                }

                sw.Stop();
                frmOutput.WriteLine(string.Format("=====生成完成  总用时：{0} 毫秒=====",sw.ElapsedMilliseconds));
                frmOutput.DockState = WeifenLuo.WinFormsUI.Docking.DockState.DockBottomAutoHide;
            }
            catch(Exception ex)
            {
                frmOutput.WriteLine("生成错误：" + ex.Message);
            }
        }
    }
}
