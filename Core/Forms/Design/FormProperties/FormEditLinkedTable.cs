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
    public partial class FormEditLinkedTable : Form
    {
        private TableData _tableData;

        public FormEditLinkedTable()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
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
                Text = $"Выбор внешних данных таблицы {_tableData.Name}";
                _tableData.LinkedTables.ForEach(fld => cmbLinkedTables.Items.Add(fld));
            }
        }

        public LinkedTable SelectedLinkedTable { get => cmbLinkedTables.SelectedItem as LinkedTable; set => cmbLinkedTables.SelectedItem = value; }
    }
}
