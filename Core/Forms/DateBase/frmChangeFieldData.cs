using Core.Data.Field;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Forms.DateBase
{
    public partial class frmChangeFieldData : Form
    {
        public frmChangeFieldData()
        {
            InitializeComponent();

            cmbFieldType.Items.Add(FieldType.TEXT);
            cmbFieldType.Items.Add(FieldType.NUMBER);
            cmbFieldType.Items.Add(FieldType.DATE);
            cmbFieldType.Items.Add(FieldType.BIND);
        }

        public FieldData Field { get; set; }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void frmChangeFieldData_Load(object sender, EventArgs e)
        {
            this.Text = $"Поле - {Field.Name}";
            this.cmbFieldType.SelectedItem = Field.Type;
            this.chkVisible.Checked = Field.Visible;
            this.chkRequire.Checked = Field.Required;
        }
    }
}
