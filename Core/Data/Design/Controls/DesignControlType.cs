using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Data.Design.Controls
{
    public enum DesignControlType
    {
        /// <summary>
        /// Простой элемент формы
        /// </summary>
        STANDARD = 1,

        /// <summary>
        /// Элемент содержащий другие подэлементы
        /// </summary>
        CONTAINER = 2,

        /// <summary>
        /// Элемент связан с полем
        /// </summary>
        FIELD = 3,

        /// <summary>
        /// Элемент предоставляет внешние данные
        /// </summary>
        LINKED_TABLE = 4,
    }
}
