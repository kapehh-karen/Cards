using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Filter.Data;
using Core.Filter.Data.Condition.Impl;

namespace Core.Filter.SQLBuilders.Impl
{
    public class FullSQLBuilder : ISQLBuilder
    {
        public FilterData Filter { get; set; }

        public string SQLExpression
        {
            get
            {
                var group = Filter.Where as ContainerCondition;
                var where = group.Conditions.Count > 0 ? $"WHERE {group.SQLExpression}" : string.Empty;
                return $"SELECT * FROM [{Filter.FilterTable.Table.Name}] as [{Filter.FilterTable.AliasName}] {where}";
            }
        }
    }
}
