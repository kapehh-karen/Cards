using Core.Data.Field;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Storage.Tables.TableStorageData
{
    public class TableStorageColumnData
    {
        public FieldData Field { get; set; } = null;

        public int Width { get; set; } = 100;

        public int Order { get; set; } = 0;
    }
}
