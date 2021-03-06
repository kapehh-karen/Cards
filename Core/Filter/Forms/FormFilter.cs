﻿using Core.Data.Table;
using Core.Filter.Data;
using Core.Notification;
using Core.Storage.Filter;
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
        
        public TableData CurrentTable { get; set; }

        private FilterData filterData;
        public FilterData Filter
        {
            get
            {
                return filterData;
            }
            set
            {
                if (value == null)
                    return;

                filterData = value;
                treeSubFilter.Nodes.Clear();
                treeSubFilter.SelectedNode = AddNode(filterData, treeSubFilter.Nodes);
                treeSubFilter.ExpandAll();
            }
        }

        private TreeNode AddNode(FilterData fdata, TreeNodeCollection nodes)
        {
            var node = new TreeNode(fdata.FilterTable.ToString()) { Tag = fdata };
            nodes.Add(node);
            fdata.Chields.ForEach(fd => AddNode(fd, node.Nodes));
            return node;
        }
        
        private void treeSubFilter_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var selectedNode = e.Node;
                var fdata = (selectedNode.Tag as FilterData);
                var table = fdata.FilterTable.Table;

                contextMenuTreeView.Tag = selectedNode;
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

                removeNodeToolStripMenuItem.Enabled = !fdata.IsRoot;
            }
        }

        private void addLinkedTableToolStripMenuItems_Click(object sender, EventArgs e)
        {
            var selectedNode = contextMenuTreeView.Tag as TreeNode;
            var fdata = selectedNode.Tag as FilterData;
            var linkedTable = (sender as ToolStripItem).Tag as LinkedTable;

            var newFilterData = FilterData.CreateSubquery(linkedTable.Table, fdata);
            AddNode(newFilterData, selectedNode.Nodes);
            selectedNode.Expand();
        }

        private void removeNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedNode = contextMenuTreeView.Tag as TreeNode;
            var fdata = selectedNode.Tag as FilterData;

            if (fdata.IsRoot)
            {
                NotificationMessage.Error("Нельзя удалить корневой элемент");
            }
            else
            {
                fdata.Remove();
                selectedNode.Remove();
            }
        }

        private void SaveCurrentChanges()
        {
            var currentFilterData = containerConditionControl1.FilterData;
            if (currentFilterData != null)
            {
                currentFilterData.Where = containerConditionControl1.BuildCondition();
            }
        }

        private void AcceptFilterData(FilterData nextFilterData)
        {
            containerConditionControl1.FilterData = nextFilterData;
            containerConditionControl1.LoadCondition(nextFilterData.Where);

            lblCondition.Text = $"Условие отбора для таблицы: {nextFilterData.FilterTable.Table.DisplayName.ToUpper()}";
        }

        private void treeSubFilter_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Сохраняем текущие изменения перед изменением ноды
            SaveCurrentChanges();

            var nextFilterData = e.Node?.Tag as FilterData;
            AcceptFilterData(nextFilterData);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Filter = FilterData.CreateRoot(CurrentTable);
            AcceptFilterData(Filter);
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            // Сохраняем текущие изменения перед применением фильтра
            SaveCurrentChanges();

            DialogResult = DialogResult.OK;
        }

        private void btnViewSQL_Click(object sender, EventArgs e)
        {
            // Сохраняем текущие изменения перед просмотром SQL
            SaveCurrentChanges();

            using (var dialog = new FormSQLView())
            {
                dialog.SQL = Filter.SQLExpression;
                dialog.ShowDialog();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Сохраняем текущие изменения перед сохранением в файл
            SaveCurrentChanges();

            FilterStorage.Instance.Save(Filter);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            var loadedFilterData = FilterStorage.Instance.Load();

            if (loadedFilterData == null)
                return;

            if (CurrentTable != null && loadedFilterData.FilterTable.Table != CurrentTable)
            {
                NotificationMessage.Error($"Выбранный фильтр создан для другой таблицы. Его нельзя применить к таблице \"{CurrentTable.DisplayName}\"");
                return;
            }

            Filter = loadedFilterData;
        }

        #region Перемещение вложенности выборок

        private void FormFilter_Load(object sender, EventArgs e)
        {
            /*treeSubFilter.AllowDrop = true;
            treeSubFilter.ItemDrag += new ItemDragEventHandler(treeView_ItemDrag);
            treeSubFilter.DragEnter += new DragEventHandler(treeView_DragEnter);
            treeSubFilter.DragOver += new DragEventHandler(treeView_DragOver);
            treeSubFilter.DragDrop += new DragEventHandler(treeView_DragDrop);*/
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

        #endregion
    }
}
