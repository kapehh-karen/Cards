using Core.Storage.Documents;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Helper
{
    public static class ExcelHelper
    {
        public static void SaveDataGridViewToExcel(string fileName, DataGridView dataGridView)
        {
            var visibleColumns = dataGridView.Columns.Cast<DataGridViewColumn>().Where(col => col.Visible).ToList();
            var colCount = visibleColumns.Count;

            using (var p = new ExcelPackage())
            {
                var ws = p.Workbook.Worksheets.Add("Лист с данными");
                
                for (var i = 1; i <= colCount; i++)
                {
                    var column = visibleColumns[i - 1];
                    var cellCol = ws.Cells[1, i];

                    cellCol.Value = column.HeaderText;
                    cellCol.Style.Font.Bold = true;
                    cellCol.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    cellCol.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    cellCol.Style.Font.Color.SetColor(Color.White);
                    cellCol.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    cellCol.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    if (column.ValueType == typeof(DateTime))
                        ws.Column(i).Style.Numberformat.Format = "dd.MM.yyyy";
                }

                for (var i = 1; i <= dataGridView.Rows.Count; i++)
                {
                    var row = dataGridView.Rows[i - 1];
                    for (var k = 1; k <= colCount; k++)
                    {
                        var column = visibleColumns[k - 1];
                        var cellValue = ws.Cells[1 + i, k];

                        if (column.ValueType == typeof(bool))
                            cellValue.Value = Convert.ToBoolean(row.Cells[column.Index].Value) ? "Да" : "Нет";
                        else
                            cellValue.Value = row.Cells[column.Index].Value;
                    }
                }

                ws.Row(1).Height = 30;
                ws.Cells.AutoFitColumns(10, 50);
                ws.Cells.Style.WrapText = true;

                p.SaveAs(new FileInfo(fileName));
            }
        }
    }
}
