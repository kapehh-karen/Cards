using Core.Data.Config;
using Core.Data.Field;
using Core.Data.Table;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;

namespace Core.DataBase
{
    public class DataBaseConfigLoader
    {
        private string configFileName;
        private string fileBaseName;

        public DataBaseConfigLoader(string fileBaseName)
        {
            this.configFileName = fileBaseName + ".conf";
            this.fileBaseName = fileBaseName;
        }

        public Data.Config.DataBase Load()
        {
            var dbc = new Data.Config.DataBase();

            if (File.Exists(configFileName))
            {
                this.LoadFromConfig(dbc);
                this.LoadFromBase(dbc, true);
            }
            else
            {
                this.LoadFromBase(dbc);
            }

            return dbc;
        }

        public void Save()
        {
            // TODO
        }

        private void LoadFromConfig(Data.Config.DataBase dataBaseConfig)
        {
            // TODO
        }

        private void LoadFromBase(Data.Config.DataBase dataBaseConfig, bool append = false)
        {
            using (var dbc = new DataBaseConnection(fileBaseName))
            {
                var conn = dbc.Connection;

                if (append)
                {
                    // TODO
                }
                else
                {
                    DataTable table = conn.GetSchema("Tables");

                    foreach (DataRow row in table.Rows)
                    {
                        // skip system tables
                        if (!"TABLE".Equals(row["TABLE_TYPE"]))
                            continue;

                        var tableData = new TableData()
                        {
                            Name = row["TABLE_NAME"].ToString()
                        };
                        dataBaseConfig.Tables.Add(tableData);

                        var dataTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, row["TABLE_NAME"] });
                        foreach (DataRow dtRow in dataTable.Rows)
                        {
                            var fieldData = new FieldData()
                            {
                                Name = dtRow["COLUMN_NAME"].ToString()
                            };

                            tableData.Fields.Add(fieldData);
                        }
                    }
                }
            }
        }
    }
}
