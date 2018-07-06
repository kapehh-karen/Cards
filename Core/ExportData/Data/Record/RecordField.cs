using Core.ExportData.Data.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Core.ExportData.Data.Record
{
    public class RecordField : IRecordReader
    {
        public FieldToken Token { get; set; }

        public object Value { get; set; }

        public void Process(SqlDataReader reader)
        {
            Value = reader[Token.InternalName];
        }
    }
}
