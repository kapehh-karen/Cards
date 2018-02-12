using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Filter.Data;
using Core.Filter.Data.Condition.Impl;
using Core.Data.Field;

namespace Core.Filter.SQLBuilders.Impl
{
    public class CountSQLBuilder : ISQLBuilder
    {
        public override string BuildSQLExpression(FieldData[] fields = null)
        {
            var group = Filter.Where as ContainerCondition;
            var where = group.Conditions.Count > 0 ? $"AND {group.SQLExpression}" : string.Empty;
            var currentTable = Filter.FilterTable;
            var parentTable = Filter.Parent.FilterTable;
            var foreignField = parentTable.Table.LinkedTables.Single(lt => lt.Table == currentTable.Table).Field;
            return $@"(SELECT COUNT(*)
                        FROM [{currentTable.Table.Name}] AS [{currentTable.AliasName}]
                        WHERE [{parentTable.AliasName}].[{parentTable.Table.IdentifierField.Name}] = [{currentTable.AliasName}].[{foreignField.Name}] {where})";
        }
    }
}
