using Core.Data.Field;
using Core.Data.Table;
using Core.Storage.TableStorageData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Core.Storage
{
    [DataContract]
    public class TableStorageInformation
    {
        [DataMember]
        public List<TableStorageColumnData> Columns { get; set; } = new List<TableStorageColumnData>();

        public TableData Table { get; set; } = null;

        /// <summary>
        /// Полностью новая информация, не сохранена на диске
        /// </summary>
        public bool IsNew { get; set; } = true;

        public DataTable Data { get; set; } = null;

        public DataView View { get; set; } = null;

        public bool IsEmpty => Data == null;

        public bool HasColumns => Columns.Count > 0;

        public void AddColumn(FieldData field)
        {
            Columns.Add(new TableStorageColumnData() { Field = field });
        }

        public void Reset()
        {
            if (View != null)
                View.RowFilter = string.Empty;
        }
    }
}
