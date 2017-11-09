namespace CodeGenerator
{
    partial class DatabaseForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.vS2005Theme1 = new WeifenLuo.WinFormsUI.Docking.VS2005Theme();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnConnect = new System.Windows.Forms.ToolStripButton();
            this.cbbDatabaseList = new System.Windows.Forms.ToolStripComboBox();
            this.lbTables = new System.Windows.Forms.ListBox();
            this.cmsTableList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsTableList_reverse = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsTableList_selectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsTableList_reload = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsTableList_compile = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsTableList_compileToFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.cmsTableList.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnConnect,
            this.cbbDatabaseList});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.MinimumSize = new System.Drawing.Size(250, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(278, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnConnect
            // 
            this.btnConnect.Image = global::CodeGenerator.Properties.Resources._00194;
            this.btnConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(61, 22);
            this.btnConnect.Text = "连接...";
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // cbbDatabaseList
            // 
            this.cbbDatabaseList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbDatabaseList.Name = "cbbDatabaseList";
            this.cbbDatabaseList.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.cbbDatabaseList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbbDatabaseList.Size = new System.Drawing.Size(121, 25);
            this.cbbDatabaseList.SelectedIndexChanged += new System.EventHandler(this.cbbDatabaseList_SelectedIndexChanged);
            // 
            // lbTables
            // 
            this.lbTables.CausesValidation = false;
            this.lbTables.ContextMenuStrip = this.cmsTableList;
            this.lbTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbTables.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTables.FormattingEnabled = true;
            this.lbTables.IntegralHeight = false;
            this.lbTables.ItemHeight = 17;
            this.lbTables.Location = new System.Drawing.Point(0, 25);
            this.lbTables.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lbTables.Name = "lbTables";
            this.lbTables.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbTables.Size = new System.Drawing.Size(278, 586);
            this.lbTables.TabIndex = 1;
            // 
            // cmsTableList
            // 
            this.cmsTableList.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.cmsTableList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsTableList_reverse,
            this.cmsTableList_selectAll,
            this.toolStripMenuItem1,
            this.cmsTableList_reload,
            this.toolStripSeparator1,
            this.cmsTableList_compile,
            this.cmsTableList_compileToFile});
            this.cmsTableList.Name = "cmsTableList";
            this.cmsTableList.Size = new System.Drawing.Size(153, 148);
            // 
            // cmsTableList_reverse
            // 
            this.cmsTableList_reverse.Name = "cmsTableList_reverse";
            this.cmsTableList_reverse.Size = new System.Drawing.Size(152, 22);
            this.cmsTableList_reverse.Text = "反选";
            // 
            // cmsTableList_selectAll
            // 
            this.cmsTableList_selectAll.Name = "cmsTableList_selectAll";
            this.cmsTableList_selectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.cmsTableList_selectAll.Size = new System.Drawing.Size(152, 22);
            this.cmsTableList_selectAll.Text = "全选";
            this.cmsTableList_selectAll.Click += new System.EventHandler(this.cmsTableList_selectAll_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // cmsTableList_reload
            // 
            this.cmsTableList_reload.Name = "cmsTableList_reload";
            this.cmsTableList_reload.Size = new System.Drawing.Size(152, 22);
            this.cmsTableList_reload.Text = "刷新";
            this.cmsTableList_reload.Click += new System.EventHandler(this.cmsTableList_reload_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // cmsTableList_compile
            // 
            this.cmsTableList_compile.Name = "cmsTableList_compile";
            this.cmsTableList_compile.Size = new System.Drawing.Size(152, 22);
            this.cmsTableList_compile.Text = "生成到新窗口";
            this.cmsTableList_compile.Click += new System.EventHandler(this.cmsTableList_compile_Click);
            // 
            // cmsTableList_compileToFile
            // 
            this.cmsTableList_compileToFile.Name = "cmsTableList_compileToFile";
            this.cmsTableList_compileToFile.Size = new System.Drawing.Size(152, 22);
            this.cmsTableList_compileToFile.Text = "生成到文件";
            this.cmsTableList_compileToFile.Click += new System.EventHandler(this.cmsTableList_compileToFile_Click);
            // 
            // DatabaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(278, 611);
            this.CloseButton = false;
            this.Controls.Add(this.lbTables);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.HideOnClose = true;
            this.Name = "DatabaseForm";
            this.Text = "数据库资源管理器";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.cmsTableList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private WeifenLuo.WinFormsUI.Docking.VS2005Theme vS2005Theme1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnConnect;
        private System.Windows.Forms.ToolStripComboBox cbbDatabaseList;
        private System.Windows.Forms.ListBox lbTables;
        private System.Windows.Forms.ContextMenuStrip cmsTableList;
        private System.Windows.Forms.ToolStripMenuItem cmsTableList_reverse;
        private System.Windows.Forms.ToolStripMenuItem cmsTableList_selectAll;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem cmsTableList_compile;
        private System.Windows.Forms.ToolStripMenuItem cmsTableList_reload;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem cmsTableList_compileToFile;
    }
}