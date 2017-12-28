using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Core.Storage.Tables.TableStorageData
{
    [DataContract]
    public class TableStorageSortData
    {
        [DataMember]
        public TableStorageColumnData SortedColumn { get; set; }

        [DataMember]
        public SortDirection Direction { get; set; }

        public bool Exists => SortedColumn != null;

        public void Reset()
        {
            SortedColumn = null;
            Direction = SortDirection.None;
        }
    }
}
