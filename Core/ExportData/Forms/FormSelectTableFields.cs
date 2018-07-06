using Core.Common.TreeViewEx;
using Core.Data.Field;
using Core.Data.Table;
using Core.ExportData.Data.Token;
using Core.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.ExportData.Forms
{
    public partial class FormSelectTableFields : Form
    {
        public FormSelectTableFields()
        {
            InitializeComponent();
        }

        private TableData _table;
        public TableData Table
        {
            get => _table;
            set
            {
                _table = value;
                if (_table != null)
                {
                    var rootNode = AddTableToTree(_table);
                    treeViewFields.Nodes.Add(rootNode);
                    RootTableToken = rootNode.Tag as TableToken;
                }
            }
        }

        public TableToken RootTableToken { get; private set; }

        private TreeNode AddTableToTree(TableData table, string customTitle = null)
        {
            var fieldId = table.IdentifierField;
            var node = new HiddenCheckBoxTreeNode(customTitle ?? table.DisplayName);
            node.Tag = new TableToken(table)
            {
                Table = table,
                FieldIdToken = new FieldToken(fieldId)
            };
            table.Fields.OrderBy(it => it.DisplayName).Where(it => it.Visible).ForEach(it =>
            {
                if (it.Type != FieldType.BIND)
                {
                    var newItem = new TreeNode(it.DisplayName);
                    newItem.Tag = new FieldToken(it);
                    node.Nodes.Add(newItem);
                }
                else
                {
                    var newNode = AddTableToTree(it.BindData.Table, it.DisplayName);
                    var newTableToken = newNode.Tag as TableToken;
                    newTableToken.JoinFieldParent = it;
                    newTableToken.JoinFieldCurrent = it.BindData.Table.IdentifierField;
                    node.Nodes.Add(newNode);
                }
            });
            table.LinkedTables.OrderBy(it => it.Table.DisplayName).ForEach(it =>
            {
                var newNode = AddTableToTree(it.Table);
                var newTableToken = newNode.Tag as TableToken;
                newTableToken.JoinFieldParent = fieldId;
                newTableToken.JoinFieldCurrent = it.Field;
                node.Nodes.Add(newNode);
            });
            return node;
        }

        private bool ContainsCheckedNodes(TreeNode node)
        {
            foreach (TreeNode chield in node.Nodes)
                if (chield.Checked)
                    return true;
                else if (ContainsCheckedNodes(chield))
                    return true;

            return false;
        }

        private void FillNodeTokens(TreeNode node)
        {
            if (node.Tag is TableToken tableToken) // Если таблица
            {
                var nodes = node.Nodes.Cast<TreeNode>().Where(it => it.Checked || ContainsCheckedNodes(it));

                tableToken.Tables.Clear();
                tableToken.Fields.Clear();

                foreach (var currNode in nodes)
                {
                    if (currNode.Tag is TableToken tableTokenChield) // Если таблица
                    {
                        tableToken.Tables.Add(tableTokenChield);
                        FillNodeTokens(currNode);
                    }
                    else if (currNode.Tag is FieldToken fieldToken) // Если поле
                    {
                        tableToken.Fields.Add(fieldToken);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            treeViewFields.Nodes.Cast<TreeNode>().ForEach(it => FillNodeTokens(it));
            textBox1.Text = RootTableToken.BuildSqlExpression();
        }

        private void FormSelectTableFields_Load(object sender, EventArgs e)
        {
            TableToken.ResetIndex();
            FieldToken.ResetIndex();
        }
    }
}
