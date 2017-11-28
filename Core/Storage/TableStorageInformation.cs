using Core.Data.Field;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Core.Storage
{
    public class TableStorageInformation
    {
        public List<FieldData> Fields { get; set; } = new List<FieldData>();

        public DataTable Data { get; set; } = null;

        public DataView View { get; set; } = null;

        public bool IsEmpty => Data == null;

        public bool HasFields => Fields.Count > 0;

        public void Reset()
        {
            if (View != null)
                View.RowFilter = string.Empty;
        }
    }
}
