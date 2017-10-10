using Core.Data.Field;
using Core.Data.Table;
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

        public FormEditFieldProperty()
        {
            InitializeComponent();
        }

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
                _tableData.Fields.ForEach(fld => cmbFields.Items.Add(fld));
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        public FieldData SelectedField { get => cmbFields.SelectedItem as FieldData; set => cmbFields.SelectedItem = value; }
    }
}
