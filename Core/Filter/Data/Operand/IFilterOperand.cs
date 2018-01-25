using Core.Data.Field;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Filter.Data.Operand
{
    public abstract class IFilterOperand
    {
        public abstract OperandType Type { get; }

        public abstract FieldType ValueType { get; set; }

        public virtual string SQLExpression => string.Empty;
    }
}
