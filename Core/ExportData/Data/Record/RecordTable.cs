using Core.ExportData.Data.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Core.ExportData.Data.Record
{
    public class RecordTable : IRecordReader
    {
        public TableToken Token { get; set; }

        public Dictionary<object, RecordItem> Items { get; } = new Dictionary<object, RecordItem>();

        public void Process(SqlDataReader reader)
        {
            var id = reader[Token.FieldIdToken.InternalName];
            if (id == null)
                return;

            if (Items.ContainsKey(id))
            {
                var existingRecordItem = Items[id];
                existingRecordItem.Process(reader);
            }
            else
            {
                var newRecordItem = Token.CreateRecordItem();
                Items.Add(id, newRecordItem);
                newRecordItem.Process(reader);
            }
        }
    }
}
