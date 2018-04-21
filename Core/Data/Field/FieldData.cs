using Core.Data.Table;
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
        private string displayName;

        /// <summary>
        /// Является ли поле идентификатором таблицы
        /// </summary>
        [DataMember]
        public bool IsIdentifier { get; set; } = false;

        /// <summary>
        /// Имя поля
        /// </summary>
        [DataMember]
        public string Name { get; set; } = "NoName";

        /// <summary>
        /// Отображаемое название поля
        /// </summary>
        [DataMember]
        public string DisplayName
        {
            get => string.IsNullOrEmpty(displayName) ? Name : displayName;
            set
            {
                displayName = value;
            }
        }

        /// <summary>
        /// Тип поля
        /// </summary>
        [DataMember]
        public FieldType Type { get; set; } = FieldType.TEXT;

        /// <summary>
        /// Длина текста. Используется только для текстового типа.
        /// </summary>
        [DataMember]
        public int Size { get; set; } = 0;

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

        /// <summary>
        /// Таблица в которой находится текущее поле
        /// </summary>
        [DataMember]
        public TableData ParentTable { get; set; } = null;

        public override string ToString()
        {
            return Name;
        }
    }
}
