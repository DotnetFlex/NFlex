using CodeGenerator.Schemas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace CodeGenerator
{
    public partial class DatabaseForm : DockContent
    {
        OpenFileDialog openDialog = new OpenFileDialog();
        FolderBrowserDialog folderDialog = new FolderBrowserDialog();

        public delegate void CompileHandler(DatabaseInfo database, List<TableInfo> selectedTables, List<string> selectedTemplates, CompileTarget target,string outputFolder);
        public event CompileHandler Compile; 
        public DatabaseForm()
        {
            InitializeComponent();
            //var initDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates");
            //if (!Directory.Exists(initDir))
            //    initDir = AppDomain.CurrentDomain.BaseDirectory;

            //openDialog.InitialDirectory = initDir;
            openDialog.Filter = "模板文件|*.*";
            openDialog.Multiselect = true;
            openDialog.Title = "选择模板";

            
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            ConnectForm frm = new CodeGenerator.ConnectForm();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                cbbDatabaseList.Items.Clear();
                var dbList = DatabaseInfo.GetDatabaseList(frm.ConnectionInfo).OrderBy(t => t.DatabaseName).ToList();
                foreach (var db in dbList)
                {
                    cbbDatabaseList.Items.Add(db);
                }
                if (dbList.Count > 0)
                {
                    cbbDatabaseList.SelectedIndex = 0;
                }
                BindTableList(false);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        private void cbbDatabaseList_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindTableList(false);
        }

        private void cmsTableList_selectAll_Click(object sender, EventArgs e)
        {
            for(int i=0;i<lbTables.Items.Count;i++)
            {
                lbTables.SetSelected(i, true);
            }
        }

        private void cmsTableList_reverse_Click(object sender, EventArgs e)
        {
            for(int i=0;i<lbTables.Items.Count;i++)
            {
                var selectedStatus = !lbTables.GetSelected(i);
                lbTables.SetSelected(i, selectedStatus);
            }
        }

        private void cmsTableList_reload_Click(object sender, EventArgs e)
        {
            BindTableList(true);
        }

        private async void BindTableList(bool reload)
        {
            lbTables.Items.Clear();
            lbTables.Items.Add("正在加载...");
            var dbInfo = cbbDatabaseList.SelectedItem as DatabaseInfo;
            if (dbInfo != null)
            {
                var tables = await dbInfo.LoadTablesAsync(reload);
                lbTables.Items.Clear();
                foreach (var table in tables)
                {
                    lbTables.Items.Add(table);
                }
            }
        }

        private void cmsTableList_compile_Click(object sender, EventArgs e)
        {
            CompileClick(CompileTarget.NewWindow);
        }

        private void cmsTableList_compileToFile_Click(object sender, EventArgs e)
        {
            CompileClick(CompileTarget.File);
        }

        private void CompileClick(CompileTarget target)
        {
            if (openDialog.ShowDialog() != DialogResult.OK) return;
            string folder = "";
            if(target==CompileTarget.File)
                if (folderDialog.ShowDialog() != DialogResult.OK) return;

            folder = folderDialog.SelectedPath;

            var database = cbbDatabaseList.SelectedItem as DatabaseInfo;
            var templates = openDialog.FileNames.ToList();
            var selectedTables = lbTables.SelectedItems.OfType<TableInfo>().ToList();

            if (database == null || templates.Count == 0 || selectedTables.Count == 0) return;

            Compile?.Invoke(database, selectedTables, templates, target,folder);
        }
    }
}
