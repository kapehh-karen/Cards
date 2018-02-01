using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Core.Filter.Data;

namespace Core.Filter.Controls
{
    public partial class InputOperand : UserControl
    {
        public InputOperand()
        {
            InitializeComponent();
        }

        public FilterData FilterData { get; set; }

        private Control inputControl;
        public Control InputControl
        {
            get => inputControl;
            set
            {
                if (inputControl != null)
                {
                    inputControl.Dispose();
                    inputControl = null;
                }

                if (value == null)
                    return;

                inputControl = value;
                panel1.Controls.Add(inputControl);
                inputControl.Dock = DockStyle.Fill;
            }
        }

        private void btnSelectInput_Click(object sender, EventArgs e)
        {
            contextMenuStripInput.Show(btnSelectInput, 0, 0);
        }

        private void constToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputControl = new InputValue();
        }

        private void fieldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputControl = new InputField() { FilterData = FilterData };
        }

        private void subqueryToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
