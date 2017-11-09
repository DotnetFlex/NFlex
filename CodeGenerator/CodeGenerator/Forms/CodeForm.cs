using ICSharpCode.TextEditor.Document;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace CodeGenerator
{
    public partial class CodeForm : DockContentEx
    {
        public CodeForm(string code)
        {
            InitializeComponent();
            txtCode.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("C#");
            txtCode.Encoding = Encoding.UTF8;
            txtCode.Document.FoldingManager.FoldingStrategy = new MingFolding();
            txtCode.Document.FoldingManager.UpdateFoldings(null, null);
            txtCode.Text = code;
        }

        private void txtCode_TextChanged(object sender, EventArgs e)
        {
            txtCode.Document.FoldingManager.UpdateFoldings(null, null);
        }
    }
}
