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
        public string Name { get; set; }

        /// <summary>
        /// Поле идентификатор
        /// </summary>
        public FieldData IdentifierField { get; set; }

        /// <summary>
        /// Поля таблицы (включая поле идентификатора)
        /// </summary>
        public List<FieldData> Fields { get; set; }

        /// <summary>
        /// Форма связанная с этой таблицей
        /// </summary>
        public FormData Form { get; set; }

        /// <summary>
        /// Связанные данные (списки). Список таблиц и по какому полю связаны
        /// </summary>
        public List<BindField> LinkedTables { get; set; }
    }
}
