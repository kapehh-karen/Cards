using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Data.Field
{
    public class BindFieldData
    {
        /// <summary>
        /// По какой таблице связано
        /// </summary>
        public string Table { get; set; }

        /// <summary>
        /// Какое поле будет отображаться вместо ID
        /// </summary>
        public string DisplayField { get; set; }
    }
}
