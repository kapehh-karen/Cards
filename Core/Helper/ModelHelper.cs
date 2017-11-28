using Core.Connection;
using Core.Data.Base;
using Core.Data.Table;
using Core.Notification;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Core.Helper
{
    public static class ModelHelper
    {
        public static bool Delete(DataBase dataBase, TableData table, object id)
        {
            bool ret = false;

            // Make SQL request
            using (var dbc = new SQLServerConnection(dataBase))
            {
                var connection = dbc.Connection;
                var transaction = connection.BeginTransaction();

                try
                {
                    SqlModelHelper.DeleteFull(connection, transaction, table, id);
                    transaction.Commit();
                    ret = true;
                    NotificationMessage.Info("Удалено!");
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    NotificationMessage.Error($"Ошибка при удалении:\r\n\r\n{ex.Message}");
                }

                transaction.Dispose();
            }

            return ret;
        }
    }
}
