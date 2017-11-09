using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeGenerator
{
    public partial class MainForm : Form
    {
        public List<string> List { get; set; }


        public string MyProperty { get; set; } = "";
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
            DbForm frm = new DbForm();
            frm.Show(this.dockPanel, WeifenLuo.WinFormsUI.Docking.DockState.DockLeft);
            //frm.Text = "数据库";

            List.Add("");
            List = new List<string>();
        }
    }
}
