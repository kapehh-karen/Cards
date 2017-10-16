using Core.Notification;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Core.Connection
{
    public class SQLServerConnection : IDisposable
    {
        private SqlConnection conn;
        private bool connected;

        public SQLServerConnection(string server, int port, string user, string pass, string basename)
        {
            conn = new SqlConnection()
            {
                ConnectionString = $@"server='tcp:{server},{port}'; uid='{user}'; pwd='{pass}'; database='{basename}'; Connection Timeout=5"
            };

            try
            {
                conn.Open();
                connected = true;
            }
            catch (Exception e)
            {
                NotificationMessage.Error($"Произошла ошибка при соединении с базой данных: {e.Message}", e);
                connected = false;
            }
        }

        public SqlConnection Connection => conn;

        public void Dispose()
        {
            if (connected)
                conn.Close();
        }
    }
}
