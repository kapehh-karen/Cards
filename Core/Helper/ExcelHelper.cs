﻿using Core.Connection;
using Core.ExportData.Data.Token;
using Core.Storage.Documents;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Helper
{
    public static class ExcelHelper
    {
        public static void SaveDataGridViewToExcel(WaitDialog dialog, string fileName, DataGridView dataGridView)
        {
            var visibleColumns = dataGridView.Columns.Cast<DataGridViewColumn>().Where(col => col.Visible).ToList();
            var colCount = visibleColumns.Count;
            var rowCount = dataGridView.Rows.Count;

            using (var p = new ExcelPackage())
            {
                var ws = p.Workbook.Worksheets.Add("Лист с данными");

                var excelRow = ws.Row(1);
                excelRow.Height = 30;

                var excelHeaderCellsStyle = ws.Cells[1, 1, 1, colCount].Style;
                excelHeaderCellsStyle.Font.Bold = true;
                excelHeaderCellsStyle.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                excelHeaderCellsStyle.VerticalAlignment = ExcelVerticalAlignment.Center;
                excelHeaderCellsStyle.Border.Bottom.Style = ExcelBorderStyle.Medium;

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

                    dialog.Message = $"Обрабатывается {i} запись из {rowCount}...";

                    for (var k = 1; k <= colCount; k++)
                    {
                        var column = visibleColumns[k - 1];
                        var cellValue = ws.Cells[1 + i, k];
                        cellValue.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        cellValue.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        cellValue.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                        if (column.ValueType == typeof(bool))
                        {
                            var boolVal = row.Cells[column.Index].Value;
                            cellValue.Value = boolVal == DBNull.Value ? "Пусто" : Convert.ToBoolean(boolVal) ? "Да" : "Нет";
                        }
                        else
                            cellValue.Value = row.Cells[column.Index].Value;
                    }
                }

                ws.Cells.AutoFitColumns(10, 50);
                ws.Cells.Style.WrapText = true;

                p.SaveAs(new FileInfo(fileName));
            }
        }

        public static void SaveExtendedTableToExcel(WaitDialog dialog, string fileName, TableToken rootTableToken)
        {
            var recordTable = rootTableToken.CreateRecordTable();
            recordTable.IsRootTable = true;

            dialog.Message = "Получение информации от сервера...";
            using (var dbc = new SQLServerConnection())
            using (var command = new SqlCommand(rootTableToken.BuildSqlExpression(), dbc.Connection))
            using (var reader = command.ExecuteReader())
                while (reader.Read())
                {
                    recordTable.Process(reader);
                }

            dialog.Message = "Запись данных в Excel файл...";
            using (var p = new ExcelPackage())
            {
                var ws = p.Workbook.Worksheets.Add("Лист с данными");
                recordTable.PrintToExcel(ws, 1, 1, out int row, out int col);
                ws.Cells.AutoFitColumns(10, 50);
                ws.Cells.Style.WrapText = true;
                p.SaveAs(new FileInfo(fileName));
            }
        }
    }
}
