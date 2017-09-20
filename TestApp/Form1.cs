using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestApp
{
    public partial class Form1 : Form
    {
        public Form1()
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
            OleDbConnection conn = new OleDbConnection();

            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=test_db.mdb";

            conn.Open();
            // Insert code to process data.
            DataTable table = conn.GetSchema("Tables");

            foreach (DataRow row in table.Rows)
            {
                //if (!"TABLE".Equals(row["TABLE_TYPE"]))
                //    continue;

                listBox1.Items.Add("============================");

                foreach (DataColumn col in table.Columns)
                {
                    listBox1.Items.Add($"{col.ColumnName} = {row[col]}");
                }

                listBox1.Items.Add("COLUMNS:");

                var dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, row["TABLE_NAME"] });
                foreach (DataRow rrr in dt.Rows)
                {
                    listBox1.Items.Add($"    {rrr}:");
                    foreach (DataColumn ccc in dt.Columns)
                    {
                        listBox1.Items.Add($"        {ccc.ColumnName} = {rrr[ccc]}");
                    }
                }

                try
                {
                    listBox1.Items.Add("ROWS:");

                    var cmd = new OleDbCommand($"SELECT * FROM {row["TABLE_NAME"]}", conn);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            for (var i = 0; i < reader.FieldCount; i++)
                            {
                                listBox1.Items.Add($"    {reader.GetName(i)} = {reader[i]}");
                            }
                            listBox1.Items.Add("    ---");
                        }
                    }
                }
                catch (Exception)
                {

                }

                listBox1.Items.Add("============================");
            }

            conn.Close();
        }
    }
}
