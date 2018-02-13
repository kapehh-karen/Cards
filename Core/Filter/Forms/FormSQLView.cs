using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Filter.Forms
{
    public partial class FormSQLView : Form
    {
        public FormSQLView()
        {
            InitializeComponent();
        }

        public string SQL { get => txtSQL.Text; set => txtSQL.Text = value; }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(SQL);
        }
    }
}
