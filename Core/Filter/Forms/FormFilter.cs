using Core.Data.Table;
using Core.Filter.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Filter.Forms
{
    public partial class FormFilter : Form
    {
        public FormFilter()
        {
            InitializeComponent();
        }

        private void FormFilter_Load(object sender, EventArgs e)
        {
            treeSubFilter.AllowDrop = true;
            treeSubFilter.ItemDrag += new ItemDragEventHandler(treeView_ItemDrag);
            treeSubFilter.DragEnter += new DragEventHandler(treeView_DragEnter);
            treeSubFilter.DragOver += new DragEventHandler(treeView_DragOver);
            treeSubFilter.DragDrop += new DragEventHandler(treeView_DragDrop);
        }

        private FilterData filterData;
        public FilterData FilterData
        {
            get
            {
                return filterData;
            }
            set
            {
                filterData = value;
                if (filterData != null)
                {
                    treeSubFilter.Nodes.Clear();
                    AddNode(filterData, treeSubFilter.Nodes);
                }
            }
        }

        private void AddNode(FilterData fdata, TreeNodeCollection nodes)
        {
            var node = new TreeNode(fdata.FilterTable.ToString()) { Tag = fdata };
            nodes.Add(node);
            fdata.Chields.ForEach(fd => AddNode(fd, node.Nodes));
        }

        public void InitializeFilter(TableData table)
        {
            FilterData = FilterData.CreateBy(table);
        }
        
        private void treeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            // Move the dragged node when the left mouse button is used.
            if (e.Button == MouseButtons.Left)
            {
                DoDragDrop(e.Item, DragDropEffects.Move);
            }
        }

        // Set the target drop effect to the effect 
        // specified in the ItemDrag event handler.
        private void treeView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }

        // Select the node under the mouse pointer to indicate the 
        // expected drop location.
        private void treeView_DragOver(object sender, DragEventArgs e)
        {
            var treeView = (sender as TreeView);

            // Retrieve the client coordinates of the mouse position.
            Point targetPoint = treeView.PointToClient(new Point(e.X, e.Y));

            // Select the node at the mouse position.
            treeView.SelectedNode = treeView.GetNodeAt(targetPoint);
        }

        private void treeView_DragDrop(object sender, DragEventArgs e)
        {
            var treeView = (sender as TreeView);

            // Retrieve the client coordinates of the drop location.
            Point targetPoint = treeView.PointToClient(new Point(e.X, e.Y));

            // Retrieve the node at the drop location.
            TreeNode targetNode = treeView.GetNodeAt(targetPoint);

            // Retrieve the node that was dragged.
            TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));

            // Confirm that the node at the drop location is not 
            // the dragged node or a descendant of the dragged node.
            if (!draggedNode.Equals(targetNode) && !ContainsNode(draggedNode, targetNode))
            {
                // If it is a move operation, remove the node from its current 
                // location and add it to the node at the drop location.
                if (e.Effect == DragDropEffects.Move)
                {
                    draggedNode.Remove();
                    targetNode.Nodes.Add(draggedNode);

                    var draggedData = draggedNode.Tag as FilterData;
                    var targetData = targetNode.Tag as FilterData;
                    draggedData.MoveTo(targetData);
                }

                // Expand the node at the location 
                // to show the dropped node.
                targetNode.Expand();
            }
        }

        // Determine whether one node is a parent 
        // or ancestor of a second node.
        private bool ContainsNode(TreeNode node1, TreeNode node2)
        {
            // Check the parent node of the second node.
            if (node2.Parent == null) return false;
            if (node2.Parent.Equals(node1)) return true;

            // If the parent node is not null or equal to the first node, 
            // call the ContainsNode method recursively using the parent of 
            // the second node.
            return ContainsNode(node1, node2.Parent);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            var fd = FilterData;
        }

        private void treeSubFilter_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var table = (e.Node.Tag as FilterData).FilterTable.Table;
                contextMenuTreeView.Tag = e.Node;
                contextMenuTreeView.Show(sender as Control, e.Location);

                addLinkedTableToolStripMenuItem.DropDownItems.Clear();
                if (table.LinkedTables.Count > 0)
                {
                    table.LinkedTables.ForEach(lt =>
                    {
                        var item = addLinkedTableToolStripMenuItem.DropDownItems.Add(lt.Table?.DisplayName);
                        item.Tag = lt;
                        item.Click += addLinkedTableToolStripMenuItems_Click;
                    });
                }
                else
                {
                    var item = addLinkedTableToolStripMenuItem.DropDownItems.Add("Пусто");
                    item.Enabled = false;
                }
            }
        }

        private void addLinkedTableToolStripMenuItems_Click(object sender, EventArgs e)
        {
            var selectedNode = contextMenuTreeView.Tag as TreeNode;
            var fdata = selectedNode.Tag as FilterData;
            var linkedTable = (sender as ToolStripItem).Tag as LinkedTable;

            var newFilterData = FilterData.CreateBy(linkedTable.Table, fdata);
            AddNode(newFilterData, selectedNode.Nodes);
            selectedNode.Expand();
        }
    }
}
