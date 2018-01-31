using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Data.Field
{
    public enum FieldType
    {
        UNKNOWN = 0,

        /// <summary>
        /// Текстовое
        /// </summary>
        TEXT = 1,

        /// <summary>
        /// Числовое
        /// </summary>
        NUMBER = 2,

        /// <summary>
        /// Дата
        /// </summary>
        DATE = 3,

        /// <summary>
        /// Логический
        /// </summary>
        BOOLEAN = 4,

        /// <summary>
        /// Связанное значение
        /// </summary>
        BIND = 5
    }
}
