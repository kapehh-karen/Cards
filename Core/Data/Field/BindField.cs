using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Data.Field
{
    /// <summary>
    /// Предоставляет связку "таблица - поле",
    /// используется в связанном значении и в списках данных
    /// </summary>
    public class BindField
    {
        /// <summary>
        /// Какая таблица
        /// </summary>
        public string Table { get; set; }

        /// <summary>
        /// С каким полем
        /// </summary>
        public string Field { get; set; }
    }
}
