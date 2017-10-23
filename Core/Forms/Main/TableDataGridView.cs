using Core.Connection;
using Core.Data.Base;
using Core.Data.Field;
using Core.Data.Table;
using Core.Helper;
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
        private TableData tableData;

        public TableDataGridView()
        {
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
                    // TODO: Add to ColumnFields IdentificatorField if it not contains
                    FieldID = tableData.IdentifierField;
                }
            }
        }

        public DataBase Base { get; set; }

        public DataTable CurrentDataTable { get; set; }

        public object GetCurrentID()
        {
            return Rows[CurrentRow.Index].Cells[FieldID.Name].Value;
        }

        public void FillTable()
        {
            if (Base == null || Table == null)
                return;

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
                var data = new DataSet();

                WaitDialog.Run("Ожидается ответ от сервера...", () => adapter.Fill(data));

                var tableData = data.Tables[0];
                CurrentDataTable = tableData;
                DataSource = tableData;

                // Hide ID column
                this.Columns[FieldID.Name].Visible = false;

                // Renaming columns header
                foreach (DataGridViewColumn column in this.Columns)
                {
                    var fieldData = ColumnFields.Single(f => f.Name.Equals(column.Name));
                    column.HeaderText = fieldData.DisplayName;
                    column.Tag = fieldData;
                }
            }
        }
    }
}
