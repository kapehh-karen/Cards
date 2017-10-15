using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace Core.Connection
{
    public class DataBaseConnection : IDisposable
    {
        private OleDbConnection conn;

        public DataBaseConnection(string fileDBName)
        {
            conn = new OleDbConnection()
            {
                ConnectionString = $@"Provider='Microsoft.Jet.OLEDB.4.0';Data Source='{fileDBName}'"
            };
            conn.Open();
        }

        public OleDbConnection Connection => conn;

        public void Dispose()
        {
            conn.Close();
        }
    }
}
