using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Filter.Data.Operand
{
    public enum OperandWeight
    {
        /// <summary>
        /// Задающий тип для оператора и правых операндов
        /// </summary>
        HIGH = 1,

        /// <summary>
        /// Принимает требуемый тип и оперирует только с ним
        /// </summary>
        LOW = 2
    }
}
