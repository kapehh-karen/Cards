using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Core.Storage
{
    public class TableStorageInformation
    {
        public DataTable Data { get; set; } = null;

        public DataView View { get; set; } = null;

        public bool IsEmpty => Data == null;

        public void Reset()
        {
            if (View != null)
                View.RowFilter = string.Empty;
        }
    }
}
