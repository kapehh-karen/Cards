using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Data.Design.InternalData
{
    public class ControlData
    {
        /// <summary>
        /// Строка вида: namespace + type
        /// для динамического создания класса
        /// </summary>
        public string FullClassName { get; set; }

        public List<PropertyData> Properties { get; set; } = new List<PropertyData>();
    }
}
