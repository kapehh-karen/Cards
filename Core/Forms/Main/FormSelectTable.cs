using Core.Config;
using Core.Connection;
using Core.Data.Base;
using Core.Data.Table;
using Core.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Forms.Main
{
    public partial class FormSelectTable : Form
    {
        private List<TableData> listOfTables = new List<TableData>();

        public FormSelectTable()
        {
            InitializeComponent();
        }

        public string FileName { get; set; }

        public DataBase Base { get; set; }

        public TableData SelectedTableData { get; set; }

        private void FillListView(IEnumerable<TableData> tables)
        {
            listViewTables.BeginUpdate();
            listViewTables.Items.Clear();
            tables.ForEach(t =>
            {
                var item = new ListViewItem(t.DisplayName);
                item.SubItems.Add(t.IsClassifier ? "Классификатор" : "Таблица");
                item.SubItems.Add(t.Name);
                item.ImageIndex = t.IsClassifier ? 1 : 0;
                item.Tag = t;
                listViewTables.Items.Add(item);
            });
            listViewTables.EndUpdate();
        }

        private void FormSelectTable_Load(object sender, EventArgs e)
        {
            this.Text = $".:: CARDS ::. - {FileName}";

            listOfTables = Base?.Tables
                .Where(t => t.Visible)
                .OrderBy(t => t.IsClassifier)
                .ThenBy(t => t.DisplayName).ToList();
            FillListView(listOfTables);
        }

        private void listViewTables_ItemActivate(object sender, EventArgs e)
        {
            if (listViewTables.SelectedItems.Count > 0 &&
                listViewTables.SelectedItems[0].Tag is TableData table)
            {
                SelectedTableData = table;
                DialogResult = DialogResult.OK;
            }
        }

        private void txtSearchTable_TextChanged(object sender, EventArgs e)
        {
            FillListView(listOfTables.Where(t => t.DisplayName.IndexOf(txtSearchTable.Text, StringComparison.OrdinalIgnoreCase) >= 0));
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }
    }
}
