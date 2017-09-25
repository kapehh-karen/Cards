using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Data.Field
{
    public class FieldData
    {
        /// <summary>
        /// Является ли поле идентификатором таблицы
        /// </summary>
        public bool IsIdentifier { get; set; } = false;

        /// <summary>
        /// Название поля
        /// </summary>
        public string Name { get; set; } = "NULL";

        /// <summary>
        /// Тип поля
        /// </summary>
        public FieldType Type { get; set; } = FieldType.TEXT;

        /// <summary>
        /// Видимость поля в конструкторе форм
        /// </summary>
        public bool Visible { get; set; } = true;

        /// <summary>
        /// Информация о связанной таблице
        /// </summary>
        public BindField BindData { get; set; } = null;

        /// <summary>
        /// Значение поля (будет использоваться для получния предыдущего/текущего значения поля)
        /// </summary>
        public object Value { get; set; } = null;
    }
}
