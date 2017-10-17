using Core.Config;
using Core.Data.Base;
using Core.Data.Table;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Forms.Main
{
    public partial class FormSelectTable : Form
    {
        public FormSelectTable()
        {
            InitializeComponent();
        }

        public DataBase SelectedDataBase { get; set; }

        public TableData SelectedTableData { get; set; }

        private void FormSelectTable_Load(object sender, EventArgs e)
        {
            foreach (var fileName in Directory.GetFiles("BASE")
                                              .Where(fname => Path.GetExtension(fname).Equals(".cards", StringComparison.CurrentCultureIgnoreCase)))
            {
                cmbConfigs.Items.Add(fileName);
            }
        }

        private void cmbConfigs_SelectedValueChanged(object sender, EventArgs e)
        {
            if (sender is ComboBox combo && combo.SelectedItem != null)
            {
                SelectedDataBase = new Configuration<DataBase>().ReadFromFile(combo.SelectedItem.ToString());

                lbTables.Items.Clear();
                SelectedDataBase?.Tables.ForEach(t => lbTables.Items.Add(t));
            }
        }

        private void btnSelectTable_Click(object sender, EventArgs e)
        {
            if (lbTables.SelectedItem is TableData table && table != null)
            {
                SelectedTableData = table;
                DialogResult = DialogResult.OK;
            }
        }
    }
}
