using Core.Data.Field;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Core.Filter.Data
{
    [DataContract]
    public class FilterField
    {
        /// <summary>
        /// Поле таблицы
        /// </summary>
        [DataMember]
        public FieldData Field { get; set; }
    }
}
