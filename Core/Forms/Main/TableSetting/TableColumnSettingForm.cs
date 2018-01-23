﻿using Core.Data.Field;
using Core.Data.Table;
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
                    lstColumns.Items.AddRange(tableData.Fields
                        .Where(f => f.Visible && !fieldsSelected.Contains(f))
                        .OrderBy(f => f.DisplayName)
                        .Select(f => new ListBoxFieldItem() { Field = f })
                        .ToArray());

                    // Сортируем по Order-у
                    lvSelectedColumns.Items.AddRange(TableStorageInformation.Columns
                        .OrderBy(col => col.Order)
                        .Select(col => new ListViewColumnFieldItem(col))
                        .ToArray());
                }
            }
        }
    }
}
