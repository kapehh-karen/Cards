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

        private TableData table = null;
        public TableData Table
        {
            get => table;
            set
            {
                table = value;
                if (table == null)
                    return;

                table.Fields.OrderBy(field => field.DisplayName)
                    .ForEach(field => lstFields.Items.Add(new ListBoxFieldItem() { Field = field }));
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }
    }
}
