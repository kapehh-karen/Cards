using Core.Data.Base;
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

namespace Core.Forms.DateBase
{
    public partial class FormChangeLinkedTable : Form
    {
        public FormChangeLinkedTable()
        {
            InitializeComponent();
        }

        public LinkedTable BindData { get; set; }

        public DataBase Base { get; set; }

        public TableData SelectedTable => cmbTables.SelectedItem as TableData;

        public FieldData SelectedField => cmbFields.SelectedItem as FieldData;

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (SelectedTable == null || SelectedField == null)
            {
                MessageBox.Show("Обязательно требуется выбрать таблицу и поле", Consts.ProgramTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult = DialogResult.OK;
        }

        private void frmChangeLinkedTable_Load(object sender, EventArgs e)
        {
            Base.Tables.ForEach(td => cmbTables.Items.Add(td));

            if (BindData != null)
            {
                Text = "Изменение существующей связи";

                cmbTables.SelectedItem = BindData.Table;
                cmbFields.SelectedItem = BindData.Field;
            }
            else
            {
                Text = "Создание новой связи";
            }
        }

        private void cmbTables_SelectedValueChanged(object sender, EventArgs e)
        {
            if ((sender as ComboBox).SelectedItem is TableData item)
            {
                cmbFields.Items.Clear();
                item.Fields.ForEach(fd => cmbFields.Items.Add(fd));
            }
        }
    }
}
