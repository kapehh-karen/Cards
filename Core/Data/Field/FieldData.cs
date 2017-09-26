using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Core.Data.Field
{
    [DataContract(IsReference = true)]
    public class FieldData
    {
        /// <summary>
        /// Является ли поле идентификатором таблицы
        /// </summary>
        [DataMember]
        public bool IsIdentifier { get; set; } = false;

        /// <summary>
        /// Название поля
        /// </summary>
        [DataMember]
        public string Name { get; set; } = "NULL";

        /// <summary>
        /// Тип поля
        /// </summary>
        [DataMember]
        public FieldType Type { get; set; } = FieldType.TEXT;

        /// <summary>
        /// Видимость поля в конструкторе форм
        /// </summary>
        [DataMember]
        public bool Visible { get; set; } = true;

        /// <summary>
        /// Обязательное поле
        /// </summary>
        [DataMember]
        public bool Required { get; set; } = false;

        /// <summary>
        /// Информация о связанной таблице
        /// </summary>
        [DataMember]
        public BindField BindData { get; set; } = null;

        public override string ToString()
        {
            return this.Name;
        }
    }
}
