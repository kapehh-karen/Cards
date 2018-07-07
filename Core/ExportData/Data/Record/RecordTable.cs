using Core.ExportData.Data.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using OfficeOpenXml;
using Core.Helper;

namespace Core.ExportData.Data.Record
{
    public class RecordTable : IRecordReader
    {
        public TableToken Token { get; set; }

        public bool IsRootTable { get; set; }

        public Dictionary<object, RecordItem> Items { get; } = new Dictionary<object, RecordItem>();

        public void Process(SqlDataReader reader)
        {
            var id = reader[Token.FieldIdToken.InternalName];
            if (id == null || id == DBNull.Value)
                return;

            if (Items.ContainsKey(id))
            {
                var existingRecordItem = Items[id];
                existingRecordItem.Process(reader);
            }
            else
            {
                var newRecordItem = Token.CreateRecordItem();
                newRecordItem.IsRootItem = IsRootTable;
                Items.Add(id, newRecordItem);
                newRecordItem.Process(reader);
            }
        }

        public void PrintToExcel(ExcelWorksheet worksheet, int row, int col, out int offsetRow, out int offsetCol)
        {
            int nextRow = row, nextCol = col;

            if (Items.Count > 0)
                Items.Values.ForEach(it => it.PrintToExcel(worksheet, nextRow, col, out nextRow, out nextCol));
            else
                nextCol += Token.GetWidth();

            offsetRow = nextRow;
            offsetCol = nextCol;
        }
    }
}
