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

namespace Core.Common.Fields
{
    public partial class FormPopupSearchField : Form
    {
        private class FieldItem
        {
            public FieldItem(FieldData field)
            {
                Field = field;
            }

            public FieldData Field { get; set; }

            public override string ToString()
            {
                return Field.DisplayName;
            }
        }

        public event EventHandler FieldSelected = (s, e) => { };

        public FormPopupSearchField()
        {
            InitializeComponent();
        }

        private void FormPopupSearchField_Load(object sender, EventArgs e)
        {

        }

        private TableData table;
        public TableData Table
        {
            get => table;
            set
            {
                table = value;
                if (table != null)
                {
                    Fields = table.Fields.Select(it => new FieldItem(it)).ToList();
                    lstFields.Items.Clear();
                    Fields.ForEach(it => lstFields.Items.Add(it));
                }
            }
        }
        
        private List<FieldItem> Fields { get; set; }

        public FieldData SelectedField { get; set; }

        private void txtSearchField_TextChanged(object sender, EventArgs e)
        {
            lstFields.BeginUpdate();
            lstFields.Items.Clear();
            Fields.Where(it => it.Field.DisplayName.IndexOf(txtSearchField.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                .ForEach(it => lstFields.Items.Add(it));
            lstFields.EndUpdate();
        }

        private void lstFields_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DoSelect();
        }

        private void FormPopupSearchField_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DoSelect()
        {
            FieldItem fieldItem = null;

            if (lstFields.SelectedItem != null)
            {
                fieldItem = lstFields.SelectedItem as FieldItem;
            }
            else if (lstFields.Items.Count == 1) // Если остался только один элемент, то пикнем его
            {
                fieldItem = lstFields.Items[0] as FieldItem;
            }

            if (fieldItem != null)
            {
                this.Close();
                SelectedField = fieldItem.Field;
                FieldSelected(this, EventArgs.Empty);
            }
        }

        private void txtSearchField_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.ActiveControl = lstFields;
                lstFields.Focus();
                SendKeys.Send($"{{{e.KeyCode}}}");
            }
        }

        private void lstFields_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.ActiveControl = txtSearchField;
            txtSearchField.Focus();
            SendKeys.Send(e.KeyChar.ToString());
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            if (ModifierKeys == Keys.None && keyData == Keys.Enter)
            {
                DoSelect();
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }
    }
}
