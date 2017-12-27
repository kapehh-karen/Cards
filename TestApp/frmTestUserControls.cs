using Core.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
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

            new HighlightFocusedControl(this);
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        
    }
}
