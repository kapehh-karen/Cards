using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Data.Field;

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

        public FilterData ParentFilter { get; set; }

        public FilterData CurrentFilter { get; set; }
        
        public override bool Completed => ParentFilter != null && CurrentFilter != null;

        public override string SQLExpression => $"<< {CurrentFilter.Where.SQLExpression} >>";
    }
}
