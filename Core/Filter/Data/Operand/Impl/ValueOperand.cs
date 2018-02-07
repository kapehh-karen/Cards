using Core.Data.Field;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Filter.Data.Operand.Impl
{
    public class ValueOperand : IFilterOperand
    {
        public override OperandType Type => OperandType.VALUE;

        public override FieldType ValueType { get; set; }

        /// <summary>
        /// Значение любого FieldType типа
        /// </summary>
        public object Value { get; set; }

        public override bool Completed => Value != null;

        public override string SQLExpression => Value?.ToString();
    }
}
