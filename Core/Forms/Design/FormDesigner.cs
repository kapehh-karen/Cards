using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Forms.Design
{
    public partial class FormDesigner : Form
    {
        private FormEmpty frmEmpty = new FormEmpty();

        public FormDesigner()
        {
            InitializeComponent();
        }

        private void FormDesigner_Load(object sender, EventArgs e)
        {
            frmEmpty.MdiParent = this;
            frmEmpty.Location = new Point(0, 0);
            frmEmpty.Show();
        }
    }
}
