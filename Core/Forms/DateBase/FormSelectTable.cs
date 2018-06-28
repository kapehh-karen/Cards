using Core.Data.Base;
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

namespace Core.Forms.DateBase
{
    public partial class FormSelectTable : Form
    {
        public FormSelectTable()
        {
            InitializeComponent();
        }

        private void FormSelectTable_Load(object sender, EventArgs e)
        {

        }

        public DataBase Base
        {
            set
            {
                if (value != null)
                    value.Tables.OrderBy(it => it.Name).ForEach(it => cmbTables.Items.Add(it));
            }
        }

        public TableData SelectedTable => cmbTables.SelectedItem as TableData;
    }
}
