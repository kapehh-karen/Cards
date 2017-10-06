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
    public partial class FormEditSize : Form
    {
        public FormEditSize()
        {
            InitializeComponent();
        }

        public Size EnteredSize
        {
            get => new Size((int)numericWidth.Value, (int)numericHeight.Value);
            set
            {
                numericWidth.Value = value.Width;
                numericHeight.Value = value.Height;
            }
        }

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
