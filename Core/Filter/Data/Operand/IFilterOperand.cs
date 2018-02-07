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

        /// <summary>
        /// Тип операнда. Используется в форме фильтрации для определения типа вводимого значения
        /// </summary>
        public abstract FieldType ValueType { get; set; }

        public virtual bool Completed => true;

        public virtual string SQLExpression => string.Empty;
    }
}
