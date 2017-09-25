using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Core.Forms.DateBase
{
    public partial class frmBindSetting : Form
    {
        public frmBindSetting()
        {
            InitializeComponent();
        }

        private void frmBindSetting_Load(object sender, EventArgs e)
        {
            foreach (var fileName in Directory.GetFiles(".").Where(fname => Path.GetExtension(fname).Equals(".mdb")))
            {
                cmbBasesList.Items.Add(fileName);
            }
        }
    }
}
