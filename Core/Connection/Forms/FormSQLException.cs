using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Connection.Forms
{
    public partial class FormSQLException : Form
    {
        public FormSQLException()
        {
            InitializeComponent();
        }

        private SqlException ex;
        public SqlException Error
        {
            get => ex;
            set
            {
                ex = value;
                txtErrorMessage.Text = $"{ex.Message}\r\n\r\nStackTrace:\r\n{ex.StackTrace}";
            }
        }

        private void btnRetry_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Retry;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
