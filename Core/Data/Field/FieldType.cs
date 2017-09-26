using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Data.Field
{
    public enum FieldType
    {
        /// <summary>
        /// Текстовое
        /// </summary>
        TEXT = 0,

        /// <summary>
        /// Числовое
        /// </summary>
        NUMBER = 1,

        /// <summary>
        /// Дата
        /// </summary>
        DATE = 2,

        /// <summary>
        /// Связанное значение
        /// </summary>
        BIND = 3
    }
}
