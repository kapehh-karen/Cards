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
                }
            }
        }

        public DataBase Base { get; set; }

        public DataSet Data { get; set; }

        public void FillTable()
        {
            using (var dbc = WaitDialog.Run("Подождите, идет подключение к SQL Server",
                () => new SQLServerConnection(Base)))
            {
                var columns = string.Join(", ", ColumnFields.Select(f => $"{Table.Name}.{f.Name}").ToArray());
                var query = $"SELECT {columns} FROM {Table.Name}";
                var connection = dbc.Connection;
                var adapter = new SqlDataAdapter(query, connection);

                Data = new DataSet();
                adapter.Fill(Data);
                DataSource = Data.Tables[0];
            }
        }
    }
}
