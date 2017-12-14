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

        /// <summary>
        /// Тип значения, используется в окне изменения значения в фильтрах
        /// </summary>
        public FieldType FieldType { get; set; }

        /// <summary>
        /// Значение любого FieldType типа
        /// </summary>
        public object Value { get; set; }
    }
}
