using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Filter.Data.Operand
{
    public enum OperandType
    {
        /// <summary>
        /// Заданное значение
        /// </summary>
        VALUE,

        /// <summary>
        /// Поле
        /// </summary>
        FIELD,

        /// <summary>
        /// Выборка
        /// </summary>
        SUBQUERY
    }
}
