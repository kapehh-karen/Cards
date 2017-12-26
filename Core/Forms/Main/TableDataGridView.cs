using Core.Common;
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
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Forms.Main
{
    public class TableDataGridView : BaseDataGridView
    {
        private TableData tableData;

        public TableDataGridView()
        {
            DoubleBuffered = true;
            BackgroundColor = Color.White;
            MultiSelect = false;
            BorderStyle = BorderStyle.Fixed3D;

            // STYLE

            // Set the selection background color for all the cells.
            this.DefaultCellStyle.SelectionBackColor = Color.DarkGray;
            this.DefaultCellStyle.SelectionForeColor = Color.White;
            
            // Set the background color for all rows and for alternating rows. 
            // The value for alternating rows overrides the value for all rows. 
            this.RowsDefaultCellStyle.BackColor = Color.White;
            this.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
        }

        /// <summary>
        /// Используется в режиме классификатора. Содержит ссылку на поле для которого используется классификатор.
        /// </summary>
        public FieldData ParentField { get; set; } = null;

        /// <summary>
        /// Указывает, открыта таблица в классификаторе или нет
        /// </summary>
        public bool IsInClassificator => ParentField == null;

        /// <summary>
        /// Поле идентификатора
        /// </summary>
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
                    TableStorageInformation = TableStorage.Instance.Get(tableData);
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
        
        public bool AllowCache { get; set; } = true;

        public bool IsInitialization { get; private set; } = true;

        /// <summary>
        /// Распределение полей по дефолту. Если в настройках не указано какие поля отображать
        /// </summary>
        /// <returns>false - если инициализации небыло, true - если была</returns>
        private bool InitializeFields()
        {
            if (TableStorageInformation.HasColumns)
                return false;

            // Обязательно добавляем ID, т.к. он может быть скрыт, но нам он нужен
            TableStorageInformation.AddColumn(FieldID);

            if (ParentField?.Type == FieldType.BIND)
            {
                var displayField = ParentField.BindData?.Field; // Отображаемое поле

                // Добавляем в список отображаемых полей только тогда, когда displayField не ID
                // NOTE: Если отображаемое поле = ID, то надо показать что-то ещё, а что именно - хз
                if (displayField != null && displayField != FieldID)
                {
                    // Только отображаемое поле
                    TableStorageInformation.AddColumn(displayField);
                    return true;
                }
            }

            // Все видимые поля (кроме ID, его добавили уже)
            Table.Fields.Where(f => f.Visible && !f.IsIdentifier).Take(5).ForEach(TableStorageInformation.AddColumn);
            return true;
        }

        public void FillTable(bool forceUpdate = false)
        {
            if (Base == null || Table == null)
                return;

            InitializeFields();

            var needUpdate = true;
            var fields = TableStorageInformation.Columns.Select(item => item.Field).ToArray();

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
                    var columns = string.Join(", ", fields
                        .Select(f => f.Type != FieldType.BIND ? $"[{Table.Name}].[{f.Name}]" : $"[{f.Name}__{f.BindData.Table.Name}].[{f.BindData.Field.Name}] AS [{f.Name}]")
                        .ToArray());

                    // joins part query
                    var joins = string.Join("\r\n", fields.Where(f => f.Type == FieldType.BIND)
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
        }

        #region Post-processing for data bindings

        private object needSelectID;
        private bool firstAfterBind;

        public DataGridViewColumn KeepSelectedColumn { get; set; }

        private void BindingColumns()
        {
            var fields = TableStorageInformation.Columns.Select(item => item.Field);

            // Renaming columns header
            foreach (DataGridViewColumn column in this.Columns)
            {
                var fieldData = fields.Single(f => f.Name.Equals(column.Name));
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

        protected override void OnDataBindingComplete(DataGridViewBindingCompleteEventArgs e)
        {
            base.OnDataBindingComplete(e);

            // Привязываем к колонкам тег и переименовываем их
            BindingColumns();

            if (TableStorageInformation.IsNew)
            {
                TableStorageInformationSave();
            }
            else
            {
                // Применить все настройки ширины столбцов и т.п.
                TableStorageInformationApply();
            }

            // Выделить строку
            TrySelectRow();

            IsInitialization = false;
        }

        #endregion

        #region TableStorageInformation

        private TableStorageInformation TableStorageInformation { get; set; }

        private void TableStorageInformationSave()
        {
            // Сохраняем ширину всех колонок
            TableStorageInformation.Columns.ForEach(col =>
            {
                foreach (DataGridViewColumn column in Columns)
                    if (column.GetTag().Field.Equals(col.Field))
                    {
                        col.Width = column.Width;
                        col.Order = column.DisplayIndex;
                        break;
                    }
            });
            TableStorage.Instance.Save(TableStorageInformation);
        }

        private void TableStorageInformationApply()
        {
            // Восстанавливаем ширину всех колонок
            TableStorageInformation.Columns.ForEach(col =>
            {
                foreach (DataGridViewColumn column in Columns)
                    if (column.GetTag().Field.Equals(col.Field))
                    {
                        column.Width = col.Width;
                        column.DisplayIndex = col.Order;
                        break;
                    }
            });
        }

        protected override void OnColumnWidthChanged(DataGridViewColumnEventArgs e)
        {
            base.OnColumnWidthChanged(e);

            // Если возникло событие не при инициализации
            if (!IsInitialization)
                TableStorageInformationSave();
        }
        

        #endregion
    }
}
