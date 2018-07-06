using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Core.ExportData.Data.Record
{
    public interface IRecordReader
    {
        void Process(SqlDataReader reader);
    }
}
