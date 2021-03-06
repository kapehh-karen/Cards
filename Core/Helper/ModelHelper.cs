﻿using Core.Connection;
using Core.Data.Base;
using Core.Data.Field;
using Core.Data.Model;
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
        // GET

        public static bool Get(string tableName, object id, out CardModel outModel, FieldData customFieldId = null)
            => Get(SQLServerConnection.DefaultDataBase.Tables.First(it => it.Name.Equals(tableName)), id, out outModel, customFieldId);

        public static bool Get(TableData table, object id, out CardModel outModel, FieldData customFieldId = null)
        {
            bool ret = false;

            if (id == null)
            {
                outModel = null;
                return false;
            }

            using (var dbc = new SQLServerConnection())
            {
                var model = SqlModelHelper.GetById(dbc.Connection, table, id, customFieldId: customFieldId);

                if (model != null)
                {
                    outModel = model;
                    ret = true;
                }
                else
                {
                    outModel = null;
                }
            }

            return ret;
        }

        // SAVE

        public static bool Save(string tableName, CardModel model)
            => Save(SQLServerConnection.DefaultDataBase.Tables.First(it => it.Name.Equals(tableName)), model);

        public static bool Save(TableData table, CardModel model)
        {
            bool ret = false;

            // Make SQL request
            using (var dbc = new SQLServerConnection())
            {
                var connection = dbc.Connection;
                var transaction = connection.BeginTransaction();

                try
                {
                    model.LinkedState = model.IsNew ? ModelLinkedItemState.ADDED : ModelLinkedItemState.CHANGED;
                    SqlModelHelper.Save(connection, transaction, table, model);
                    transaction.Commit();
                    model.ResetStates();
                    ret = true;

                    NotificationMessage.Info("Сохранено!");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    NotificationMessage.Error($"Ошибка при сохранении:\r\n\r\n{ex.Message}\r\n\r\n{ex.StackTrace}");
                }

                transaction.Dispose();
            }

            return ret;
        }

        // DELETE

        public static bool Delete(string tableName, object id)
            => Delete(SQLServerConnection.DefaultDataBase.Tables.First(it => it.Name.Equals(tableName)), id);

        public static bool Delete(TableData table, object id)
        {
            bool ret = false;

            if (id == null)
                return false;

            // Make SQL request
            using (var dbc = new SQLServerConnection())
            {
                var connection = dbc.Connection;
                var transaction = connection.BeginTransaction();

                try
                {
                    SqlModelHelper.Delete(connection, transaction, table, id);
                    transaction.Commit();
                    ret = true;

                    NotificationMessage.Info("Удалено!");
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();

                    NotificationMessage.Error($"Ошибка при удалении:\r\n\r\n{ex.Message}\r\n\r\n{ex.StackTrace}");
                }

                transaction.Dispose();
            }

            return ret;
        }
    }
}
