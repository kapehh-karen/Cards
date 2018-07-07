using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.ExportData.Data.Token
{
    public interface IToken
    {
        void PrintHeaderToExcel(ExcelWorksheet worksheet, int row, int col, out int offsetRow, out int offsetCol);
    }
}
