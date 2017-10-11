using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Forms.Design.FormProperties
{
    public partial class FormEditNumber : Form
    {
        public FormEditNumber()
        {
            InitializeComponent();
        }

        public decimal EnteredNumber { get => numericUpDown1.Value; set => numericUpDown1.Value = value; }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
