using Core.Common.DataGrid;
using Core.Connection;
using Core.Data.Base;
using Core.Data.Field;
using Core.Data.Model;
using Core.Data.Table;
using Core.Helper;
using Core.Storage.Tables;
using Core.Storage.Tables.TableStorageData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public TableDataGridView() : base()
        {
            DoubleBuffered = true;
            BorderStyle = BorderStyle.Fixed3D;
        }

        public override DataGridType ViewType => DataGridType.TableAndClassificator;

        /// <summary>
        /// Используется в режиме классификатора. Содержит ссылку на поле для которого используется классификатор.
        /// </summary>
        public FieldData ParentField { get; set; } = null;

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
        
        /// <summary>
        /// Используется когда требуется поддержка кеша, и наоборот
        /// </summary>
        public bool AllowCache { get; set; } = true;

        public void FillTable(bool forceUpdate = false)
        {
            if (Base == null || Table == null)
                return;

            var needUpdate = true;
            var fields = TableStorageInformation.Columns.Select(item => item.Field).ToArray();

            // При загрузке данных в компонент используется флаг AllowCache
            if (Table.IsClassifier && AllowCache && CurrentDataTable == null)
            {
                CurrentDataTable = TableStorageInformation.Data;
                needUpdate = !TableStorageInformation.HasData;
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
                    
                    this.DataSource = null;
                    var data = new DataSet();
                    WaitDialog.Run("Ожидается ответ от сервера...", () => adapter.Fill(data));
                    CurrentDataTable = data.Tables[0];
                    CurrentDataView.Table = CurrentDataTable;
                    firstAfterBind = true; // Перед биндингом
                    this.DataSource = CurrentDataView;

                    adapter.Dispose();
                }

                // Не важно значение флага AllowCache, всегда сохраняем данные классификатора в память
                if (Table.IsClassifier && CurrentDataTable != null)
                {
                    TableStorageInformation.Data = CurrentDataTable;
                }
            }
            else
            {
                CurrentDataView.Table = CurrentDataTable;
                firstAfterBind = true; // Перед биндингом
                this.DataSource = CurrentDataView;
            }
        }
        
        #region Post-processing for data bindings

        private object needSelectID;
        private bool firstAfterBind;

        public DataGridViewColumn KeepSelectedColumn { get; set; }
        
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
            
            // Выделить строку
            TrySelectRow();
        }

        #endregion

        #region Table Storage Information

        protected override void OnTableStorageInformationUpdated()
        {
            // При обновлении настроек таблицы, обновляем контент
            FillTable(true);
        }

        /// <summary>
        /// Распределение полей по дефолту. Если в настройках не указано какие поля отображать
        /// </summary>
        /// <returns>false - если инициализации небыло, true - если была</returns>
        protected override bool InitializeFields()
        {
            FieldData displayedField = null;

            // Обязательно добавляем ID, т.к. он может быть скрыт, но нам он нужен
            TableStorageInformation.AddColumn(FieldID);

            // Если открывается через просмотрщик классификаторов
            if (ParentField?.Type == FieldType.BIND)
            {
                displayedField = ParentField.BindData?.Field; // Отображаемое поле

                // Добавляем в список полей поле которое должно отображаться заместо ID
                if (displayedField != null && displayedField != FieldID)
                {
                    // Только отображаемое поле
                    TableStorageInformation.AddColumn(displayedField);
                }
            }

            // Все видимые поля (кроме ID и DisplayField, их добавили уже)
            Table.Fields
                .Where(f => f.Visible && f != FieldID && f != displayedField)
                .Take(5)
                .ForEach(TableStorageInformation.AddColumn);
            return true;
        }
        
        #endregion
    }
}
