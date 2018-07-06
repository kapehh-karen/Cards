using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Core.ExportData.Data.Record
{
    public class RecordItem : IRecordReader
    {
        public List<RecordField> Fields { get; } = new List<RecordField>();

        public List<RecordTable> Tables { get; } = new List<RecordTable>();

        public bool IsEmpty { get; set; }

        public void Process(SqlDataReader reader)
        {
            if (IsEmpty)
            {
                Fields.ForEach(it => it.Process(reader));
                IsEmpty = false;
            }
            Tables.ForEach(it => it.Process(reader));
        }
    }
}
