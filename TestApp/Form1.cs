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

            foreach (System.Data.DataRow row in table.Rows)
            {
                foreach (System.Data.DataColumn col in table.Columns)
                {
                    listBox1.Items.Add($"{col.ColumnName} = {row[col]}");
                }
                listBox1.Items.Add("============================");
            }

            conn.Close();
        }
    }
}
