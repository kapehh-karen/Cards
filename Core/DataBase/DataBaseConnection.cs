using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace Core.DataBase
{
    public class DataBaseConnection : IDisposable
    {
        private OleDbConnection conn;

        public DataBaseConnection(string fileDBName)
        {
            conn = new OleDbConnection();
            conn.ConnectionString = $@"Provider='Microsoft.Jet.OLEDB.4.0';Data Source='{fileDBName}'";
        }

        public OleDbConnection Connection => conn;

        public void Dispose()
        {
            conn.Close();
        }
    }
}
