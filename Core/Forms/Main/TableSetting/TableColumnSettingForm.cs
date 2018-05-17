using Core.Common;
using Core.Data.Field;
using Core.Data.Table;
using Core.Helper;
using Core.Storage.Tables;
using Core.Storage.Tables.TableStorageData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Forms.Main.TableSetting
{
    public partial class TableColumnSettingForm : Form
    {
        private class ListBoxFieldItem
        {
            public FieldData Field { get; set; }

            public override string ToString() => Field.DisplayName;
        }

        private class ListViewColumnFieldItem : ListViewItem
        {
            public ListViewColumnFieldItem(TableStorageColumnData columnField) : base()
            {
                Text = columnField.Field.IsIdentifier ? "*" : "";
                SubItems.Add(columnField.Field.DisplayName);
                SubItems.Add(columnField.Field.Visible ? "Видим" : "Скрыт");

                ForeColor = columnField.Field.IsIdentifier ? Color.Blue : columnField.Field.Visible ? Color.Black : Color.Green;

                ColumnField = columnField;
            }

            public TableStorageColumnData ColumnField { get; private set; }

            public override string ToString() => ColumnField.Field.DisplayName;
        }

        public TableColumnSettingForm()
        {
            InitializeComponent();
        }

        private void TableColumnSettingForm_Load(object sender, EventArgs e)
        {
            
        }

        private List<ListViewColumnFieldItem> NotSelectedItems { get; set; }

        public TableStorageType TableStorageType { get; set; }

        private TableStorageInformation TableStorageInformation { get; set; }

        private TableData tableData;
        public TableData Table
        {
            get => tableData;
            set
            {
                tableData = value;

                if (tableData != null)
                {
                    // Получаем настройки таблицы
                    TableStorageInformation = TableStorage.Instance.Get(tableData, TableStorageType);

                    var fieldsSelected = TableStorageInformation.Columns.Select(col => col.Field);

                    // Сортируем по отображаемому имени столбцы которые ещё не выбраны
                    lvColumns.ListViewItemSorter = new ListViewItemComparer(1);
                    lvColumns.Sorting = SortOrder.Ascending;
                    NotSelectedItems = tableData.Fields
                        .Where(f => !fieldsSelected.Contains(f))
                        .Select(f => new ListViewColumnFieldItem(new TableStorageColumnData() { Field = f }))
                        .ToList();
                    NotSelectedItems.ForEach(it => lvColumns.Items.Add(it));

                    // Сортируем по Order-у
                    lvSelectedColumns.Items.AddRange(TableStorageInformation.Columns
                        .OrderBy(col => col.Order)
                        .Select(col => new ListViewColumnFieldItem(col))
                        .ToArray());

                    Text = $"Настройки столбцов для таблицы \"{tableData.DisplayName}\"";
                }
            }
        }

        public List<TableStorageColumnData> Columns { get; private set; }

        private void btnUpColumn_Click(object sender, EventArgs e)
        {
            lvSelectedColumns.Focus();
            
            if (lvSelectedColumns.SelectedItems.Count > 0 &&
                lvSelectedColumns.SelectedItems[0] is ListViewColumnFieldItem item &&
                item.Index > 0)
            {
                var index = item.Index - 1;
                lvSelectedColumns.Items.RemoveAt(item.Index);
                lvSelectedColumns.Items.Insert(index, item);
            }
        }

        private void btnDownColumn_Click(object sender, EventArgs e)
        {
            lvSelectedColumns.Focus();
            
            if (lvSelectedColumns.SelectedItems.Count > 0 &&
                lvSelectedColumns.SelectedItems[0] is ListViewColumnFieldItem item &&
                (item.Index + 1) < lvSelectedColumns.Items.Count)
            {
                var index = item.Index + 1;
                lvSelectedColumns.Items.RemoveAt(item.Index);
                lvSelectedColumns.Items.Insert(index, item);
            }
        }

        private void btnAddColumn_Click(object sender, EventArgs e)
        {
            if (lvColumns.SelectedItems.Count > 0)
            {
                lvColumns.SelectedItems
                    .Cast<ListViewColumnFieldItem>()
                    .ToArray()
                    .ForEach(it =>
                    {
                        it.Selected = false;
                        lvColumns.Items.Remove(it);
                        NotSelectedItems.Remove(it);
                        lvSelectedColumns.Items.Add(it);
                    });
            }
        }

        private void btnRemoveColumn_Click(object sender, EventArgs e)
        {
            if (lvSelectedColumns.SelectedItems.Count > 0)
            {
                var selectedItems = lvSelectedColumns.SelectedItems.Cast<ListViewColumnFieldItem>().ToArray();

                if (selectedItems.SingleOrDefault(col => col.ColumnField.Field.IsIdentifier) is ListViewColumnFieldItem itemID
                    && itemID != null)
                {
                    MessageBox.Show("Нельзя убрать поле идентификатора!", Consts.ProgramTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                selectedItems.ForEach(it =>
                {
                    it.Selected = false;
                    lvSelectedColumns.Items.Remove(it);
                    lvColumns.Items.Add(it);
                    NotSelectedItems.Add(it);
                });
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Columns = lvSelectedColumns.Items
                .Cast<ListViewColumnFieldItem>()
                .Select((item, index) =>
                {
                    item.ColumnField.Order = index;
                    return item.ColumnField;
                })
                .ToList();

            TableStorageInformation.Columns = Columns;
            TableStorageInformation.Repair();

            DialogResult = DialogResult.OK;
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

        private void txtSearchField_TextChanged(object sender, EventArgs e)
        {
            lvColumns.BeginUpdate();
            lvColumns.Items.Clear();
            NotSelectedItems
                .Where(it => it.ColumnField.Field.DisplayName.IndexOf(txtSearchField.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                .ForEach(it =>
                {
                    it.Selected = false;
                    lvColumns.Items.Add(it);
                });
            lvColumns.EndUpdate();
        }
    }
}
