using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Forms.DateBase
{
    public partial class FormEditConnection : Form
    {
        public FormEditConnection()
        {
            InitializeComponent();
        }

        public string Server { get => txtServer.Text; set => txtServer.Text = value; }

        public int Port { get { int x; int.TryParse(txtPort.Text, out x); return x; } set => txtPort.Text = value.ToString(); }

        public string UserName { get => txtUsername.Text; set => txtUsername.Text = value; }

        public string Password { get => txtPassword.Text; set => txtPassword.Text = value; }

        public string BaseName { get => txtBasename.Text; set => txtBasename.Text = value; }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
