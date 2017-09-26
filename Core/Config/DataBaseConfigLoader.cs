using Core.Config;
using Core.Connection;
using Core.Data.Base;
using Core.Data.Field;
using Core.Data.Table;
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
            DataBase dbc;
            if (File.Exists(configFileName))
            {
                dbc = this.LoadFromConfig();
                this.LoadFromBase(dbc, true);
            }
            else
            {
                dbc = new DataBase();
                this.LoadFromBase(dbc);
            }
            return dbc;
        }

        public void Save(DataBase dbc)
        {
            this.config.WriteToFile(dbc, this.configFileName);
        }

        private DataBase LoadFromConfig()
        {
            return this.config.ReadFromFile(this.configFileName);
        }

        private void LoadFromBase(DataBase dataBaseConfig, bool append = false)
        {
            using (var dbc = new DataBaseConnection(fileBaseName))
            {
                var conn = dbc.Connection;

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

                    // add table
                    dataBaseConfig.Tables.Add(tableData);

                    var dataTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, row["TABLE_NAME"] });
                    foreach (DataRow dtRow in dataTable.Rows)
                    {
                        var fieldData = new FieldData()
                        {
                            Name = dtRow["COLUMN_NAME"].ToString()
                        };

                        // add field in table
                        tableData.Fields.Add(fieldData);
                    }
                }

                if (append)
                {
                    // TODO
                }
            }
        }
    }
}
