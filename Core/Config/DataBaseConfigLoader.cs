using Core.Config;
using Core.Connection;
using Core.Data.Base;
using Core.Data.Field;
using Core.Data.Table;
using Core.Notification;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;

namespace Core.Config
{
    public class DataBaseConfigLoader
    {
        private string configFileName;
        private string fileBaseName;
        private Configuration<DataBase> config;

        public DataBaseConfigLoader(string fileBaseName)
        {
            this.configFileName = fileBaseName + ".conf";
            this.fileBaseName = fileBaseName;
            this.config = new Configuration<DataBase>();
        }

        public DataBase Load()
        {
            DataBase dbConfig = null;

            // load configuration if exists
            if (File.Exists(configFileName))
            {
                dbConfig = this.LoadFromConfig();
                NotificationMessage.SystemInfo($"Конфигурация для базы \"{fileBaseName}\" успешно загружена из файла \"{configFileName}\"");
            }
            else
            {
                NotificationMessage.SystemInfo($"Конфигурация для базы \"{fileBaseName}\" не найдена");
            }

            return this.LoadFromBase(dbConfig);
        }

        public void Save(DataBase dbc)
        {
            this.config.WriteToFile(dbc, this.configFileName);
        }

        private DataBase LoadFromConfig()
        {
            return this.config.ReadFromFile(this.configFileName);
        }

        private DataBase LoadFromBase(DataBase dataBaseConfig)
        {
            var dataBase = dataBaseConfig ?? new DataBase();

            using (var dbc = new DataBaseConnection(fileBaseName))
            {
                var conn = dbc.Connection;
                DataTable table = conn.GetSchema("Tables");

                var tableNames = new List<string>();
                var fieldNames = new List<string>();

                foreach (DataRow row in table.Rows)
                {
                    // skip system tables
                    if (!"TABLE".Equals(row["TABLE_TYPE"]))
                        continue;

                    var tableName = row["TABLE_NAME"].ToString();
                    var tableInConfig = dataBase.Tables.FirstOrDefault(td => td.Name == tableName);
                    var tableData = tableInConfig ?? new TableData() { Name = tableName };
                    tableNames.Add(tableName);

                    // if table not exists in config
                    if (tableInConfig == null)
                    {
                        dataBase.Tables.Add(tableData);
                    }

                    var dataTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, tableName });
                    foreach (DataRow dtRow in dataTable.Rows)
                    {
                        var fieldName = dtRow["COLUMN_NAME"].ToString();
                        var fieldInTable = tableData.Fields.FirstOrDefault(fd => fd.Name == fieldName);
                        var fieldData = fieldInTable ?? new FieldData() { Name = fieldName };
                        fieldNames.Add(fieldName);

                        // if field NOT exists in table
                        if (fieldInTable == null)
                        {
                            tableData.Fields.Add(fieldData);
                        }
                    }

                    // filter fields
                    tableData.Fields = tableData.Fields.Where(fd => fieldNames.Contains(fd.Name)).ToList();
                }

                // filter tables
                dataBase.Tables = dataBase.Tables.Where(td => tableNames.Contains(td.Name)).ToList();
            }

            return dataBase;
        }
    }
}
