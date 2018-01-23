using Core.Data.Field;
using Core.Data.Table;
using Core.Storage.Tables.TableStorageData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Core.Storage.Tables
{
    [DataContract]
    public class TableStorageInformation
    {
        [DataMember]
        public List<TableStorageColumnData> Columns { get; set; } = new List<TableStorageColumnData>();

        [DataMember]
        public TableStorageSortData SortData { get; set; } = new TableStorageSortData();

        #region Non Serializable Part

        public bool HasColumns => Columns.Count > 0;

        public void AddColumn(FieldData field)
        {
            Columns.Add(new TableStorageColumnData()
            {
                Field = field,
                Order = Columns.Count
            });
        }

        public TableData Table { get; set; } = null;

        /// <summary>
        /// Полностью новая информация, не сохранена на диске
        /// </summary>
        public bool IsNew { get; set; } = false;

        public DataTable Data { get; set; } = null;

        public bool HasData => Data != null;

        #endregion
    }
}
