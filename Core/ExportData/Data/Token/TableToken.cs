using Core.Data.Field;
using Core.Data.Table;
using Core.ExportData.Data.Record;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace Core.ExportData.Data.Token
{
    public class TableToken : IToken
    {
        private static int Index = 1;

        public static void ResetIndex()
        {
            Index = 1;
        }

        // End static

        public TableToken(TableData table)
        {
            InternalName = $"T{Index++}";
            Table = table;
        }

        public bool IsClassificator { get; set; }

        public bool IsRootable { get; set; }

        public TableData Table { get; set; }

        public string InternalName { get; private set; }

        public FieldData JoinFieldParent { get; set; }

        public FieldData JoinFieldCurrent { get; set; }
        
        public FieldToken FieldIdToken { get; set; }

        public List<TableToken> Tables { get; } = new List<TableToken>();

        public List<FieldToken> Fields { get; } = new List<FieldToken>();

        public int GetWidth()
        {
            return Fields.Count + Tables.Sum(it => it.GetWidth());
        }

        public RecordTable CreateRecordTable()
        {
            return new RecordTable() { Token = this };
        }

        public RecordItem CreateRecordItem()
        {
            var item = new RecordItem() { IsEmpty = true };
            item.Fields.AddRange(Fields.Select(it => new RecordField() { Token = it }));
            item.Tables.AddRange(Tables.Select(it => new RecordTable() { Token = it }));
            return item;
        }

        public IEnumerable<string> JoinEnumerable()
        {
            foreach (var table in Tables)
            {
                if (table.JoinFieldParent != null && table.JoinFieldCurrent != null)
                    yield return $"LEFT JOIN {table.Table.Name} AS {table.InternalName} ON {table.InternalName}.{table.JoinFieldCurrent.Name} = {InternalName}.{table.JoinFieldParent.Name}";

                foreach (var strJoin in table.JoinEnumerable())
                    yield return strJoin;
            }
        }

        public IEnumerable<string> FieldEnumerable()
        {
            // Если идентификатора нету в выбранных столбцах, то добавляем, он нам нужен
            if (!Fields.Contains(FieldIdToken))
                yield return $"{InternalName}.{FieldIdToken.Field.Name} AS {FieldIdToken.InternalName}";

            foreach (var field in Fields)
                yield return $"{InternalName}.{field.Field.Name} AS {field.InternalName}";

            foreach (var table in Tables)
                foreach (var strField in table.FieldEnumerable())
                    yield return strField;
        }

        public string BuildSqlExpression()
        {
            return $"SELECT {string.Join(", ", FieldEnumerable())}\r\nFROM {Table.Name} AS {InternalName}\r\n{string.Join("\r\n", JoinEnumerable())}";
        }

        public void PrintHeaderToExcel(ExcelWorksheet worksheet, int row, int col, out int offsetRow, out int offsetCol)
        {
            int nextRow = row, nextCol = col, maxRow = row + 2;
            worksheet.Cells[row, col].Value = IsClassificator ? JoinFieldParent.DisplayName : Table.DisplayName;

            Fields.ForEach(it => it.PrintHeaderToExcel(worksheet, row + 1, nextCol, out nextRow, out nextCol));

            Tables.ForEach(it =>
            {
                it.PrintHeaderToExcel(worksheet, row + 1, nextCol, out nextRow, out nextCol);
                if (maxRow < nextRow)
                    maxRow = nextRow;
            });

            worksheet.Cells[row, col, row, nextCol - 1].Merge = true;
            if (IsRootable)
            {
                var border = worksheet.Cells[row, col, maxRow - 1, nextCol - 1].Style.Border;
                border.Top.Style = ExcelBorderStyle.Medium;
                border.Left.Style = ExcelBorderStyle.Medium;
                border.Right.Style = ExcelBorderStyle.Medium;
                border.Bottom.Style = ExcelBorderStyle.Medium;
                border.Top.Color.SetColor(Color.DarkRed);
                border.Left.Color.SetColor(Color.DarkRed);
                border.Right.Color.SetColor(Color.DarkRed);
                border.Bottom.Color.SetColor(Color.DarkRed);
            }

            offsetRow = maxRow;
            offsetCol = nextCol;
        }
    }
}
