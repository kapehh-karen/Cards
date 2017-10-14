using Core.Forms.DateBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestApp
{
    public partial class frmBDFields : Form
    {
        public frmBDFields()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = @"server=DESKTOP-M4QPOPI\CARDS;uid=root;pwd=123;database=OFFICERS";
            conn.Open();

            // Insert code to process data.
            DataTable table = conn.GetSchema("Tables");

            foreach (DataRow row in table.Rows)
            {
                //if (!"TABLE".Equals(row["TABLE_TYPE"]))
                //    continue;

                string tableName = row["TABLE_NAME"].ToString();

                foreach (DataColumn col in table.Columns)
                {
                    listBox1.Items.Add($"{col.ColumnName} = {row[col]}");
                }

                listBox1.Items.Add("FIELDS:");

                using (SqlCommand command = new SqlCommand($"SELECT TOP 0 [{tableName}].* FROM [{tableName}] WHERE 1 = 2", conn))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        // This will return false - we don't care, we just want to make sure the schema table is there.
                        reader.Read();

                        var tableSchema = reader.GetSchemaTable();

                        /*
                            <string>ColumnName</string>
                            <string>ColumnOrdinal</string>
                            <string>ColumnSize</string>
                            <string>NumericPrecision</string>
                            <string>NumericScale</string>
                            <string>IsUnique</string>
                            <string>IsKey</string>
                            <string>BaseServerName</string>
                            <string>BaseCatalogName</string>
                            <string>BaseColumnName</string>
                            <string>BaseSchemaName</string>
                            <string>BaseTableName</string>
                            <string>DataType</string>
                            <string>AllowDBNull</string>
                            <string>ProviderType</string>
                            <string>IsAliased</string>
                            <string>IsExpression</string>
                            <string>IsIdentity</string>
                            <string>IsAutoIncrement</string>
                            <string>IsRowVersion</string>
                            <string>IsHidden</string>
                            <string>IsLong</string>
                            <string>IsReadOnly</string>
                            <string>ProviderSpecificDataType</string>
                            <string>DataTypeName</string>
                            <string>XmlSchemaCollectionDatabase</string>
                            <string>XmlSchemaCollectionOwningSchema</string>
                            <string>XmlSchemaCollectionName</string>
                            <string>UdtAssemblyQualifiedName</string>
                            <string>NonVersionedProviderType</string>
                            <string>IsColumnSet</string>
                         */

                        // Each row in the table schema describes a column
                        foreach (DataRow rowColumn in tableSchema.Rows)
                        {
                            listBox1.Items.Add($"    {rowColumn["ColumnName"]}");
                        }
                    }
                }

                listBox1.Items.Add("============================");
            }

            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
