using Core.Data.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Core.Data.Field
{
    [DataContract(IsReference = true)]
    public class BindField
    {
        /// <summary>
        /// Какая таблица используется (значение для записи берется из поля идентификатора этой же таблицы)
        /// </summary>
        [DataMember]
        public TableData Table { get; set; }

        /// <summary>
        /// Какое поле отображать пользователю
        /// </summary>
        [DataMember]
        public FieldData Field { get; set; }

        public string Name => $"BindField__{Table?.Name}_{Field?.Name}";

        public override string ToString()
        {
            return $"Таблица \"{Table?.Name}\", отображаемое поле \"{Field?.Name}\"";
        }
    }
}
