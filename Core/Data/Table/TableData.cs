using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Data.Field;
using Core.Data.Design;

namespace Core.Data.Table
{
    public class TableData
    {
        /// <summary>
        /// Имя таблицы
        /// </summary>
        public string Name { get; set; } = "NULL";

        /// <summary>
        /// Поле идентификатор
        /// </summary>
        public FieldData IdentifierField
        {
            get
            {
                return this.Fields.FirstOrDefault(fd => fd.IsIdentifier);
            }
        }

        /// <summary>
        /// Поля таблицы (включая поле идентификатора)
        /// </summary>
        public List<FieldData> Fields { get; set; } = new List<FieldData>();

        /// <summary>
        /// Форма связанная с этой таблицей
        /// </summary>
        public FormData Form { get; set; } = null;

        /// <summary>
        /// Связанные данные (списки). Список таблиц и по какому полю связаны
        /// </summary>
        public List<BindField> LinkedTables { get; set; } = new List<BindField>();

        /// <summary>
        /// Является ли таблица классификатором
        /// </summary>
        public bool IsClassifier { get; set; } = false;

        /// <summary>
        /// Последнее изменение таблицы (для классификаторов)
        /// </summary>
        public DateTime LastUpdate { get; set; } = DateTime.MinValue;

        public override string ToString()
        {
            return this.Name;
        }
    }
}
