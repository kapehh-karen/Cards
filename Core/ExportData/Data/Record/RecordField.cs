using Core.ExportData.Data.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using OfficeOpenXml;
using Core.Data.Field;

namespace Core.ExportData.Data.Record
{
    public class RecordField : IRecordReader
    {
        public FieldToken Token { get; set; }

        public object Value { get; set; }
        
        public void Process(SqlDataReader reader)
        {
            var val = reader[Token.InternalName];
            Value = val == DBNull.Value ? null : val;
        }

        public void PrintToExcel(ExcelWorksheet worksheet, int row, int col, out int offsetRow, out int offsetCol)
        {
            var cell = worksheet.Cells[row, col];

            if (Token.Field.Type == FieldType.DATE)
                cell.Style.Numberformat.Format = "dd.MM.yyyy";

            if (Token.Field.Type == FieldType.BOOLEAN)
                cell.Value = Value == null ? null : Convert.ToBoolean(Value) ? "Да" : "Нет";
            else
                cell.Value = Value;

            offsetRow = row + 1;
            offsetCol = col + 1;
        }
    }
}
