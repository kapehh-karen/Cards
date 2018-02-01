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
    public partial class FormCreateTabPage : Form
    {
        private string enteredText;

        public FormCreateTabPage()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            enteredText = txtCaption.Text.Trim();

            if (string.IsNullOrEmpty(enteredText))
            {
                MessageBox.Show("Нельзя создать вкладку с пустым заголовком", Consts.ProgramTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult = DialogResult.OK;
        }

        public string EnteredText { get => enteredText; set => txtCaption.Text = enteredText = value; }
    }
}
