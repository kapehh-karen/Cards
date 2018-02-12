using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Filter.Data;
using Core.Filter.Data.Condition.Impl;
using Core.Data.Field;

namespace Core.Filter.SQLBuilders.Impl
{
    public class FullSQLBuilder : ISQLBuilder
    {
        public override string BuildSQLExpression(FieldData[] fields = null)
        {
            var table = Filter.FilterTable;

            // where part query
            var where = Filter.Where.Completed ? $"WHERE {Filter.Where.SQLExpression}" : string.Empty;

            if (fields != null)
            {
                // columns part query
                var columns = string.Join(", ", fields
                    .Select(f => f.Type != FieldType.BIND ? $"[{table.AliasName}].[{f.Name}] AS [{f.Name}]" : $"[{f.Name}__{f.BindData.Table.Name}].[{f.BindData.Field.Name}] AS [{f.Name}]")
                    .ToArray());

                // joins part query
                var joins = string.Join("\r\n", fields.Where(f => f.Type == FieldType.BIND)
                    .Select(f => $"LEFT JOIN [{f.BindData.Table.Name}] AS [{f.Name}__{f.BindData.Table.Name}] ON [{f.Name}__{f.BindData.Table.Name}].[{f.BindData.Table.IdentifierField.Name}] = [{table.AliasName}].[{f.Name}]")
                    .ToArray());

                return $"SELECT {columns} FROM [{table.Table.Name}] AS [{table.AliasName}]\r\n{joins}\r\n{where}";
            }
            else
            {
                return $"SELECT * FROM [{table.Table.Name}] AS [{table.AliasName}]\r\n{where}";
            }
        }
    }
}
