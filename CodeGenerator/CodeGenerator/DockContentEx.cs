using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeifenLuo.WinFormsUI.Docking;

namespace CodeGenerator
{
    public class DockContentEx : DockContent
    {
        //在Tag列表上添加右键关闭菜单
        public DockContentEx()
        {
            System.Windows.Forms.ContextMenuStrip cms = new System.Windows.Forms.ContextMenuStrip();
            cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[]{
                new System.Windows.Forms.ToolStripMenuItem("关闭", null, tsmiClose_Click, "tsmiClose")
            ,new System.Windows.Forms.ToolStripMenuItem("关闭其他", null, tsmiClose_Click, "tsmiOtherClose")
            ,new System.Windows.Forms.ToolStripMenuItem("全部关闭", null, tsmiClose_Click, "tsmiAllClose")
            });
            //将右键菜单绑定到DockContent的TabPage上
            this.TabPageContextMenuStrip = cms;
            //也可以使用这种方式
            //this.TabPageContextMenu=newSystem.Windows.Forms.ContextMenu();
        }
        //菜单事件
        private void tsmiClose_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolStripMenuItem _tsmt = sender as System.Windows.Forms.ToolStripMenuItem;
            if (_tsmt == null) return;
            switch (_tsmt.Name)
            {
                case "tsmiClose": this.Close(); break;
                case "tsmiAllClose": HideOrCloseFrom(true); break;
                case "tsmiOtherClose": HideOrCloseFrom(); break;
                default: break;
            }
        }
        /// <summary>
        /// 隐藏其他窗体
        /// </summary>
        void HideOrCloseFrom()
        {
            HideOrCloseFrom(false, false);
        }
        /// <summary>
        /// 隐藏窗体
        /// </summary>
        /// <param name="isAll">隐藏所有还是其他窗体</param>
        void HideOrCloseFrom(bool isAll)
        {
            HideOrCloseFrom(isAll, false);
        }
        /// <summary>
        /// 关闭还是隐藏窗体
        /// </summary>
        /// <param name="isAll">是否是所有窗体，还是除当前窗体以外的其他窗体</param>
        /// <param name="isClose">是否是关闭窗体，默认为隐藏窗体</param>
        private void HideOrCloseFrom(bool isAll, bool isClose)
        {
            DockContentCollection contents = DockPanel.Contents;
            for (int i = 0; i < contents.Count; i++)
            {
                if (isAll)
                {
                    HideOrCloseFrom(contents[i].DockHandler, isClose);
                }//end if
                else if (DockPanel.ActiveContent != contents[i])
                {
                    HideOrCloseFrom(contents[i].DockHandler, isClose);
                }//END ELSE IF
            }//end for
        }//end HideOrCloseFrom
         /// <summary>
         /// 关闭或隐藏当前窗体
         /// 仅DockState状态跟当前的DockState状态相同的窗体
         /// </summary>
         /// <param name="content">当前窗体对象</param>
        void HideOrCloseFrom(DockContentHandler content, bool isClose)
        {
            if (content == null || content.DockState != this.DockState) return;
            if (isClose)
                content.Close();
            else if (!content.IsHidden)
                content.Hide();
        }
    }
}
