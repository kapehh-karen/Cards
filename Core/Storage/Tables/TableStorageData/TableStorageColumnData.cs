using Core.Data.Field;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Storage.Tables.TableStorageData
{
    public class TableStorageColumnData
    {
        public FieldData Field { get; set; }

        public int Width { get; set; }

        public int Order { get; set; }
    }
}
