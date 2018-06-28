using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Data.Design.InternalData
{
    public class ControlData : ICloneable
    {
        /// <summary>
        /// Строка вида: namespace + type
        /// для динамического создания класса
        /// </summary>
        public string FullClassName { get; set; }

        public List<PropertyData> Properties { get; set; } = new List<PropertyData>();

        public List<ControlData> Chields { get; set; } = new List<ControlData>();

        public object Clone()
        {
            return new ControlData()
            {
                FullClassName = this.FullClassName,
                Properties = this.Properties.Select(it => it.Clone()).Cast<PropertyData>().ToList(),
                Chields = this.Chields.Select(it => it.Clone()).Cast<ControlData>().ToList()
            };
        }
    }
}
