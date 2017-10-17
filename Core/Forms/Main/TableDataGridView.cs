using Core.Data.Field;
using Core.Data.Table;
using System;
using System.Collections.Generic;
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

                ColumnFields.Clear();
                ColumnFields.Add(tableData.Fields[0]);
                ColumnFields.Add(tableData.Fields[1]);
                ColumnFields.Add(tableData.Fields[2]);
                ColumnFields.Add(tableData.Fields[3]);
            }
        }

        public void FillTable()
        {
            var columns = string.Join(", ", ColumnFields.Select(f => $"{Table.Name}.{f.Name}").ToArray());
            var query = $"SELECT {columns} FROM {Table.Name}";

            MessageBox.Show(query);
        }
    }
}
