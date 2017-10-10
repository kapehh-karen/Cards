using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static Core.Forms.Design.FormProperties.PropertiesEvent;

namespace Core.Forms.Design.FormProperties
{
    public partial class FormEditText : Form
    {
        public event PropertyEvent ChangingText;

        public FormEditText()
        {
            InitializeComponent();
        }

        public string EnteredText { get => txtText.Text; set => txtText.Text = value; }

        private void btnChange_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void txtText_TextChanged(object sender, EventArgs e)
        {
            ChangingText?.Invoke(txtText.Text);
        }
    }
}
