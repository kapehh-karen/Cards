using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Core.Data.Table;
using System.Data.OleDb;
using Core.DataBase;
using System.Data;
using Core.Data.Field;

namespace Core.Data.Config
{
    public class DataBaseConfig
    {
        private string configFileName;
        private string fileBaseName;

        public DataBaseConfig(string fileBaseName)
        {
            this.configFileName = fileBaseName + ".conf";
            this.fileBaseName = fileBaseName;

            if (File.Exists(configFileName))
            {
                this.LoadFromConfig();
                this.LoadFromBase(true);
            }
            else
            {
                this.LoadFromBase();
            }
        }

        private void LoadFromConfig()
        {
            // TODO
        }

        private void LoadFromBase(bool append = false)
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
                            Name = row["TABLE_NAME"].ToString(),
                            Fields = new List<FieldData>()
                        };
                        this.Tables.Add(tableData);

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

        public List<TableData> Tables { get; set; } = new List<TableData>();
    }
}
