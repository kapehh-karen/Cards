using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestApp
{
    public partial class frmTestUserControls : Form
    {
        public frmTestUserControls()
        {
            InitializeComponent();
        }

        private void button4_MouseClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("TEST");
        }

        private void button1_PressedButton(object sender, EventArgs e)
        {
            MessageBox.Show("KEK");
        }
    }
}
