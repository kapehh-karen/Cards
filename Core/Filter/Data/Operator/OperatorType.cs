using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Filter.Data.Operator
{
    public enum OperatorType
    {
        UNKNOWN,

        /// <summary>
        /// Равно
        /// </summary>
        EQUAL,

        /// <summary>
        /// Не равно
        /// </summary>
        NOT_EQUAL,

        /// <summary>
        /// Больше
        /// </summary>
        GREATER,

        /// <summary>
        /// Больше или равно
        /// </summary>
        GREATER_EQUAL,

        /// <summary>
        /// Меньше
        /// </summary>
        LESS,

        /// <summary>
        /// Меньше или равно
        /// </summary>
        LESS_EQUAL,
        
        /// <summary>
        /// Пусто
        /// </summary>
        IS_NULL,

        /// <summary>
        /// Не пусто
        /// </summary>
        IS_NOT_NULL,

        /// <summary>
        /// Содержит
        /// </summary>
        LIKE,

        /// <summary>
        /// Не содержит
        /// </summary>
        NOT_LIKE
    }
}
