using Core.Data.Field;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Core.Data.Table
{
    [DataContract(IsReference = true)]
    public class LinkedTable
    {
        /// <summary>
        /// Таблица хранящая связанные данные
        /// </summary>
        [DataMember]
        public TableData Table { get; set; }

        /// <summary>
        /// Внешний ключ (Foreign Key), в него записывается значение идентификатора
        /// </summary>
        [DataMember]
        public FieldData Field { get; set; }

        public string Name => $"LinkedTable__{Table?.Name}_{Field?.Name}";

        public override string ToString()
        {
            return $"Таблица \"{Table?.Name}\", внешний ключ \"{Field?.Name}\"";
        }
    }
}
