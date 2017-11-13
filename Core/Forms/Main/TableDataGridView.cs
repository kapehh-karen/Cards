﻿using Core.Connection;
using Core.Data.Base;
using Core.Data.Field;
using Core.Data.Model;
using Core.Data.Table;
using Core.Helper;
using Core.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Forms.Main
{
    public class TableDataGridView : DataGridView
    {
        public event KeyEventHandler PressedEnter;
        public event KeyEventHandler PressedKey;

        private TableData tableData;
        private DataGridViewColumn lastSelectedColumn;
        private object needSelectID;
        private bool firstAfterBind;

        public TableDataGridView()
        {
            DoubleBuffered = true;
            BackgroundColor = System.Drawing.Color.White;
            MultiSelect = false;
        }

        public List<FieldData> ColumnFields { get; } = new List<FieldData>();

        public FieldData FieldID { get; private set; }

        public TableData Table
        {
            get => tableData;
            set
            {
                tableData = value;

                if (tableData != null)
                {
                    ColumnFields.Clear();
                    ColumnFields.AddRange(tableData.Fields);
                    // TODO: Добавлять поле идентификатора автоматически, если оно не добавлено
                    FieldID = tableData.IdentifierField;
                }
            }
        }

        public DataBase Base { get; set; }

        public DataTable CurrentDataTable { get; set; }

        public DataView CurrentDataView { get; set; } = new DataView();

        public object SelectedID
        {
            get => CurrentRow == null ? null : Rows[CurrentRow.Index].Cells[FieldID.Name].Value;
            set => needSelectID = value;
        }

        public CardModel SelectedModel
        {
            get
            {
                if (CurrentRow == null)
                    return null;

                var model = CardModel.CreateFromTable(Table);
                
                (from DataGridViewCell col in CurrentRow.Cells select col)
                    .ForEach(cell =>
                    {
                        var field = cell.OwningColumn.Tag as FieldData;
                        // Только простые типы присваиваем, за остальными пускай делают запрос к БД
                        if (field.Type != FieldType.BIND)
                            model[field] = cell.Value;
                    });

                model.ResetStates();
                return model;
            }
        }

        private TableStorageInformation TableClassificatorInformation { get; set; }

        public bool AllowCache { get; set; } = true;

        public void FillTable(bool forceUpdate = false)
        {
            if (Base == null || Table == null)
                return;

            var needUpdate = true;

            if (Table.IsClassifier && CurrentDataTable == null && AllowCache)
            {
                TableClassificatorInformation = TableStorage.Get(Table);
                CurrentDataTable = TableClassificatorInformation?.Data;
                needUpdate = CurrentDataTable == null;
            }

            if (forceUpdate || needUpdate)
            {
                // Make SQL request
                using (var dbc = WaitDialog.Run("Подождите, идет подключение к SQL Server", () => new SQLServerConnection(Base)))
                {
                    // main part query
                    var columns = string.Join(", ", ColumnFields.Where(f => f.Visible || f.IsIdentifier)
                        .Select(f => f.Type != FieldType.BIND ? $"[{Table.Name}].[{f.Name}]" : $"[{f.BindData.Table.Name}].[{f.BindData.Field.Name}] AS [{f.Name}]")
                        .ToArray());

                    // joins part query
                    var joins = string.Join("\r\n", ColumnFields.Where(f => f.Visible || f.IsIdentifier).Where(f => f.Type == FieldType.BIND)
                        .Select(f => $"LEFT JOIN [{f.BindData.Table.Name}] ON [{f.BindData.Table.Name}].[{f.BindData.Table.IdentifierField.Name}] = [{Table.Name}].[{f.Name}]")
                        .ToArray());

                    var query = $"SELECT {columns} FROM {Table.Name}\r\n{joins}";
                    var connection = dbc.Connection;
                    var adapter = new SqlDataAdapter(query, connection);

                    if (CurrentDataTable == null)
                    {
                        var data = new DataSet();
                        WaitDialog.Run("Ожидается ответ от сервера...", () => adapter.Fill(data));
                        CurrentDataTable = data.Tables[0];
                        firstAfterBind = true; // Перед биндингом
                        CurrentDataView.Table = CurrentDataTable;
                        this.DataSource = CurrentDataView;
                    }
                    else
                    {
                        firstAfterBind = false; // Чтобы не обнулять needSelectID
                        CurrentDataTable.Clear();
                        WaitDialog.Run("Ожидается ответ от сервера...", () => adapter.Fill(CurrentDataTable));
                        firstAfterBind = true; // Делаем только перед применением изменений
                        CurrentDataTable.AcceptChanges();
                    }

                    adapter.Dispose();
                }

                if (Table.IsClassifier && CurrentDataTable != null)
                {
                    TableClassificatorInformation = TableStorage.Set(Table, CurrentDataTable);
                }
            }
            else
            {
                firstAfterBind = true; // Перед биндингом
                CurrentDataView.Table = CurrentDataTable;
                this.DataSource = CurrentDataView;
            }
            
            // Renaming columns header
            foreach (DataGridViewColumn column in this.Columns)
            {
                var fieldData = ColumnFields.Single(f => f.Name.Equals(column.Name));
                column.HeaderText = fieldData.DisplayName;
                column.Tag = fieldData;
                column.Visible = fieldData.Visible;
                column.SortMode = DataGridViewColumnSortMode.Automatic;
            }
        }

        private void TrySelectCell(DataGridViewRow row)
        {
            if (row == null)
                return;

            if (lastSelectedColumn != null)
            {
                CurrentCell = (from DataGridViewCell cell in row.Cells select cell).FirstOrDefault(cell => cell.OwningColumn.Equals(lastSelectedColumn));
            }
            else
            {
                CurrentCell = (from DataGridViewCell col in row.Cells select col).FirstOrDefault(col => col.Visible);
            }
        }

        private void TrySelectRow()
        {
            if (firstAfterBind && needSelectID != null) // Выделение нужной строки при первом открытии
            {
                if (CurrentRow != null)
                    CurrentRow.Selected = false;

                var findedRow = WaitDialog.Run("Подождите...", () =>
                    (from DataGridViewRow row in Rows select row).FirstOrDefault(row => row.Cells[FieldID.Name].Value.Equals(needSelectID)));

                if (findedRow != null)
                {
                    findedRow.Selected = true;
                    TrySelectCell(findedRow);
                }

                firstAfterBind = false;
                needSelectID = null;
            }
            else // Выделение нужного столбца, если значения ранее уже были в таблице
            {
                TrySelectCell(CurrentRow);
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if ((e.KeyData & Keys.KeyCode) == Keys.Enter)
            {
                if (CurrentRow != null)
                {
                    PressedEnter?.Invoke(this, e);
                }
            }
            else
                base.OnKeyDown(e);

            PressedKey?.Invoke(this, e);
        }

        protected override void OnDataBindingComplete(DataGridViewBindingCompleteEventArgs e)
        {
            base.OnDataBindingComplete(e);
            TrySelectRow();
        }
        
        protected override void OnCellClick(DataGridViewCellEventArgs e)
        {
            base.OnCellClick(e);

            if (e.ColumnIndex >= 0)
                lastSelectedColumn = Columns[e.ColumnIndex];
        }
    }
}
