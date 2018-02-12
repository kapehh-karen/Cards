using Core.Data.Field;
using Core.Filter.Data;
using Core.Filter.Data.Condition;
using Core.Filter.Data.Condition.Impl;
using Core.Filter.Data.Operand.Impl;
using Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Filter.SQLBuilders
{
    public abstract class ISQLBuilder
    {
        public FilterData Filter { get; set; }

        public Dictionary<string, object> BuildParams() =>
            Filter.Where.GetParameters().ToDictionary(item => item.Key, item => item.Value);

        public abstract string BuildSQLExpression(FieldData[] fields = null);
    }
}
