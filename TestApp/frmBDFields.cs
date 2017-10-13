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
                
                foreach (DataColumn col in table.Columns)
                {
                    listBox1.Items.Add($"{col.ColumnName} = {row[col]}");
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
