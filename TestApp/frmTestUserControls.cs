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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            inputValue2.Type = Core.Data.Field.FieldType.TEXT;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            inputValue2.Type = Core.Data.Field.FieldType.DATE;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            inputValue2.Type = Core.Data.Field.FieldType.BOOLEAN;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"{inputValue2.Value}");
        }
    }
}
