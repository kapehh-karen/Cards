using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Data.Field;
using Core.Data.Design;
using System.Runtime.Serialization;
using Core.Data.Design.InternalData;
using Core.Data.Base;
using Core.Forms.Main.CardForm;
using Core.API;
using Core.Forms.Main;

namespace Core.Data.Table
{
    [DataContract(IsReference = true)]
    public class TableData
    {
        private string displayName;
        private FormCardView dialog;
        private FormTableView formView;

        /// <summary>
        /// Имя таблицы
        /// </summary>
        [DataMember]
        public string Name { get; set; } = "NULL";

        /// <summary>
        /// Отображаемое название таблицы
        /// </summary>
        [DataMember]
        public string DisplayName
        {
            get => string.IsNullOrEmpty(displayName) ? Name : displayName;
            set => displayName = value;
        }

        /// <summary>
        /// Поле идентификатор
        /// </summary>
        public FieldData IdentifierField => Fields.FirstOrDefault(fd => fd.IsIdentifier);

        /// <summary>
        /// Поле быстрого перехода
        /// </summary>
        public FieldData FastJumpField => Fields.FirstOrDefault(fd => fd.FastJump);

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
        /// Видимость таблицы
        /// </summary>
        [DataMember]
        public bool Visible { get; set; } = true;

        // Разрешения на редактирование таблицы
        [DataMember]
        public bool AllowNew { get; set; } = true;
        [DataMember]
        public bool AllowEdit { get; set; } = true;
        [DataMember]
        public bool AllowDelete { get; set; } = true;

        /// <summary>
        /// Последнее изменение таблицы (для классификаторов)
        /// </summary>
        public DateTime LastUpdate { get; set; } = DateTime.MinValue;

        /// <summary>
        /// База в которой находится текущая таблица
        /// </summary>
        public DataBase ParentBase { get; set; } = null;

        public string FullDisplayName => $"{(IsClassifier ? "Классификатор" : "Таблица")} - {DisplayName}";

        public FieldData GetFieldByName(string fieldName)
            => Fields.FirstOrDefault(it => it.Name.Equals(fieldName));

        public LinkedTable GetLinkedTableByName(string linkedName)
            => LinkedTables.FirstOrDefault(it => it.Name.Equals(linkedName));

        public FormCardView CardView
        {
            get
            {
                if (dialog == null)
                {
                    dialog = new FormCardView() { Table = this };
                    dialog.SendEventFormCreated();
                }
                return dialog;
            }
        }

        public FormTableView TableView
        {
            get
            {
                if (formView == null)
                {
                    formView = new FormTableView() { Table = this };
                    formView.FillTable();
                    formView.SendEventFormCreated();
                }

                return formView;
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
