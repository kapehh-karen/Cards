using Core.Data.Field;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OfficeOpenXml;

namespace Core.ExportData.Data.Token
{
    public class FieldToken : IToken
    {
        private static int Index = 1;

        public static void ResetIndex()
        {
            Index = 1;
        }

        // End static

        public FieldToken(FieldData field)
        {
            InternalName = $"F{Index++}";
            Field = field;
        }

        public FieldData Field { get; set; }

        public string InternalName { get; private set; }

        public void PrintHeaderToExcel(ExcelWorksheet worksheet, int row, int col, out int offsetRow, out int offsetCol)
        {
            worksheet.Cells[row, col].Value = Field.DisplayName;
            offsetCol = col + 1;
            offsetRow = row + 1;
        }
    }
}
