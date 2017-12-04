using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Filter.Data.Operator
{
    public enum OperatorType
    {
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
        /// Между (в диапазоне)
        /// </summary>
        BETWEEN,

        /// <summary>
        /// Существует
        /// </summary>
        EXISTS,

        /// <summary>
        /// Не существует
        /// </summary>
        NOT_EXISTS,

        /// <summary>
        /// Пусто
        /// </summary>
        IS_NULL,

        /// <summary>
        /// Не пусто
        /// </summary>
        IS_NOT_NULL,
    }
}
