using Core.Data.Field;
using Core.Data.Table;
using Core.Helper;
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
    public partial class FormEditFieldProperty : Form
    {
        private TableData _tableData;

        public FormEditFieldProperty(FieldType[] acceptedTypes)
        {
            InitializeComponent();
            AcceptedTypes = acceptedTypes;
        }

        public FieldType[] AcceptedTypes { get; set; }

        public TableData TableData
        {
            get
            {
                return _tableData;
            }
            set
            {
                _tableData = value;
                Text = $"Выбор поля в таблице {_tableData.Name}";
                _tableData.Fields.Where(fld => AcceptedTypes.Contains(fld.Type)).ForEach(fld => cmbFields.Items.Add(fld));
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        public FieldData SelectedField { get => cmbFields.SelectedItem as FieldData; set => cmbFields.SelectedItem = value; }

        private void btnReset_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Ignore;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
