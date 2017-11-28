using Core.Connection;
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
        private object needSelectID;
        private bool firstAfterBind;

        public TableDataGridView()
        {
            DoubleBuffered = true;
            BackgroundColor = System.Drawing.Color.White;
            MultiSelect = false;
            BorderStyle = BorderStyle.Fixed3D;
        }
        
        public FieldData ParentField { get; set; }

        public FieldData FieldID { get; private set; }

        public TableData Table
        {
            get => tableData;
            set
            {
                tableData = value;

                if (tableData != null)
                {
                    FieldID = tableData.IdentifierField;
                    
                    // Получаем настройки таблицы
                    TableStorageInformation = TableStorage.Get(tableData);
                    TableStorageInformation.Reset();
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
                        var field = cell.OwningColumn.GetTag().Field;
                        if (field.Type != FieldType.BIND)
                        {
                            model[field] = cell.Value;
                        }
                    });

                model.ResetStates();
                return model;
            }
        }

        public DataGridViewColumn KeepSelectedColumn { get; set; }

        private TableStorageInformation TableStorageInformation { get; set; }

        public bool AllowCache { get; set; } = true;

        /// <summary>
        /// Распределение полей по дефолту. Если в настройках не указано какие поля отображать
        /// </summary>
        private void InitializeFields()
        {
            if (TableStorageInformation.HasFields)
                return;

            // Обязательно добавляем ID, т.к. он может быть скрыт, но нам он нужен
            TableStorageInformation.Fields.Add(FieldID);

            if (ParentField?.Type == FieldType.BIND)
            {
                var displayField = ParentField.BindData?.Field; // Отображаемое поле

                // Если отображаемое поле и есть ID, то надо показать что-то ещё
                // А что именно - хз, по этому идем по альтернативному пути
                if (displayField != null && displayField != FieldID)
                {
                    // Только отображаемое поле
                    TableStorageInformation.Fields.Add(ParentField.BindData.Field);
                    return;
                }
            }

            // Все видимые поля (кроме ID, его добавили уже)
            Table.Fields.Where(f => f.Visible && !f.IsIdentifier).Take(5).ForEach(TableStorageInformation.Fields.Add);
        }

        public void FillTable(bool forceUpdate = false)
        {
            if (Base == null || Table == null)
                return;

            InitializeFields();

            var needUpdate = true;

            // При загрузке данных в компонент используется флаг AllowCache
            if (Table.IsClassifier && AllowCache && CurrentDataTable == null)
            {
                CurrentDataTable = TableStorageInformation.Data;
                CurrentDataView = TableStorageInformation.View ?? CurrentDataView;
                needUpdate = TableStorageInformation.IsEmpty;
            }

            if (forceUpdate || needUpdate)
            {
                // Make SQL request
                using (var dbc = WaitDialog.Run("Подождите, идет подключение к SQL Server", () => new SQLServerConnection(Base)))
                {
                    // main part query
                    var columns = string.Join(", ", TableStorageInformation.Fields
                        .Select(f => f.Type != FieldType.BIND ? $"[{Table.Name}].[{f.Name}]" : $"[{f.Name}__{f.BindData.Table.Name}].[{f.BindData.Field.Name}] AS [{f.Name}]")
                        .ToArray());

                    // joins part query
                    var joins = string.Join("\r\n", TableStorageInformation.Fields.Where(f => f.Type == FieldType.BIND)
                        .Select(f => $"LEFT JOIN [{f.BindData.Table.Name}] AS [{f.Name}__{f.BindData.Table.Name}] ON [{f.Name}__{f.BindData.Table.Name}].[{f.BindData.Table.IdentifierField.Name}] = [{Table.Name}].[{f.Name}]")
                        .ToArray());

                    var query = $"SELECT {columns} FROM [{Table.Name}]\r\n{joins}";
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

                // Не важно значение флага AllowCache, всегда сохраняем данные классификатора в память
                if (Table.IsClassifier && CurrentDataTable != null)
                {
                    TableStorageInformation.Data = CurrentDataTable;
                    TableStorageInformation.View = CurrentDataView;
                }
            }
            else
            {
                firstAfterBind = true; // Перед биндингом
                this.DataSource = CurrentDataView;
            }
            
            // Renaming columns header
            foreach (DataGridViewColumn column in this.Columns)
            {
                var fieldData = TableStorageInformation.Fields.Single(f => f.Name.Equals(column.Name));
                var tag = new TableColumnTag() { Field = fieldData };
                column.HeaderText = fieldData.DisplayName;
                column.Tag = tag;
                column.Visible = fieldData.Visible;
                column.SortMode = DataGridViewColumnSortMode.Automatic;
            }
        }

        private void TrySelectCell(DataGridViewRow row)
        {
            if (row == null)
                return;

            if (KeepSelectedColumn != null)
            {
                CurrentCell = (from DataGridViewCell cell in row.Cells select cell).FirstOrDefault(cell => cell.OwningColumn.Equals(KeepSelectedColumn));
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
                var findedRow = WaitDialog.Run("Подождите...", () =>
                    (from DataGridViewRow row in Rows select row).FirstOrDefault(row => row.Cells[FieldID.Name].Value.Equals(needSelectID)));

                if (CurrentRow != findedRow)
                {
                    if (CurrentRow != null)
                        CurrentRow.Selected = false;

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
    }
}
