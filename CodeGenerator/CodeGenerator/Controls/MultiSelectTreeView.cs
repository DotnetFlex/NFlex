using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeGenerator.Controls
{
    /// <summary>
    /// Summary description for MultiSelectTreeView.
    /// The MultiSelectTreeView inherits from System.Windows.Forms.TreeView to 
    /// allow user to select multiple nodes.
    /// The underlying comctl32 TreeView doesn't support multiple selection.
    /// Hence this MultiSelectTreeView listens for the BeforeSelect && AfterSelect
    /// events to dynamically change the BackColor of the individual treenodes to
    /// denote selection. 
    /// It then adds the TreeNode to the internal arraylist of currently
    /// selectedNodes after validation checks.
    /// 
    /// The MultiSelectTreeView supports
    ///	1) Select + Control will add the current node to list of SelectedNodes
    ///	2) Select + Shitft  will add the current node and all the nodes between the two 
    ///	(if the start node and end node is at the same level)
    ///	3) Control + A when the MultiSelectTreeView has focus will select all Nodes.
    ///	
    /// 
    /// </summary>
    public class MultiSelectTreeView : System.Windows.Forms.TreeView
    {
        /// <summary>
        ///  This is private member which caches the last treenode user clicked
        /// </summary>
        private TreeNode lastNode;

        /// <summary>
        ///  This is private member stores the list of SelectedNodes
        /// </summary>
        private ArrayList selectedNodes;

        /// <summary>
        ///  This is private member which caches the first treenode user clicked
        /// </summary>
        private TreeNode firstNode;

        /// <summary>
        /// The constructor which initialises the MultiSelectTreeView.
        /// </summary>
        public MultiSelectTreeView()
        {
            selectedNodes = new ArrayList();
            FullRowSelect = true;
            ShowLines = false;
        }

        /// <summary>
        /// The constructor which initialises the MultiSelectTreeView.
        /// </summary>
        [
        Category("Selection"),
        Description("Gets or sets the selected nodes as ArrayList")
        ]
        public ArrayList SelectedNodes
        {
            get
            {
                return selectedNodes;
            }
            set
            {
                DeselectNodes();
                selectedNodes.Clear();
                selectedNodes = value;
                SelectNodes();
            }
        }

        #region overrides
        /// <summary>
        ///	If the user has pressed "Control+A" keys then select all nodes.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            bool Pressed = (e.Control && ((e.KeyData & Keys.A) == Keys.A));
            if (Pressed)
            {
                SelectAllNodes(this.Nodes);
            }
        }
        /// <summary>
        ///	This Function starts the multiple selection.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnBeforeSelect(TreeViewCancelEventArgs e)
        {
            base.OnBeforeSelect(e);

            //Check for the current keys press..	
            bool isControlPressed = (ModifierKeys == Keys.Control);
            bool isShiftPressed = (ModifierKeys == Keys.Shift);

            //If control is pressed and the selectedNodes contains current Node
            //Deselect that node...
            //Remove from the selectedNodes Collection...
            if (isControlPressed && selectedNodes.Contains(e.Node))
            {
                DeselectNodes();
                selectedNodes.Remove(e.Node);
                SelectNodes();
                //MultiSelectTreeView has handled this event ....
                //Windows.Forms.TreeView should eat this event.
                e.Cancel = true;
                return;
            }

            //else (if Shift key is pressed)
            //Start the multiselection ...
            //Since Shift is pressed we would "SELECT" 
            ///all nodes from first node - to last node
            lastNode = e.Node;

            //If Shift not pressed...
            //Remember this Node to be the Sart Node .. in case user presses Shift to 
            //select multiple nodes.
            if (!isShiftPressed) firstNode = e.Node;
        }

        /// <summary>
        ///	This function ends the multi selection. Also adds and removes the node to
        ///	the selectedNodes depending upon the keys prssed.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            base.OnAfterSelect(e);

            //Check for the current keys press..
            bool isControlPressed = (ModifierKeys == Keys.Control);
            bool isShiftPressed = (ModifierKeys == Keys.Shift);

            if (isControlPressed)
            {
                if (!selectedNodes.Contains(e.Node))
                {
                    //This is a new Node, so add it to the list.
                    selectedNodes.Add(e.Node);
                }
                else
                {
                    //If control is pressed and the selectedNodes contains current Node
                    //Deselect that node...
                    //Remove from the selectedNodes Collection...
                    DeselectNodes();
                    selectedNodes.Remove(e.Node);
                }
                SelectNodes();
            }
            else
            {
                // SHIFT is pressed
                if (isShiftPressed)
                {
                    //Start Looking for the start and end nodes to select all the nodes between them.	
                    TreeNode uppernode = firstNode;
                    TreeNode bottomnode = e.Node;
                    //Check Parenting Upper ---> Bottom
                    //Is Upper Node parent (direct or indirect) of Bottom Node
                    bool bParent = CheckIfParent(uppernode, bottomnode);
                    if (!bParent)
                    {
                        //Check Parenting the other way round
                        bParent = CheckIfParent(bottomnode, uppernode);
                        if (bParent) // SWAPPING
                        {
                            TreeNode temp = uppernode;
                            uppernode = bottomnode;
                            bottomnode = temp;
                        }
                    }
                    if (bParent)
                    {
                        TreeNode n = bottomnode;
                        while (n != uppernode.Parent)
                        {
                            if (!selectedNodes.Contains(n))
                                selectedNodes.Add(n);
                            n = n.Parent;
                        }
                    }
                    // Parenting Fails ... but check if the NODES are on the same LEVEL.
                    else
                    {
                        if ((uppernode.Parent == null && bottomnode.Parent == null) || (uppernode.Parent != null && uppernode.Parent.Nodes.Contains(bottomnode))) // are they siblings ?
                        {
                            int nIndexUpper = uppernode.Index;
                            int nIndexBottom = bottomnode.Index;
                            //Need to SWAP if the order is reversed...
                            if (nIndexBottom < nIndexUpper)
                            {
                                TreeNode temp = uppernode;
                                uppernode = bottomnode;
                                bottomnode = temp;
                                nIndexUpper = uppernode.Index;
                                nIndexBottom = bottomnode.Index;
                            }

                            TreeNode n = uppernode;
                            selectedNodes.Clear();
                            while (nIndexUpper < nIndexBottom)
                            {
                                //Add all the nodes if nodes not present in the current
                                //SelectedNodes list...

                                if (!selectedNodes.Contains(n))
                                {
                                    selectedNodes.Add(n);
                                    SelectAllNodesInNode(n.Nodes, n);
                                }
                                n = n.NextNode;
                                nIndexUpper++;
                            }
                            //Add the Last Node.
                            selectedNodes.Add(n);
                        }
                        else
                        {
                            if (!selectedNodes.Contains(uppernode)) selectedNodes.Add(uppernode);
                            if (!selectedNodes.Contains(bottomnode)) selectedNodes.Add(bottomnode);
                        }
                    }
                    SelectNodes();
                    //Reset the firstNode counter for subsequent "SHIFT" keys.
                    firstNode = e.Node;
                }
                else
                {
                    // If Normal selection then add this to SelectedNodes Collection.
                    if (selectedNodes != null && selectedNodes.Count > 0)
                    {
                        DeselectNodes();
                        selectedNodes.Clear();
                    }
                    selectedNodes.Add(e.Node);
                }
            }
        }

        /// <summary>
        ///	Overriden OnLostFocus to mimic TreeView's behavior of de-selecting nodes.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            DeselectNodes();
        }

        /// <summary>
        ///	Overriden OnGotFocus to mimic TreeView's behavior of selecting nodes.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            SelectNodes();
        }



        #endregion overrides

        /// <summary>
        ///	Private function to check the parenting of the two nodes passed.
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="childNode"></param>
        /// <returns></returns>
        private bool CheckIfParent(TreeNode parentNode, TreeNode childNode)
        {
            if (parentNode == childNode)
                return true;

            TreeNode node = childNode;
            bool parentFound = false;
            while (!parentFound && node != null)
            {
                node = node.Parent;
                parentFound = (node == parentNode);
            }
            return parentFound;
        }

        /// <summary>
        ///	This function provides the user feedback that the node is selected
        ///	Basically the BackColor and the ForeColor is changed for all
        ///	the nodes in the selectedNodes collection.
        /// </summary>
        private void SelectNodes()
        {
            foreach (TreeNode n in selectedNodes)
            {
                n.BackColor = SystemColors.Highlight;
                n.ForeColor = SystemColors.HighlightText;
            }
        }

        /// <summary>
        ///	This function provides the user feedback that the node is de-selected
        ///	Basically the BackColor and the ForeColor is changed for all
        ///	the nodes in the selectedNodes collection.
        /// </summary>
        private void DeselectNodes()
        {
            if (selectedNodes.Count == 0) return;

            TreeNode node = (TreeNode)selectedNodes[0];
            Color backColor = node.TreeView.BackColor;
            Color foreColor = node.TreeView.ForeColor;

            foreach (TreeNode n in selectedNodes)
            {
                n.BackColor = backColor;
                n.ForeColor = foreColor;
            }

        }

        /// <summary>
        ///	This function selects all the Nodes in the MultiSelectTreeView..
        /// </summary>
        private void SelectAllNodes(TreeNodeCollection nodes)
        {
            foreach (TreeNode n in this.Nodes)
            {
                selectedNodes.Add(n);
                if (n.Nodes.Count > 1)
                {
                    SelectAllNodesInNode(n.Nodes, n);
                }

            }
            SelectNodes();
        }

        /// <summary>
        ///	Recursive function selects all the Nodes in the MultiSelectTreeView's Node
        /// </summary>
        private void SelectAllNodesInNode(TreeNodeCollection nodes, TreeNode node)
        {
            foreach (TreeNode n in node.Nodes)
            {
                selectedNodes.Add(n);
                if (n.Nodes.Count > 1)
                {
                    SelectAllNodesInNode(n.Nodes, n);
                }
            }
            SelectNodes();
        }

    }
}
