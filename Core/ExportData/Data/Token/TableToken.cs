using Core.Data.Field;
using Core.Data.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.ExportData.Data.Token
{
    public class TableToken
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

        public TableData Table { get; set; }

        public string InternalName { get; private set; }

        public FieldData JoinFieldParent { get; set; }

        public FieldData JoinFieldCurrent { get; set; }

        public FieldToken FieldIdToken { get; set; }

        public List<TableToken> Tables { get; } = new List<TableToken>();

        public List<FieldToken> Fields { get; } = new List<FieldToken>();

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
    }
}
