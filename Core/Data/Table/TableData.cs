using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Data.Field;
using Core.Data.Design;
using System.Runtime.Serialization;
using Core.Data.Design.InternalData;

namespace Core.Data.Table
{
    [DataContract(IsReference = true)]
    public class TableData
    {
        /// <summary>
        /// Имя таблицы
        /// </summary>
        [DataMember]
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
        [DataMember]
        public List<FieldData> Fields { get; set; } = new List<FieldData>();

        /// <summary>
        /// Форма связанная с этой таблицей
        /// </summary>
        [DataMember]
        public FormData Form { get; set; } = null;

        /// <summary>
        /// Связанные данные (списки). Список таблиц и по какому полю связаны
        /// </summary>
        [DataMember]
        public List<LinkedTable> LinkedTables { get; set; } = new List<LinkedTable>();

        /// <summary>
        /// Является ли таблица классификатором
        /// </summary>
        [DataMember]
        public bool IsClassifier { get; set; } = false;

        /// <summary>
        /// Последнее изменение таблицы (для классификаторов)
        /// </summary>
        [DataMember]
        public DateTime LastUpdate { get; set; } = DateTime.MinValue;

        public override string ToString()
        {
            return this.Name;
        }
    }
}
