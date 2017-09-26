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
    public partial class frmChangeFieldData : Form
    {
        private class ComboBoxItem
        {
            public FieldType ItemType { get; set; }

            public string ItemText { get; set; }

            public override string ToString()
            {
                return ItemText;
            }
        }

        public frmChangeFieldData()
        {
            InitializeComponent();
        }

        public DataBase Base { get; set; }

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
            Text = $"Поле - {Field.Name}";
            chkVisible.Checked = Field.Visible;
            chkRequire.Checked = Field.Required;

            ComboBoxItem[] types = new ComboBoxItem[] {
                new ComboBoxItem() { ItemType = FieldType.TEXT, ItemText = "Текст" },
                new ComboBoxItem() { ItemType = FieldType.NUMBER, ItemText = "Число" },
                new ComboBoxItem() { ItemType = FieldType.DATE, ItemText = "Дата" },
                new ComboBoxItem() { ItemType = FieldType.BIND, ItemText = "Связанное поле" }
            };
            cmbFieldType.Items.AddRange(types);
            cmbFieldType.SelectedItem = types.FirstOrDefault(t => t.ItemType == Field.Type);

            cmbTable.Items.Clear();
            Base.Tables.ForEach(td => cmbTable.Items.Add(td));

            if (Field.BindData != null)
            {
                cmbTable.SelectedItem = Field.BindData.Table;
                cmbField.SelectedItem = Field.BindData.Field;
            }
        }

        private void cmbTable_SelectedValueChanged(object sender, EventArgs e)
        {
            if ((sender as ComboBox).SelectedItem is TableData item)
            {
                cmbField.Items.Clear();
                item.Fields.ForEach(td => cmbField.Items.Add(td));
            }
        }

        private void cmbFieldType_SelectedValueChanged(object sender, EventArgs e)
        {
            if ((sender as ComboBox).SelectedItem is ComboBoxItem item)
            {
                gbBindSettings.Enabled = item.ItemType == FieldType.BIND;
            }
        }
    }
}
