using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NFlex;

namespace CodeGenerator
{
    public partial class ConnectForm : Form
    {
        public ConnectionInfo ConnectionInfo { get; set; }
        public ConnectForm()
        {
            InitializeComponent();
            cbValidateType.Items.Add(new Item("Windows 身份验证", "Windows"));
            cbValidateType.Items.Add(new Item("SQL Server 身份验证", "SqlServer"));
            cbValidateType.SelectedIndex = 0;
            pnlUserInfo.Enabled = false;

            //cbServerHost.Text = "192.168.1.156";
            //cbValidateType.SelectedIndex = 1;
            //txtUserName.Text = "sa";
            //txtPassword.Text = "F5H295WxcN";

            cbServerHost.Text = ".";
            cbValidateType.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            ConnectionInfo = new ConnectionInfo
            {
                Password = txtPassword.Text,
                Server = cbServerHost.Text,
                UserName = txtUserName.Text,
                ValidateType = ((Item)cbValidateType.SelectedItem).Value
            };
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionInfo.GetConnectionString("master")))
                {
                    conn.Open();
                    conn.Close();
                }
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "连接失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbValidateType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = cbValidateType.SelectedItem as Item;
            if (item.Value == "Windows")
                pnlUserInfo.Enabled = false;
            else
                pnlUserInfo.Enabled = true;
        }

        private void ConnectForm_Load(object sender, EventArgs e)
        {
            cbServerHost.Text = "192.168.1.175";
            cbValidateType.SelectedIndex = 1;
            txtUserName.Text = "sa";
            txtPassword.Text = "1qaz@WSX";
        }
    }
}
