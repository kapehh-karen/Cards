using Core.Config;
using Core.Connection;
using Core.Data.Base;
using Core.Data.Design.Controls;
using Core.Data.Design.InternalData;
using Core.Data.Design.Properties.ControlProperties;
using Core.Data.Field;
using Core.Data.Table;
using Core.Helper;
using Core.Notification;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace Core.Config
{
    public class DataBaseLoader
    {
        public DataBaseLoader(DataBase dataBase)
        {
            RuntimeBase = LoadFromBase(dataBase);
        }

        public DataBase RuntimeBase { get; set; }

        private DataBase LoadFromBase(DataBase dataBaseConfig)
        {
            var dataBase = dataBaseConfig ?? new DataBase();
            
            if (string.IsNullOrEmpty(dataBaseConfig?.Sever)
                || string.IsNullOrEmpty(dataBaseConfig?.UserName)
                || string.IsNullOrEmpty(dataBaseConfig?.Password)
                || string.IsNullOrEmpty(dataBaseConfig?.BaseName))
            {
                dataBase.IsConnected = false;
                return dataBase;
            }

            var dbc = WaitDialog.Run("Подождите, идет подключение к SQL Server",
                                     () => new SQLServerConnection(dataBaseConfig?.Sever,
                                                                   dataBaseConfig?.Port ?? 0,
                                                                   dataBaseConfig?.UserName,
                                                                   dataBaseConfig?.Password,
                                                                   dataBaseConfig?.BaseName));

            var conn = dbc.Connection;

            if (conn.State == ConnectionState.Closed)
            {
                dataBase.IsConnected = false;
                return dataBase;
            }
            else
            {
                dataBase.IsConnected = true;
            }

            DataTable table = conn.GetSchema("Tables");

            var tableNames = new List<string>();
            var fieldNames = new List<string>();

            foreach (DataRow row in table.Rows)
            {
                var tableName = row["TABLE_NAME"].ToString();
                var tableInConfig = dataBase.Tables.FirstOrDefault(td => td.Name == tableName);
                var tableData = tableInConfig ?? new TableData() { Name = tableName };
                tableNames.Add(tableName);

                // if table not exists in config
                if (tableInConfig == null)
                {
                    dataBase.Tables.Add(tableData);
                }

                // Очищаем список полей для текущей таблицы
                fieldNames.Clear();

                using (SqlCommand command = new SqlCommand($"SELECT TOP 0 [{tableName}].* FROM [{tableName}] WHERE 1 = 2", conn))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        // This will return false - we don't care, we just want to make sure the schema table is there.
                        reader.Read();

                        var tableSchema = reader.GetSchemaTable();

                        // Each row in the table schema describes a column
                        foreach (DataRow rowColumn in tableSchema.Rows)
                        {
                            var fieldName = rowColumn["ColumnName"].ToString();
                            var fieldInTable = tableData.Fields.FirstOrDefault(fd => fd.Name == fieldName);
                            var fieldData = fieldInTable ?? new FieldData() { Name = fieldName };
                            fieldNames.Add(fieldName);

                            // if field NOT exists in table
                            if (fieldInTable == null)
                            {
                                tableData.Fields.Add(fieldData);
                            }
                        }
                    }
                }

                // Оставляем только те поля, которые получены в схеме таблицы
                tableData.Fields = tableData.Fields.Where(fd => fieldNames.Contains(fd.Name)).ToList();
            }
            
            // Оставляем только те таблицы, которые есть в базе
            dataBase.Tables = dataBase.Tables.Where(td => tableNames.Contains(td.Name)).ToList();

            // Close connection
            dbc.Dispose();

            BaseCleanupProperties(dataBase);

            return dataBase;
        }

        private void BaseCleanupProperties(DataBase dataBase)
        {
            // cleanup removed tables and fields in BindData, LinkedTable, FormData
            dataBase.Tables.ForEach(td =>
            {
                foreach (var fd in td.Fields.Where(fd => fd.Type == FieldType.BIND))
                {
                    if (!dataBase.Tables.Contains(fd.BindData.Table))
                    {
                        fd.BindData.Table = null;
                        fd.BindData.Field = null;
                    }
                    else if (!fd.BindData.Table.Fields.Contains(fd.BindData.Field))
                    {
                        fd.BindData.Field = null;
                    }
                }

                foreach (var lt in td.LinkedTables)
                {
                    if (!dataBase.Tables.Contains(lt.Table))
                    {
                        lt.Table = null;
                        lt.Field = null;
                    }
                    else if (!lt.Table.Fields.Contains(lt.Field))
                    {
                        lt.Field = null;
                    }
                }

                td.Form?.Pages.ForEach(page => CleanupProperties(td, page.Controls));
            });
        }

        private void CleanupProperties(TableData tableData, List<ControlData> Controls)
        {
            Controls.ForEach(ctl =>
            {
                // Properties cleanup
                ctl.Properties.ForEach(p =>
                {
                    // TODO: Изменить метод обнуления значения не по типу значения, а по типу проперти
                    if (p.Value is FieldData field && !tableData.Fields.Contains(field))
                    {
                        p.Value = null;
                    }
                    else if (p.Value is LinkedTable table && !tableData.LinkedTables.Contains(table))
                    {
                        p.Value = null;
                    }
                });

                // In Deepth
                CleanupProperties(tableData, ctl.Chields);
            });
        }
    }
}
