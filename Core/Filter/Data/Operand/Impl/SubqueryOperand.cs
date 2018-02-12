using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Data.Field;
using Core.Filter.SQLBuilders.Impl;

namespace Core.Filter.Data.Operand.Impl
{
    public class SubqueryOperand : IFilterOperand
    {
        public override OperandType Type => OperandType.SUBQUERY;

        public override FieldType ValueType
        {
            get => FieldType.NUMBER;
            set { }
        }
        
        public FilterData CurrentFilter { get; set; }
        
        public override bool Completed => CurrentFilter != null;

        public override IEnumerable<KeyValuePair<string, object>> GetParameters()
        {
            foreach (var param in CurrentFilter.Where.GetParameters())
                yield return param;
        }

        public override string SQLExpression => CurrentFilter.SQLExpression;
    }
}
