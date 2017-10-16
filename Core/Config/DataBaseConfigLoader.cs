using Core.Config;
using Core.Connection;
using Core.Data.Base;
using Core.Data.Design.Controls;
using Core.Data.Design.InternalData;
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
    public class DataBaseConfigLoader
    {
        //private string configFileName;
        private string fileBaseName;
        private Configuration<DataBase> config;

        public DataBaseConfigLoader(string fileBaseName)
        {
            //this.configFileName = $"{Path.GetDirectoryName(fileBaseName)}\\{Path.GetFileNameWithoutExtension(fileBaseName)}.conf";
            this.fileBaseName = fileBaseName;
            this.config = new Configuration<DataBase>();
        }

        public DataBase Load(DataBase fromDataBaseConfig = null)
        {
            DataBase dbConfig = fromDataBaseConfig;

            if (fromDataBaseConfig == null)
            {
                // load configuration if exists
                if (File.Exists(this.fileBaseName))
                {
                    dbConfig = this.LoadFromConfig();
                    NotificationMessage.SystemInfo($"Конфигурация базы \"{fileBaseName}\" успешно загружена");
                }
                else
                {
                    NotificationMessage.SystemInfo($"Конфигурация базы \"{fileBaseName}\" не найдена");
                }
            }

            return this.LoadFromBase(dbConfig);
        }

        public void Save(DataBase dbc)
        {
            this.config.WriteToFile(dbc, this.fileBaseName);
            NotificationMessage.SystemInfo($"Конфигурация базы \"{fileBaseName}\" успешно сохранена");
        }

        private DataBase LoadFromConfig()
        {
            return this.config.ReadFromFile(this.fileBaseName);
        }

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

                // filter fields
                tableData.Fields = tableData.Fields.Where(fd => fieldNames.Contains(fd.Name)).ToList();
            }

            dbc.Dispose();

            // filter tables
            dataBase.Tables = dataBase.Tables.Where(td => tableNames.Contains(td.Name)).ToList();
            
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

            return dataBase;
        }

        private void CleanupProperties(TableData tableData, List<ControlData> Controls)
        {
            Controls.ForEach(ctl =>
            {
                // Properties cleanup
                ctl.Properties.ForEach(p =>
                {
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
