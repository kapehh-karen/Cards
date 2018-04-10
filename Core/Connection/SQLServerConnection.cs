using Core.Connection.Forms;
using Core.Data.Base;
using Core.Notification;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Core.Connection
{
    public class SQLServerConnection : IDisposable
    {
        public static DataBase DefaultDataBase { get; set; }

        private SqlConnection conn;
        private bool connected;

        public SQLServerConnection()
            : this(DefaultDataBase.Sever,
                   DefaultDataBase.Port,
                   DefaultDataBase.UserName,
                   DefaultDataBase.Password,
                   DefaultDataBase.BaseName)
        {
        }

        public SQLServerConnection(DataBase dataBase)
            : this(dataBase.Sever,
                   dataBase.Port,
                   dataBase.UserName,
                   dataBase.Password,
                   dataBase.BaseName)
        {
        }

        public SQLServerConnection(string server, int port, string user, string pass, string basename)
        {
            bool retry;

            do
            {
                retry = false;

                conn = new SqlConnection()
                {
                    ConnectionString = $@"server='tcp:{server},{port}'; uid='{user}'; pwd='{pass}'; database='{basename}'; Connection Timeout=5"
                };

                try
                {
                    conn.Open();
                    connected = true;
                }
                catch (SqlException e)
                {
                    connected = false;
                    DoDispose();
                    using (var dialog = new FormSQLException() { Error = e })
                    {
                        retry = dialog.ShowDialog() == DialogResult.Retry;
                        if (retry)
                            Thread.Sleep(300);
                    }
                }
            }
            while (retry);
        }

        public SqlConnection Connection
        {
            get
            {
                if (!connected)
                    throw new InvalidOperationException("Соединение отсутствует. Дальнейшая работа невозможна.");

                return conn;
            }
        }

        private void DoDispose()
        {
            conn.Close();
            conn.Dispose();
            conn = null;
        }

        public void Dispose()
        {
            if (connected)
                DoDispose();
        }
    }
}
