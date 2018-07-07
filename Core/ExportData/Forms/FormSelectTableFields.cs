using Core.Common.TreeViewEx;
using Core.Connection;
using Core.Data.Field;
using Core.Data.Table;
using Core.ExportData.Data.Token;
using Core.Forms.Main;
using Core.Helper;
using Core.Storage.Documents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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

        public FormTableView FormTable { get; set; }

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
                    RootTableToken.IsRootable = true;
                }
            }
        }

        public TableToken RootTableToken { get; private set; }

        private TreeNode AddTableToTree(TableData table, string customTitle = null)
        {
            var fieldId = table.IdentifierField;
            var node = new HiddenCheckBoxTreeNode(customTitle ?? $"ТАБЛИЦА: {table.DisplayName}");
            var tableToken = new TableToken(table) { Table = table };
            FieldToken fieldIdToken = null;

            node.Tag = tableToken;

            table.Fields.OrderBy(it => it.DisplayName).Where(it => it.Visible).ForEach(it =>
            {
                if (it.Type != FieldType.BIND)
                {
                    var fieldToken = new FieldToken(it);
                    // Если у нас ID в видимом списке, то мы не будем отдельно создавать для него FieldToken, заюзаем тот же
                    if (fieldId == it)
                        fieldIdToken = fieldToken;
                    node.Nodes.Add(new TreeNode(it.DisplayName) { Tag = fieldToken });
                }
                else
                {
                    var newNode = AddTableToTree(it.BindData.Table, it.DisplayName);
                    var newTableToken = newNode.Tag as TableToken;
                    newTableToken.IsClassificator = true;
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

            tableToken.FieldIdToken = fieldIdToken ?? new FieldToken(fieldId);
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

            var sql = RootTableToken.BuildSqlExpression();
            textBox1.Text = sql;

            var fileName = DocStorage.Instance.GenerateFileName("Экспорт данных", "xlsx");
            WaitDialog.Run("Экспортируются данные...", dialog => ExcelHelper.SaveExtendedTableToExcel(dialog, fileName, RootTableToken, FormTable.GetSelectedIDs()));
            DocStorage.Instance.OpenDocumentFile(fileName);
        }

        private void FormSelectTableFields_Load(object sender, EventArgs e)
        {
            TableToken.ResetIndex();
            FieldToken.ResetIndex();
        }
    }
}
