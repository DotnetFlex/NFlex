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
    public partial class OutputForm : DockContent
    {
        public OutputForm()
        {
            InitializeComponent();
        }

        public void Write(string message)
        {
            WriteMessage(message);
        }

        public void WriteLine(string message)
        {
            WriteMessage(message + "\r\n");
        }

        public void Clear()
        {
            if(txtMessage.InvokeRequired)
            {
                var action = new Action(Clear);
                action.Invoke();
            }
            else
            {
                txtMessage.Text = "";
            }
        }
        
        private void WriteMessage(string message)
        {
            if(txtMessage.InvokeRequired)
            {
                var action = new Action<string>(WriteMessage);
                action.Invoke(message);
            }
            else
            {
                txtMessage.AppendText(message);
            }
        }
    }
}
