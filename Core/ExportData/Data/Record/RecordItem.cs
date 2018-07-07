using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Core.ExportData.Data.Record
{
    public class RecordItem : IRecordReader
    {
        public List<RecordField> Fields { get; } = new List<RecordField>();

        public List<RecordTable> Tables { get; } = new List<RecordTable>();

        public bool IsRootItem { get; set; }

        public bool IsEmpty { get; set; }

        public void Process(SqlDataReader reader)
        {
            if (IsEmpty)
            {
                Fields.ForEach(it => it.Process(reader));
                IsEmpty = false;
            }
            Tables.ForEach(it => it.Process(reader));
        }

        public void PrintToExcel(ExcelWorksheet worksheet, int row, int col, out int offsetRow, out int offsetCol)
        {
            int nextRow = row, nextCol = col, maxRow;

            Fields.ForEach(it => it.PrintToExcel(worksheet, row, nextCol, out nextRow, out nextCol));
            maxRow = nextRow;

            Tables.ForEach(it =>
            {
                it.PrintToExcel(worksheet, row, nextCol, out nextRow, out nextCol);
                if (maxRow < nextRow)
                    maxRow = nextRow;
            });

            if (Fields.Count > 0 || Tables.Count > 0)
                worksheet.Cells[maxRow - 1, col, maxRow - 1, nextCol - 1]
                    .Style.Border.Bottom
                    .Style = IsRootItem ? ExcelBorderStyle.Medium : ExcelBorderStyle.Thin;

            offsetRow = maxRow;
            offsetCol = nextCol;
        }
    }
}
