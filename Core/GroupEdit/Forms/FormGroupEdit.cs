using Core.Data.Field;
using Core.Data.Table;
using Core.GroupEdit.Controls;
using Core.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.GroupEdit.Forms
{
    public partial class FormGroupEdit : Form
    {
        private class ListBoxFieldItem
        {
            public FieldData Field { get; set; }
            public override string ToString() => Field.DisplayName;
        }

        public FormGroupEdit()
        {
            InitializeComponent();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        private TableData table = null;
        public TableData Table
        {
            get => table;
            set
            {
                table = value;
                if (table == null)
                    return;

                table.Fields.ForEach(field => lstFields.Items.Add(new ListBoxFieldItem() { Field = field }));
                lstFields.Sorted = true;
            }
        }
        
        private void btnAdd_Click(object sender, EventArgs e)
        {
            lstFields.SelectedItems
                .Cast<ListBoxFieldItem>()
                .ToArray()
                .ForEach(item =>
                {
                    var control = new ItemFieldValue() { Field = item.Field };
                    control.ItemDelete += Control_ItemDelete;
                    panelContainer.Controls.Add(control);
                    lstFields.Items.Remove(item);
                });
        }

        private void Control_ItemDelete(object sender, EventArgs e)
        {
            var control = sender as ItemFieldValue;
            var item = new ListBoxFieldItem() { Field = control.Field };
            lstFields.Items.Add(item);
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            var newValues = panelContainer.Controls
                .Cast<ItemFieldValue>()
                .ToDictionary(control => control.Field, control => control.Value);

            // TODO: With WaitDialog

            DialogResult = DialogResult.OK;
        }
    }
}
