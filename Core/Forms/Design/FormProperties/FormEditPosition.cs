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
    public partial class FormEditPosition : Form
    {
        public FormEditPosition()
        {
            InitializeComponent();
        }

        public Point EnteredPosition
        {
            get => new Point((int)numericX.Value, (int)numericY.Value);
            set
            {
                numericX.Value = value.X;
                numericY.Value = value.Y;
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
