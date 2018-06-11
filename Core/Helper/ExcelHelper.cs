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
        public static void SaveDataGridViewToExcel(WaitDialog waitDialog, string fileName, DataGridView dataGridView)
        {
            var visibleColumns = dataGridView.Columns.Cast<DataGridViewColumn>().Where(col => col.Visible).ToList();
            var colCount = visibleColumns.Count;
            var rowCount = dataGridView.Rows.Count;

            using (var p = new ExcelPackage())
            {
                var ws = p.Workbook.Worksheets.Add("Лист с данными");

                var excelRow = ws.Row(1);
                excelRow.Height = 30;
                excelRow.Style.Font.Bold = true;
                excelRow.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                excelRow.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excelRow.Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                for (var i = 1; i <= colCount; i++)
                {
                    var column = visibleColumns[i - 1];
                    ws.Cells[1, i].Value = column.HeaderText;
                    if (column.ValueType == typeof(DateTime))
                        ws.Column(i).Style.Numberformat.Format = "dd.MM.yyyy";
                }

                for (var i = 1; i <= rowCount; i++)
                {
                    var row = dataGridView.Rows[i - 1];

                    waitDialog.Message = $"Обрабатывается {i} запись из {rowCount}...";

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
                
                ws.Cells.AutoFitColumns(10, 50);
                ws.Cells.Style.WrapText = true;
                ws.View.FreezePanes(2, colCount + 1);

                p.SaveAs(new FileInfo(fileName));
            }
        }
    }
}
